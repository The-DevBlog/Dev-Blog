use crate::{
    components::{post::Post, text_input::TextInput},
    helpers::{self, CustomCallback},
    router::Route,
    store::Store,
    Api, PostModel, User, YoutubeVideo,
};
use gloo_net::http::Method;
use std::ops::Deref;
use stylist::Style;
use web_sys::HtmlInputElement;
use yew::prelude::*;
use yew_router::components::Link;
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/home.css");

#[function_component(Home)]
pub fn home() -> Html {
    let style = Style::new(STYLE).unwrap();
    let loading = use_state(|| true);
    let subscribed_user = use_state(|| false);
    let subscribed_guest = use_state(|| false);
    let latest_post = use_state(|| PostModel::default());
    let total_posts_count = use_state(|| i32::default());
    let latest_post_cb = CustomCallback::new(&latest_post);
    let total_posts_count_cb = CustomCallback::new(&total_posts_count);
    let url = use_state(|| "".to_string());
    let email = use_state(|| String::default());
    let store = use_store_value::<Store>();

    let get_latest_post = |latest_post_callback: Callback<PostModel>,
                           total_posts_count_callback: Callback<i32>,
                           loading_clone: UseStateHandle<bool>| {
        wasm_bindgen_futures::spawn_local(async move {
            loading_clone.set(true);
            let response = Api::GetPost(-1).fetch(None, None, Method::GET).await;
            helpers::emit(&latest_post_callback, response.unwrap()).await;

            let response = Api::GetPostsCount.fetch(None, None, Method::GET).await;
            helpers::emit(&total_posts_count_callback, response.unwrap()).await;
            loading_clone.set(false);
        });
    };

    let get_video_url = |url_clone: UseStateHandle<String>| {
        wasm_bindgen_futures::spawn_local(async move {
            let response = Api::GetVideoUrl.fetch(None, None, Method::GET).await;
            if let Some(res) = response {
                if res.status() == 200 {
                    let js: YoutubeVideo = res.json().await.unwrap();
                    url_clone.set(js.url);
                }
            }
        });
    };

    let token = store.token.clone();
    let subscribed_clone = subscribed_user.clone();
    let get_user_info = || {
        wasm_bindgen_futures::spawn_local(async move {
            let hdrs = helpers::create_auth_header(&token);
            let response = Api::GetCurrentUser
                .fetch(Some(hdrs), None, Method::GET)
                .await;

            if let Some(res) = response {
                if res.status() == 200 {
                    let data = res.json::<User>().await.unwrap();
                    subscribed_clone.set(data.subscribed);
                }
            }
        });
    };

    // get latest page and posts count
    let latest_post_cb_clone = latest_post_cb.clone();
    let total_posts_count_cb_clone = total_posts_count_cb.clone();
    let url_clone = url.clone();
    let loading_clone = loading.clone();
    let authenticated = store.authenticated.clone();
    let subscribed_guest_clone = subscribed_guest.clone();
    use_effect_with((), move |_| {
        get_latest_post(
            latest_post_cb_clone,
            total_posts_count_cb_clone,
            loading_clone,
        );
        get_video_url(url_clone);

        // if guest, check local storage to see if he/she is email subscribed
        let session_storage = web_sys::window()
            .unwrap()
            .session_storage()
            .unwrap()
            .unwrap();
        let subscribed = session_storage.get_item("subscribed_guest").unwrap();
        if let Some(sub) = subscribed {
            match sub.as_str() {
                "true" => subscribed_guest_clone.set(true),
                _ => subscribed_guest_clone.set(false),
            }
        }

        if authenticated {
            get_user_info();
        }
    });

    let on_post_delete = {
        let loading = loading.clone();
        Callback::from(move |_| {
            get_latest_post(
                latest_post_cb.clone(),
                total_posts_count_cb.clone(),
                loading.clone(),
            );
        })
    };

    let submit_url = {
        let token = store.token.clone();
        let url = url.deref().clone();
        Callback::from(move |e: SubmitEvent| {
            e.prevent_default();
            let token = token.clone();
            let url = url.clone();
            wasm_bindgen_futures::spawn_local(async move {
                let hdrs = helpers::create_auth_header(&token);
                hdrs.append("content-type", "application/json");
                let body = Some(helpers::to_jsvalue(url));
                let _response = Api::UpdateVideoUrl
                    .fetch(Some(hdrs), body, Method::PUT)
                    .await;
            });
        })
    };

    // subscribed a user to the email list who is not a devblog user (guest user)
    let submit_email_subscribe = {
        let email = email.clone();
        let subscribed_guest_clone = subscribed_guest.clone();
        Callback::from(move |e: SubmitEvent| {
            e.prevent_default();
            let email = email.clone();
            let subscribed_guest_clone = subscribed_guest_clone.clone();

            wasm_bindgen_futures::spawn_local(async move {
                if let Some(res) = Api::Subscribe(email.deref().to_string())
                    .fetch(None, None, Method::POST)
                    .await
                {
                    if res.status() == 200 {
                        let session_storage = web_sys::window()
                            .unwrap()
                            .session_storage()
                            .unwrap()
                            .unwrap();
                        let _res = session_storage.set_item("subscribed_guest", "true");
                        subscribed_guest_clone.set(true);
                    } else {
                    }
                }
            });
        })
    };

    let on_email_change = {
        let email = email.clone();
        Callback::from(move |e: Event| {
            let input = e.target_dyn_into::<HtmlInputElement>();
            if let Some(value) = input {
                email.set(value.value());
            }
        })
    };

    let on_url_change = {
        let url = url.clone();
        Callback::from(move |e: Event| {
            let input = e.target_dyn_into::<HtmlInputElement>();
            if let Some(value) = input {
                url.set(value.value());
            };
        })
    };

    html! {
        <section class={style}>
            <div class="home">

                // signup / subscribe prompt
                if !store.authenticated {
                    <div class="signup-prompt">
                        <Link<Route> to={Route::SignUp}>
                            <span>{"Join the Party. Sign up!"}</span>
                        </Link<Route>>
                    </div>
                    <div class="subscribe-prompt">
                        if !*subscribed_guest {
                            <span>{"Subscribe to Email!"}</span>
                            <form onsubmit={submit_email_subscribe}>
                                <TextInput label="" input_type="text" value={email.deref().clone()} onchange={on_email_change}/>
                                <button>{"Submit"}</button>
                            </form>
                        } else {
                            <span>{"Thanks for subscribing!"}</span>
                        }
                    </div>
                } else if store.authenticated && !*subscribed_user {
                    <div class="signup-prompt">
                        <Link<Route> to={Route::Account}>
                           <span>{"Subscribe to Email!"}</span>
                        </Link<Route>>
                    </div>
                }

                // latest post
                if !*loading {
                    <Post post={(*latest_post).clone()} post_number={*total_posts_count} on_post_delete={&on_post_delete}/>
                } else {
                    <h1>{"Loading..."}</h1>
                }

                // youtube video
                if store.admin && store.authenticated {
                    <form onsubmit={submit_url} class="update-video">
                        <TextInput label="url" input_type="text" value={url.deref().clone()} onchange={on_url_change}/>
                        <button>{"Update"}</button>
                    </form>
                }
                <div class="youtube-video">
                    <iframe class="youtube-video"
                        allowfullscreen={true}
                        src={url.deref().clone()}
                        title="YouTube Video">
                    </iframe>
                </div>
            </div>
        </section>
    }
}
