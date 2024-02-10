use crate::{components::items::text_input::TextInput, helpers, Api, User, UserField};
use stylist::Style;
use yew::prelude::*;
use yew_router::hooks::use_navigator;

const STYLE: &str = include_str!("styles/signIn.css");

#[function_component(SignIn)]
pub fn sign_in() -> Html {
    let style = Style::new(STYLE).unwrap();
    let user = use_state(User::default);
    let nav = use_navigator().unwrap();
    // let (_state, dispatch) = use_store::<Store>();
    // let onsubmit = { helpers::onsubmit(&user, nav, Api::SignIn) };

    html! {
        <div class={style}>
            <div class="sign-in-container">

                // {error && <span>{error}</span>}

                <form onsubmit={helpers::onsubmit(&user, nav, Api::SignIn)} class="sign-in">
                // <form {onsubmit}>
                    <TextInput label="username" input_type="text" value={user.username.clone()} onchange={helpers::onchange(&user, UserField::Username)}/>
                    <TextInput label="password" input_type="password" value={user.password.clone()} onchange={helpers::onchange(&user, UserField::Password)}/>
                    <button>{"Login"}</button>
                </form>
            </div>
        </div>
    }
}
