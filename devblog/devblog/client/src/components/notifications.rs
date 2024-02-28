use crate::{icons::icons::BellIcon, store::Store};
use stylist::Style;
use yew::{function_component, html, use_state, Callback, Html, Properties};
use yew_router::hooks::use_location;
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/notifications.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub bell_display: String,
    pub bell_clicked: bool,
    pub menu_clicked: bool,
}

#[function_component(Notifications)]
pub fn notifications() -> Html {
    let style = Style::new(STYLE).unwrap();
    let store = use_store_value::<Store>();
    let menu_clicked = use_state(|| false);
    let bell_display = use_state(|| "none".to_string());
    let location = use_location();

    let on_menu_click = || Callback::from(move |_| {});

    let on_bell_click = || Callback::from(move |_| {});

    html! {
        if store.authenticated {
            <div class={style}>
                <div class="notification-drop-down">

                    <span>
                        <BellIcon />
                    </span>

                    <span class="notification-count">{"length"}</span>
                    <div class="notifications">
                        // notifications map
                            <div class="">
                                <span></span>
                                <div class="notification-txt">
                                    <span>{" posted"}</span>
                                    <span>{" dismiss"}</span>
                                </div>
                            </div>
                        //
                    </div>
                </div>
            </div>
        }
    }
}
