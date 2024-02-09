use stylist::Style;
use yew::prelude::*;
use yew_router::hooks::use_navigator;

use crate::{components::items::text_input::TextInput, helpers, ApiPost, User, UserField};

const STYLE: &str = include_str!("styles/signUp.css");

#[function_component(SignUp)]
pub fn sign_up() -> Html {
    let style = Style::new(STYLE).unwrap();
    let user = use_state(User::default);
    let nav = use_navigator().unwrap();

    html! {
        <div class={style}>
            <div class="sign-up-container">
                <form onsubmit={helpers::onsubmit(&user, nav, ApiPost::SignUp)}>
                    <TextInput label="username" input_type="text" value={user.username.clone()} onchange={helpers::onchange(&user, UserField::Username)}/>
                    <TextInput label="email" input_type="text" value={user.email.clone()} onchange={helpers::onchange(&user, UserField::Email)}/>
                    <TextInput label="password" input_type="password" value={user.password_hash.clone()} onchange={helpers::onchange(&user, UserField::PasswordHash)}/>
                    // <TextInput label="confirmPassword" input_type="password" value={register.confirm_password.clone()} onchange={onchange(RegisterField::ConfirmPassword)}/>
                    <button>{"Sign Up"}</button>
                </form>
            </div>
        </div>
    }
}
