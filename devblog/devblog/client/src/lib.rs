mod api;
mod components;
mod pages;
mod router;
mod store;

use api::*;
use components::navbar::Navbar;
use store::*;
use stylist::yew::styled_component;
use yew::{html, Html};
use yew_router::{BrowserRouter, Switch};

use crate::router::{switch, Route};

#[styled_component(App)]
pub fn app() -> Html {
    html! {
        <div>
            <BrowserRouter>
                <Navbar />
                <Switch<Route> render={switch} />
            </BrowserRouter>
        </div>
    }
}
