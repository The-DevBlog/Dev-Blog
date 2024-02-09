use crate::{components::post::Post, ApiGet, CustomCallback, PostModel};
use stylist::Style;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/home.css");

#[function_component(Home)]
pub fn home() -> Html {
    let style = Style::new(STYLE).unwrap();
    let latest_post = use_state(|| PostModel::default());
    let total_posts_count = use_state(|| i32::default());
    let callback_latest_post = CustomCallback::new(&latest_post);
    let callback_total_posts_count = CustomCallback::new(&total_posts_count);

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            let _ = ApiGet::Post(-1).fetch(callback_latest_post).await;
            let _ = ApiGet::PostsCount.fetch(callback_total_posts_count).await;
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
