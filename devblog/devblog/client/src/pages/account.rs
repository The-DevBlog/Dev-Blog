use std::rc::Rc;

use crate::{
    helpers::{self, on_click, CustomCallback},
    store::Store,
    Api, User,
};
use gloo_net::http::{Headers, Method};
use stylist::Style;
use yew::prelude::*;
use yew_router::hooks::use_navigator;
use yewdux::prelude::*;

const STYLE: &str = include_str!("styles/account.css");

#[function_component(Account)]
pub fn account() -> Html {
    let style = Style::new(STYLE).unwrap();
    let user = use_state(User::default);
    let user_cb = CustomCallback::new(&user);
    let nav = use_navigator().unwrap();
    let store = use_store_value::<Store>();

    // get current user
    let token = store.token.clone();
    use_effect_with((), |_| {
        wasm_bindgen_futures::spawn_local(async move {
            let hdrs = Headers::new();
            let auth = format!("Bearer {}", token);
            hdrs.append("Authorization", &auth);

            let res = Api::GetCurrentUser
                .fetch(Some(hdrs), None, Method::GET)
                .await;

            helpers::emit(&user_cb, res.unwrap()).await;
        })
    });

    // toggle subscription state of current user
    let toggle_subscribe = {
        let token = store.token.clone();
        let api = Rc::new(Api::ToggleSubscribe);
        on_click(token, api, Method::PUT, None)
    };

    let delete_account = {
        let token = store.token.clone();
        let api = Rc::new(Api::DeleteCurrentAccount);
        on_click(token, api, Method::DELETE, Some(nav))
    };

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
                        onclick={toggle_subscribe}/>
                    <span>{"Subscribed"}</span>
                </div>
                <button onclick={delete_account}>{"Delete Account"}</button>
            </div>
        </section>
    }
}
