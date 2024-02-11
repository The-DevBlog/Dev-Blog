use crate::router::Route;
use stylist::Style;
use yew::prelude::*;
use yew_router::prelude::*;

const STYLE: &str = include_str!("styles/navbar.css");

#[function_component(Navbar)]
pub fn navbar() -> Html {
    let style = Style::new(STYLE).unwrap();

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
                    <Link<Route> to={Route::AddPost}>{"AddPost"}</Link<Route>>
                    <Link<Route> to={Route::About}>{"About"}</Link<Route>>
                    <Link<Route> to={Route::Insights}>{"Insights"}</Link<Route>>
                    <Link<Route> to={Route::Account}>{"Account"}</Link<Route>>
                    <Link<Route> to={Route::SignIn}>{"SignIn"}</Link<Route>>
                    <Link<Route> to={Route::SignOut}>{"SignOut"}</Link<Route>>
                    <Link<Route> to={Route::SignUp}>{"SignUp"}</Link<Route>>
                </div>
            </nav>
        </div>
    }
}
