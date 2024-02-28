use crate::{
    helpers::{self, CustomCallback},
    icons::icons::BellIcon,
    store::Store,
    Api, Notification,
};
use gloo_net::http::Method;
use stylist::Style;
use yew::prelude::*;
use yew_router::hooks::use_location;
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/notifications.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub is_bell_clicked: bool,
    pub is_menu_clicked: bool,
    pub onclick_bell: Callback<()>,
}

#[function_component(Notifications)]
pub fn notifications(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();
    let display = use_state(|| "none".to_string());
    let loading = use_state(|| true);
    let notifications = use_state(|| Vec::<Notification>::new());
    let notifications_cb = CustomCallback::new(&notifications);
    let store = use_store_value::<Store>();
    let onclick_bell = props.onclick_bell.clone();
    let location = use_location();

    // get all notifications for user
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
                    helpers::emit(&notifications_cb, res).await;
                    loading_clone.set(false);
                }
            }
        });
    };

    let authenticated = store.authenticated.clone();
    use_effect_with((), move |_| {
        if authenticated {
            get_notifications();
        }
    });

    let display_clone = display.clone();
    use_effect_with(notifications.clone(), move |n| {
        display_clone.set(if n.len() > 0 { "inline" } else { "none" }.to_string());
    });
    // let token = store.token.clone();
    // let username = store.username.clone();

    html! {
        if store.authenticated {
            <div class={style}>
                    <div class="notification-drop-down">

                    if notifications.len() > 0 {
                        <span class="bell-icon" onclick={move |_| {onclick_bell.emit(())}}>
                            <BellIcon />
                        </span>
                    }

                    <span class="notification-count">{notifications.len()}</span>
                        <div class="notifications">
                        if !*loading && props.is_bell_clicked && !props.is_menu_clicked {
                            {for notifications.iter().enumerate().map(|(_idx, n)| {

                            let store_clone = store.clone();
                            let id = n.post_id;
                            let delete_notification = {
                                Callback::from(move |_| {
                                    let hdrs = helpers::create_auth_header(&store_clone.token);
                                    let username = store_clone.username.clone();

                                    wasm_bindgen_futures::spawn_local(async move {
                                        let response = Api::DeleteNotification(id, username)
                                            .fetch(Some(hdrs), None, Method::DELETE)
                                            .await;
                                    });
                                })
                            };

                            html! {
                            <div class="">
                                // thumbnail
                                <span>
                                    <img src={n.img_url.clone()} alt="post thumbnail"/>
                                    </span>
                                    <div class="notification-txt">
                                        <span>{n.username.clone()}{" posted"}</span>
                                        <span onclick={delete_notification}>{" dismiss"}</span>
                                    </div>
                                </div>
                            }})}
                        }
                    </div>
                </div>
            </div>
        }
    }
}
