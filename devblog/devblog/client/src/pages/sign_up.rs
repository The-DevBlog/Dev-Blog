use std::rc::Rc;

use crate::{components::items::text_input::TextInput, helpers, Api, Store, User, UserField};
use stylist::Style;
use yew::prelude::*;
use yew_router::hooks::use_navigator;
use yewdux::prelude::*;

const STYLE: &str = include_str!("styles/signUp.css");

#[function_component(SignUp)]
pub fn sign_up() -> Html {
    let style = Style::new(STYLE).unwrap();
    let user = use_state(User::default);
    let nav = use_navigator().unwrap();
    let (_, dispatch) = use_store::<Store>();

    html! {
        <div class={style}>
            <div class="sign-up-container">
                <form onsubmit={helpers::on_submit(&user, nav, Rc::new(Api::SignUp), dispatch)}>
                    <TextInput label="username" input_type="text" value={user.username.clone()} onchange={helpers::on_change(&user, UserField::Username)}/>
                    <TextInput label="email" input_type="text" value={user.email.clone()} onchange={helpers::on_change(&user, UserField::Email)}/>
                    <TextInput label="password" input_type="password" value={user.password_hash.clone()} onchange={helpers::on_change(&user, UserField::PasswordHash)}/>
                    // <TextInput label="confirmPassword" input_type="password" value={register.confirm_password.clone()} onchange={onchange(RegisterField::ConfirmPassword)}/>
                    <button>{"Sign Up"}</button>
                </form>
            </div>
        </div>
    }
}
