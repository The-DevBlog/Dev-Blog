use std::ops::Deref;

use crate::{
    helpers::{self, CustomCallback},
    icons::icons::BellIcon,
    store::Store,
    Api, Notification,
};
use gloo::console::log;
use gloo_net::http::Method;
use stylist::Style;
use yew::prelude::*;
use yew_router::hooks::use_location;
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/notifications.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub bell_display: String,
    pub is_bell_clicked: bool,
    pub is_menu_clicked: bool,
    pub onclick_bell: Callback<()>,
    pub set_bell_display: Callback<String>,
}

#[function_component(Notifications)]
pub fn notifications(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();
    let loading = use_state(|| true);
    let notifications = use_state(|| vec![Notification::default()]);
    let notifications_cb = CustomCallback::new(&notifications);
    let store = use_store_value::<Store>();
    let location = use_location();
    let onclick_bell = props.onclick_bell.clone();

    let on_menu_click = || {};

    let token = store.token.clone();
    let username = store.username.clone();
    let loading_clone = loading.clone();
    let get_notifications = move || {
        let hdrs = helpers::create_auth_header(&token);
        wasm_bindgen_futures::spawn_local(async move {
            let response = Api::GetNotifications(username)
                .fetch(Some(hdrs), None, Method::GET)
                .await;

            if let Some(res) = response {
                if res.status() == 200 {
                    // log!(res.text().await.unwrap());
                    helpers::emit(&notifications_cb, res).await;
                    loading_clone.set(false);
                }
            }
        });
    };

    use_effect_with((), move |_| {
        log!("GETTING NOTIFICATIONS");
        get_notifications();
    });

    // let set_bell_display = props.set_bell_display.clone();
    // let is_bell_clicked = props.is_bell_clicked.clone();
    // let path = location.unwrap().path().to_string();
    // use_effect_with(
    //     (
    //         props.is_bell_clicked.clone(),
    //         set_bell_display.clone(),
    //         path,
    //     ),
    //     move |_| set_bell_display.emit(if is_bell_clicked { "flex" } else { "none" }.to_string()),
    // );

    use_effect_with(
        (
            props.is_bell_clicked,
            props.set_bell_display.clone(),
            location.unwrap().path().to_string(),
        ),
        move |(is_bell_clicked, set_bell_display, _path)| {
            set_bell_display.emit(if *is_bell_clicked { "flex" } else { "none" }.to_string())
        },
    );

    html! {
        if store.authenticated {
            <div class={style}>
                <div class="notification-drop-down">

                    <span onclick={move |_| {onclick_bell.emit(())}}>
                        <BellIcon />
                    </span>

                    <span class="notification-count">{"length"}</span>
                    <div class="notifications">
                        if !*loading {
                            // notifications map
                            {for notifications.iter().enumerate().map(|(idx, n)| html! {
                                <div class="">
                                    // thumbnail
                                    <span>
                                        <img src={n.img_url.clone()} alt="post thumbnail"/>
                                    </span>
                                    <div class="notification-txt">
                                        <span>{n.username.clone()}{" posted"}</span>
                                        <span>{" dismiss"}</span>
                                    </div>
                                </div>
                            })}
                        }
                    </div>
                </div>
            </div>
        }
    }
}
