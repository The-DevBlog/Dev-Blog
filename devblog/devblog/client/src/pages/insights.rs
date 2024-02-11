use crate::{
    helpers::{self, on_click, CustomCallback},
    store::Store,
    Api, User,
};
use gloo_net::http::{Headers, Method};
use std::rc::Rc;
use stylist::Style;
use yew::prelude::*;
use yewdux::prelude::*;

const STYLE: &str = include_str!("styles/insights.css");

#[function_component(Insights)]
pub fn insights() -> Html {
    let style = Style::new(STYLE).unwrap();
    let users = use_state(|| vec![User::default()]);
    let users_cb = CustomCallback::new(&users);
    let store = use_store_value::<Store>();

    // get all users
    let token = store.token.clone();
    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            let hdrs = Headers::new();
            let auth = format!("Bearer {}", token);
            hdrs.append("Authorization", &auth);

            let res = Api::GetUsers.fetch(Some(hdrs), None, Method::GET).await;
            helpers::emit(&users_cb, res.unwrap()).await;
        });
    });

    html! {
        <section class={style}>
            <div class="insights">
                <p>{"Total Users: "}</p>
                <p>{"Subscribed Users: "}</p>

                <table class="user-table">
                    <thead>
                        <tr>
                            <th>{"Username"}</th>
                            <th>{"Email"}</th>
                            <th>{"Subscribed"}</th>
                            <th>{"Delete"}</th>
                        </tr>
                    </thead>
                    <tbody>
                        {for users.iter().map(|user|{
                            let onclick = {
                                let token = store.token.clone();
                                let api = Rc::new(Api::DeleteAccount(user.username.clone()));
                                on_click(token, api, Method::DELETE, None)
                            };

                            html! {
                                <tr>
                                    <td>{&user.username}</td>
                                    <td>{&user.email}</td>
                                    <td>{if user.subscribed {"yes"} else {"no"}}</td>
                                    <td><button {onclick}>{"X"}</button></td>
                                </tr>
                            }
                        })}
                    </tbody>
                </table>
            </div>
        </section>
    }
}
