use crate::{pages::sign_out::SignOut, router::Route, store::Store};
use stylist::Style;
use yew::prelude::*;
use yew_router::prelude::*;
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/navbar.css");

#[function_component(Navbar)]
pub fn navbar() -> Html {
    let style = Style::new(STYLE).unwrap();
    let store = use_store_value::<Store>();

    html! {
        <div class={style}>
            <nav class="navbar">
                <Link<Route> to={Route::Home} classes="logo-link">
                    <div class="logo">
                        <span>{"The DevBlog"}</span>
                    </div>
                </Link<Route>>

                <div class="nav-menus-container">
                    <Link<Route> to={Route::Home}>{"Home"}</Link<Route>>
                    <Link<Route> to={Route::Posts}>{"Posts"}</Link<Route>>
                    <Link<Route> to={Route::About}>{"About"}</Link<Route>>

                    if store.admin {
                        <Link<Route> to={Route::Insights}>{"Insights"}</Link<Route>>
                    }

                    if store.authenticated {
                        <Link<Route> to={Route::Account}>{"Account"}</Link<Route>>
                        <SignOut />
                    } else {
                        <Link<Route> to={Route::SignIn}>{"SignIn"}</Link<Route>>
                        <Link<Route> to={Route::SignUp}>{"SignUp"}</Link<Route>>
                    }
                </div>
            </nav>
        </div>
    }
}
