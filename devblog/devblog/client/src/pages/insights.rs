use crate::{Api, CustomCallback, User};
use gloo_net::http::{Headers, Method};
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
            let hdrs = Headers::new();
            hdrs.append("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyTmFtZSI6ImRldm1hc3RlciIsImVtYWlsIjoiZGV2bWFzdGVyQHRoZWRldmJsb2cubmV0IiwianRpIjoiN2M3OGQxNDUtNzk3OC00OGJmLWJjNGQtOWRlYmU2ZTA4MDBlIiwicm9sZSI6IkFkbWluIiwiZXhwIjoxNzA3NTEwMTYzLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDQ4Mi8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDQ4Mi9hcGkvIn0.iDAeXuhIB4eaf0bnirAvI3H9TFABrqVTZZNhEsickAM");

            let _ = Api::GetUsers
                .fetch(Some(users_cb), Some(hdrs), None, Method::GET)
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
