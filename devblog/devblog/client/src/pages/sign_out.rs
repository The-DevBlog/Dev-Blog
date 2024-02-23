use crate::{helpers, Api, Store, User};
use std::rc::Rc;
use stylist::Style;
use yew::prelude::*;
use yew_router::hooks::use_navigator;
use yewdux::prelude::*;

const STYLE: &str = include_str!("styles/signOut.css");

#[function_component(SignOut)]
pub fn sign_out() -> Html {
    let style = Style::new(STYLE).unwrap();
    let user = use_state(User::default);
    let nav = use_navigator().unwrap();
    let (_, dispatch) = use_store::<Store>();

    html! {
            <form class={style} onsubmit={helpers::on_submit(&user, nav, Rc::new(Api::SignOut), dispatch)}>
                <button class="logout">{"Logout"}</button>
            </form>
    }
}
