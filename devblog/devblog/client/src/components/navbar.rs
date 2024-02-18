use crate::{icons::icons::MenuIcon, pages::sign_out::SignOut, router::Route, store::Store};
use stylist::Style;
use yew::prelude::*;
use yew_router::prelude::*;
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/navbar.css");

#[function_component(Navbar)]
pub fn navbar() -> Html {
    let style = Style::new(STYLE).unwrap();
    let is_menu_clicked = use_state(|| false);
    let nav_display = use_state(|| "none".to_string());
    let store = use_store_value::<Store>();
    let location = use_location();

    let is_active = |path: String| -> String {
        match location.clone().unwrap().path() == path {
            true => "active".to_string(),
            false => "non-active".to_string(),
        }
    };

    let st = format!(r#":root {{ display: {} }}"#, *nav_display);
    let nav_links_style = Style::new(st).unwrap();

    // set 'is_menu_clicked' state to true
    let is_menu_clicked_clone = is_menu_clicked.clone();
    let menu_click = move |_| {
        is_menu_clicked_clone.set(!*is_menu_clicked_clone);
    };

    // reset 'is_menu_clicked' state when path changes
    let path = location.clone().unwrap().path().to_string();
    let is_menu_clicked_clone = is_menu_clicked.clone();
    use_effect_with(path, move |_| {
        is_menu_clicked_clone.set(false);
    });

    // show / hide nav menu
    let is_menu_clicked_clone = is_menu_clicked.clone();
    use_effect_with(
        is_menu_clicked_clone.clone(),
        move |_| match *is_menu_clicked_clone {
            true => nav_display.set("flex".to_string()),
            false => nav_display.set("none".to_string()),
        },
    );

    html! {
        <div class={style}>
            <nav class="navbar">
                <Link<Route> to={Route::Home} classes="logo-link">
                    <div class="logo">
                        <span>{"The DevBlog"}</span>
                    </div>
                </Link<Route>>

                <div class="nav-menus-container">

                    <div class="nav-drop-down">
                        <span class="nav-icon" onclick={menu_click}>
                            <MenuIcon />
                        </span>

                        <div class={classes!("nav-links", nav_links_style)}>
                            <span class={is_active("/".to_string())}><Link<Route> to={Route::Home}>{"Home"}</Link<Route>></span>
                            <span class={is_active("/posts".to_string())}><Link<Route> to={Route::Posts}>{"Posts"}</Link<Route>></span>
                            <span class={is_active("/about".to_string())}><Link<Route> to={Route::About}>{"About"}</Link<Route>></span>

                            if store.admin {
                                <span class={is_active("/insights".to_string())}><Link<Route> to={Route::Insights}>{"Insights"}</Link<Route>></span>
                            }

                            if store.authenticated {
                                <span class={is_active("/account".to_string())}><Link<Route> to={Route::Account}>{"Account"}</Link<Route>></span>
                                <SignOut />
                            } else {
                                <span class="nav-accounts">
                                    <span class={is_active("/signin".to_string())}><Link<Route> to={Route::SignIn}>{"SignIn"}</Link<Route>></span>
                                    <span class={is_active("/signup".to_string())}><Link<Route> to={Route::SignUp}>{"SignUp"}</Link<Route>></span>
                                </span>
                            }
                        </div>
                    </div>
                </div>
            </nav>
        </div>
    }
}
