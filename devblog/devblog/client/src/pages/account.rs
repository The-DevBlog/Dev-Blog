use gloo_net::http::{Headers, Method};
use stylist::Style;
use yew::prelude::*;
use yewdux::use_store_value;

use crate::{
    helpers::{self, CustomCallback},
    store::Store,
    Api, User,
};

const STYLE: &str = include_str!("styles/account.css");

#[function_component(Account)]
pub fn account() -> Html {
    let style = Style::new(STYLE).unwrap();
    let user = use_state(User::default);
    let user_cb = CustomCallback::new(&user);
    let store = use_store_value::<Store>();

    // get current user
    use_effect_with((), |_| {
        wasm_bindgen_futures::spawn_local(async move {
            let hdrs = Headers::new();
            let auth = format!("Bearer {}", store.token.clone());
            hdrs.append("Authorization", &auth);

            let res = Api::GetCurrentUser
                .fetch(Some(hdrs), None, Method::GET)
                .await;

            helpers::emit(&user_cb, res.unwrap()).await;
        })
    });

    html! {
        <section class={style}>
            <div class="account">
                <div>
                    <h1>{"Account Details"}</h1>
                    <p>{"Username: "}{&user.username}</p>
                    <p>{"Email: "}{&user.email}</p>
                </div>
                <div>
                    <h1>{"Email Preferences"}</h1>
                    <input type="checkbox"
                        checked={user.subscribed}
                        // onClick={handleSubscribeChange}
                        />
                    <span>{"Subscribed"}</span>
                </div>
            </div>
        </section>
    }
}
