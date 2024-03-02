use crate::{components::items::text_input::TextInput, helpers, Api, Store, User, UserField};
use gloo::utils::window;
use std::rc::Rc;
use stylist::Style;
use yew::prelude::*;
use yew_router::hooks::use_navigator;
use yewdux::prelude::*;

const STYLE: &str = include_str!("styles/signIn.css");

#[function_component(SignIn)]
pub fn sign_in() -> Html {
    let style = Style::new(STYLE).unwrap();
    let user = use_state(User::default);
    let nav = use_navigator().unwrap();
    let (_, dispatch) = use_store::<Store>();

    html! {
        <div class={style}>
            <div class="sign-in-container">
                // {error && <span>{error}</span>}
                <form onsubmit={helpers::on_submit(&user, nav, Rc::new(Api::SignIn), dispatch)} class="sign-in">
                    <TextInput label="username" input_type="text" value={user.username.clone()} onchange={helpers::on_change(&user, UserField::Username)}/>
                    <TextInput label="password" input_type="password" value={user.password.clone()} onchange={helpers::on_change(&user, UserField::Password)}/>
                    <button>{"Login"}</button>
                </form>
            </div>
        </div>
    }
}
