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

    // get latest page and posts count
    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            let res = Api::GetPost(-1).fetch(None, None, Method::GET).await;
            helpers::emit(&latest_post_cb, res.unwrap()).await;

            let response = Api::GetPostsCount.fetch(None, None, Method::GET).await;
            helpers::emit(&total_posts_count_cb, response.unwrap()).await;
        });
    });

    html! {
        <section class={style}>
            <div class="home">
                <Post post={(*latest_post).clone()} post_number={*total_posts_count} />
            </div>
        </section>
    }
}
