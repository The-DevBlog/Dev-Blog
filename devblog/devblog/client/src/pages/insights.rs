use crate::{
    helpers::{self, CustomCallback},
    store::Store,
    Api, UserInfo,
};
use gloo_net::http::{Headers, Method};
use stylist::Style;
use yew::prelude::*;
use yewdux::prelude::*;

const STYLE: &str = include_str!("styles/insights.css");

#[function_component(Insights)]
pub fn insights() -> Html {
    let style = Style::new(STYLE).unwrap();
    let users = use_state(|| vec![UserInfo::default()]);
    let users_cb = CustomCallback::new(&users);
    let store = use_store_value::<Store>();

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            let hdrs = Headers::new();
            let auth = format!("Bearer {}", store.token.clone());
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
                        {for users.iter().map(|user| html! {
                            <tr>
                                <td>{&user.username}</td>
                                <td>{&user.email}</td>
                                <td>{if user.subscribed {"yes"} else {"no"}}</td>
                            </tr>
                        })}
                    </tbody>
                </table>
            </div>
        </section>
    }
}
