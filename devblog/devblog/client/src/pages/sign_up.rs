use std::ops::Deref;

use gloo_net::http::{Headers, Method};
use stylist::Style;
use web_sys::HtmlInputElement;
use yew::prelude::*;

use crate::{
    components::items::text_input::TextInput, Api, CustomCallback, Register, RegisterField,
};

const STYLE: &str = include_str!("styles/signUp.css");

#[function_component(SignUp)]
pub fn sign_up() -> Html {
    let style = Style::new(STYLE).unwrap();
    let register = use_state(Register::default);
    let register_cb = CustomCallback::new(&register);

    let onchange = |field: RegisterField| -> Callback<Event> {
        let register = register.clone();
        Callback::from(move |e: Event| {
            let register_clone = register.deref().clone();
            let input = e.target_dyn_into::<HtmlInputElement>();
            if let Some(value) = input {
                let tmp = register_clone.set(value.value(), &field);
                register.set(tmp);
            }
        })
    };

    let onsubmit = {
        let register = register.clone();
        Callback::from(move |e: SubmitEvent| {
            e.prevent_default();
            let register_cb = register_cb.clone();
            let register = register.deref().clone();
            let hdrs = Headers::new();
            hdrs.append("content-type", "application/json");
            wasm_bindgen_futures::spawn_local(async move {
                Api::SignUp
                    .call(
                        register_cb.clone(),
                        Some(hdrs),
                        Method::POST,
                        Some(register),
                    )
                    .await;
            });
        })
    };

    html! {
        <div class={style}>
            <div class="sign-up-container">
                <form {onsubmit}>
                    <TextInput label="username" input_type="text" value={register.username.clone()} onchange={onchange(RegisterField::Username)}/>
                    <TextInput label="email" input_type="text" value={register.email.clone()} onchange={onchange(RegisterField::Email)}/>
                    <TextInput label="password" input_type="password" value={register.password_hash.clone()} onchange={onchange(RegisterField::PasswordHash)}/>
                    <TextInput label="confirmPassword" input_type="password" value={register.confirm_password.clone()} onchange={onchange(RegisterField::ConfirmPassword)}/>
                    <button>{"Sign Up"}</button>
                </form>
            </div>
        </div>
    }
}
