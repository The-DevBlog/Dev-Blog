use crate::{Api, CustomCallback, User};
use gloo_net::http::Method;
use stylist::Style;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/insights.css");

#[function_component(Insights)]
pub fn insights() -> Html {
    let style = Style::new(STYLE).unwrap();
    let users = use_state(|| vec![User::default()]);
    let users_cb = CustomCallback::new(&users);

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async {
            let _ = Api::GetUsers
                .fetch(Some(users_cb), None, None, Method::GET)
                .await;
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
