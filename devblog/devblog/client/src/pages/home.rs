use crate::{
    components::{items::text_input::TextInput, post::Post},
    helpers::{self, CustomCallback},
    store::Store,
    Api, PostModel, YoutubeVideo,
};
use gloo_net::http::Method;
use std::ops::Deref;
use stylist::Style;
use web_sys::HtmlInputElement;
use yew::prelude::*;
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/home.css");

#[function_component(Home)]
pub fn home() -> Html {
    let style = Style::new(STYLE).unwrap();
    let loading = use_state(|| true);
    let latest_post = use_state(|| PostModel::default());
    let total_posts_count = use_state(|| i32::default());
    let latest_post_cb = CustomCallback::new(&latest_post);
    let total_posts_count_cb = CustomCallback::new(&total_posts_count);
    let url = use_state(|| "".to_string());
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
                let js: YoutubeVideo = res.json().await.unwrap();
                url_clone.set(js.url);
            }
        });
    };

    // get latest page and posts count
    let latest_post_cb_clone = latest_post_cb.clone();
    let total_posts_count_cb_clone = total_posts_count_cb.clone();
    let url_clone = url.clone();
    let loading_clone = loading.clone();
    use_effect_with((), move |_| {
        get_latest_post(
            latest_post_cb_clone,
            total_posts_count_cb_clone,
            loading_clone,
        );
        get_video_url(url_clone);
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

    let update_url = {
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
                // latest post
                if !*loading {
                    <Post post={(*latest_post).clone()} post_number={*total_posts_count} on_post_delete={&on_post_delete}/>
                } else {
                    <h1>{"Loading..."}</h1>
                }

                // youtube video
                <div class="youtube-video">

                    if store.admin && store.authenticated {
                        <form onsubmit={update_url} class="update-video">
                            <TextInput label="url" input_type="text" value={url.deref().clone()} onchange={on_url_change}/>
                            <button>{"Update"}</button>
                        </form>
                    }

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
