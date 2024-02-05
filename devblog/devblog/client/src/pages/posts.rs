use crate::{components::post::Post, Api, CustomCallback, PostModel};
use stylist::Style;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/posts.css");

#[function_component(Posts)]
pub fn posts() -> Html {
    let style = Style::new(STYLE).unwrap();
    let posts = use_state(|| vec![PostModel::default()]);
    let total_posts_count = use_state(|| u32::default());
    let callback_posts = CustomCallback::new(&posts);
    let callback_total_posts_count = CustomCallback::new(&total_posts_count);

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            Api::GetPage(1).call(callback_posts).await;
            Api::GetPostsCount.call(callback_total_posts_count).await;
        });

        || {} // on destroy
    });

    html! {
        <section class={style}>
            <div class="posts">
                    {for posts.iter().map(|post| html! {<Post post={post.clone()}/>})}
            </div>
        </section>
    }
}
