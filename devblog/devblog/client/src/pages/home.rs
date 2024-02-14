use crate::{
    components::post::Post,
    helpers::{self, CustomCallback},
    Api, PostModel,
};
use gloo_net::http::Method;
use stylist::Style;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/home.css");

#[function_component(Home)]
pub fn home() -> Html {
    let style = Style::new(STYLE).unwrap();
    let latest_post = use_state(|| PostModel::default());
    let total_posts_count = use_state(|| i32::default());
    let latest_post_cb = CustomCallback::new(&latest_post);
    let total_posts_count_cb = CustomCallback::new(&total_posts_count);

    let get_latest_post = |latest_post_callback: Callback<PostModel>,
                           total_posts_count_callback: Callback<i32>| {
        wasm_bindgen_futures::spawn_local(async move {
            let res = Api::GetPost(-1).fetch(None, None, Method::GET).await;
            helpers::emit(&latest_post_callback, res.unwrap()).await;

            let response = Api::GetPostsCount.fetch(None, None, Method::GET).await;
            helpers::emit(&total_posts_count_callback, response.unwrap()).await;
        });
    };

    // get latest page and posts count
    let latest_post_cb_clone = latest_post_cb.clone();
    let total_posts_count_cb_clone = total_posts_count_cb.clone();
    use_effect_with((), move |_| {
        get_latest_post(latest_post_cb_clone, total_posts_count_cb_clone);
    });

    let on_post_delete = {
        Callback::from(move |_| {
            get_latest_post(latest_post_cb.clone(), total_posts_count_cb.clone());
        })
    };

    html! {
        <section class={style}>
            <div class="home">
                <Post post={(*latest_post).clone()} post_number={*total_posts_count} on_post_delete={&on_post_delete}/>
            </div>
        </section>
    }
}
