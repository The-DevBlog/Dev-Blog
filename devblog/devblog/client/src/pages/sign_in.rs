use crate::{Api, CustomCallback, Login};
use gloo_net::http::Method;
use serde_json::json;
use std::ops::Deref;
use stylist::Style;
use web_sys::HtmlInputElement;
use yew::prelude::*;
// use gloo::console::log;

const STYLE: &str = include_str!("styles/signIn.css");

#[function_component(SignIn)]
pub fn sign_in() -> Html {
    let style = Style::new(STYLE).unwrap();
    let login = use_state(Login::default);
    let login_cb = CustomCallback::new(&login);

    let username_onchange = {
        let login = login.clone();
        Callback::from(move |e: Event| {
            let input = e.target_dyn_into::<HtmlInputElement>();
            let mut login_clone = login.deref().clone();
            if let Some(username) = input {
                login_clone.username = username.value();
                login.set(login_clone);
            }
        })
    };

    let password_onchange = {
        let login = login.clone();
        Callback::from(move |e: Event| {
            let input = e.target_dyn_into::<HtmlInputElement>();
            let mut login_clone = login.deref().clone();
            if let Some(password) = input {
                login_clone.password = password.value();
                login.set(login_clone);
            }
        })
    };

    let onsubmit = {
        Callback::from(move |e: SubmitEvent| {
            e.prevent_default();
            let login_cb = login_cb.clone();
            let login = login.deref().clone();
            let json = json!({"userName": login.username, "password": login.password});
            // {"userName":"devmaster","password":"@Test123!"}
            wasm_bindgen_futures::spawn_local(async move {
                Api::SignIn
                    .call(login_cb.clone(), None, Method::POST, json)
                    .await;
            });
        })
    };

    html! {
            <div class={style}>
                <div class="sign-in-container">
                    <form {onsubmit}>
                        <label for="username">{"username"}</label>
                        <input
                            id="username"
                            type="text"
                            placeholder="username"
                            value="username"
                            onchange={username_onchange}
                        />

                        <label for="password">{"password"}</label>
                        <input
                            id="password"
                            type="password"
                            value="mypassword"
                            onchange={password_onchange}
                        />

                        <button>{"Login"}</button>
                    </form>

                    // {error && <span>{error}</span>}

    // <form onSubmit={handleSubmit} className="sign-in">
    //     <TextField
    //         label="Username"
    //         value={userName}
    //         type="text"
    //         onChange={(e) => setUsername(e.currentTarget.value)}
    //         validateOnFocusOut
    //         required
    //     />

    //     <TextField
    //         label="Password"
    //         value={password}
    //         type="password"
    //         onChange={(e) => setPassword(e.currentTarget.value)}
    //         validateOnFocusOut
    //         required
    //     />

    //     <button>Login</button>
    // </form>
                </div>
            </div>
        }
}
