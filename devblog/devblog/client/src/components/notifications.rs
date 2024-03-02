use crate::{
    helpers::{self, CustomCallback},
    icons::icons::BellIcon,
    router::{PostQuery, Route},
    store::Store,
    Api, Notification,
};
use gloo::console::log;
use gloo_net::http::Method;
use std::{ops::Deref, rc::Rc};
use stylist::Style;
use yew::prelude::*;
use yew_router::hooks::use_navigator;
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
    let navigator = use_navigator();

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
    let username_clone = store.username.clone();
    use_effect_with(username_clone, move |_| {
        if authenticated {
            get_notifications();
        }
    });

    let display_clone = display.clone();
    use_effect_with(notifications.clone(), move |n| {
        display_clone.set(if n.len() > 0 { "inline" } else { "none" }.to_string());
    });

    let nav_to_post = |post_id: u32| -> Callback<MouseEvent> {
        let navigator = navigator.clone();
        Callback::from(move |_| {
            let navigator = navigator.clone().unwrap();
            wasm_bindgen_futures::spawn_local(async move {
                let response = Api::GetPageNumber(post_id)
                    .fetch(None, None, Method::GET)
                    .await;

                if let Some(res) = response {
                    if res.status() == 200 {
                        let data = res.text().await.unwrap();
                        let page_num = serde_json::from_str::<u32>(&data).unwrap();

                        let query = &PostQuery { page: page_num };
                        let _ = navigator.push_with_query(&Route::Posts, &query);
                    }
                }
            });
        })
    };

    let delete_notification = |post_id: u32,
                               store: Rc<Store>,
                               notifications: UseStateHandle<Vec<Notification>>|
     -> Callback<MouseEvent> {
        let store_clone = store.clone();
        let notifications = notifications.clone();

        Callback::from(move |_| {
            let hdrs = helpers::create_auth_header(&store_clone.token);
            let username = store_clone.username.clone();
            let notifications = notifications.clone();

            wasm_bindgen_futures::spawn_local(async move {
                let response = Api::DeleteNotification(post_id, username)
                    .fetch(Some(hdrs), None, Method::DELETE)
                    .await;

                if let Some(res) = response {
                    // if delete is success, remove the deleted notifications from the notifications vec
                    if res.status() == 200 {
                        let mut new_notifications = notifications.deref().clone();
                        if let Some(idx) =
                            new_notifications.iter().position(|n| n.post_id == post_id)
                        {
                            new_notifications.remove(idx);
                            notifications.set(new_notifications);
                        }
                    }
                }
            });
        })
    };

    html! {
        if store.authenticated  && notifications.len() > 0 {
            <div class={style}>
                    <div class="notification-drop-down">

                    <span class="bell-icon" onclick={move |_| {onclick_bell.emit(())}}>
                        <BellIcon />
                    </span>

                    <span class="notification-count">{notifications.len()}</span>
                        <div class="notifications">
                        if !*loading && props.is_bell_clicked && !props.is_menu_clicked {
                            {for notifications.iter().enumerate().map(|(_idx, n)| {
                                let id = n.post_id;
                                let content = match n.notification_type.as_str() {
                                    "Post" => "Devmaster posted",
                                    "Comment" => "commented",
                                    "Reply" => "replied",
                                    _ => "",
                                };

                                html! {
                                    <div class="">
                                        // thumbnail
                                        <span>
                                            <img src={n.img_url.clone()} alt="post thumbnail"/>
                                        </span>

                                        <div class="notification-txt">
                                            <span onclick={nav_to_post(id)}>{content}</span>
                                            <span onclick={delete_notification(id, store.clone(), notifications.clone())}>{" dismiss"}</span>
                                        </div>
                                    </div>
                                }
                            })}
                        }
                    </div>
                </div>
            </div>
        }
    }
}
