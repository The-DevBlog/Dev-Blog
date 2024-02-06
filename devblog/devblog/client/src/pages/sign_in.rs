use crate::{components::items::text_input::TextInput, Api, CustomCallback, Login, LoginField};
use gloo_net::http::{Headers, Method};
use std::ops::Deref;
use stylist::Style;
use web_sys::HtmlInputElement;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/signIn.css");

#[function_component(SignIn)]
pub fn sign_in() -> Html {
    let style = Style::new(STYLE).unwrap();
    let login = use_state(Login::default);
    let login_cb = CustomCallback::new(&login);

    let onchange = |field: LoginField| -> Callback<Event> {
        let login = login.clone();
        Callback::from(move |e: Event| {
            let login_clone = login.deref().clone();
            let input = e.target_dyn_into::<HtmlInputElement>();
            if let Some(value) = input {
                let tmp = login_clone.set(value.value(), &field);
                login.set(tmp);
            }
        })
    };

    let onsubmit = {
        let login = login.clone();
        Callback::from(move |e: SubmitEvent| {
            e.prevent_default();
            let login_cb = login_cb.clone();
            let login = login.deref().clone();
            let hdrs = Headers::new();
            hdrs.append("content-type", "application/json");
            wasm_bindgen_futures::spawn_local(async move {
                Api::SignIn
                    .call(login_cb.clone(), Some(hdrs), Method::POST, Some(login))
                    .await;
            });
        })
    };

    html! {
        <div class={style}>
            <div class="sign-in-container">

                // {error && <span>{error}</span>}

                <form {onsubmit} class="sign-in">
                    <TextInput label="username" input_type="text" value={login.username.clone()} onchange={onchange(LoginField::Username)}/>
                    <TextInput label="password" input_type="password" value={login.password.clone()} onchange={onchange(LoginField::Password)}/>
                    <button>{"Login"}</button>
                </form>
            </div>
        </div>
    }
}
