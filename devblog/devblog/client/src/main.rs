mod api;
mod components;
mod helpers;
mod models;
mod pages;
mod router;

use crate::router::{switch, Route};
use api::*;
use components::navbar::Navbar;
use models::*;
use stylist::yew::Global;
use yew::{function_component, html, Html};
// use yew_oauth2::oauth2::{Config, OAuth2};
use yew_router::{BrowserRouter, Switch};

const STYLE: &str = include_str!("theme.css");

fn main() {
    yew::Renderer::<App>::new().render();
}

#[function_component(App)]
pub fn app() -> Html {
    // let config = Config {};

    html! {
        <div>
            // <OAuth2 {config}>
            // </OAuth2>
            <Global css={STYLE} />
            <BrowserRouter>
                <Navbar />
                <Switch<Route> render={switch} />
            </BrowserRouter>
        </div>
    }
}
