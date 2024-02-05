use crate::{
    components::{pager::Pager, post::Post},
    Api, CustomCallback, PostModel,
};
use stylist::Style;
use web_sys::js_sys::Promise;
use yew::prelude::*;
use yewdux::use_store;

const STYLE: &str = include_str!("styles/posts.css");

#[function_component(Posts)]
pub fn posts() -> Html {
    let style = Style::new(STYLE).unwrap();
    let posts = use_state(|| vec![PostModel::default()]);
    let total_posts_count = use_state(|| u32::default());
    let total_pages_count = use_state(|| u32::default());
    let cb_posts = CustomCallback::new(&posts);
    let cb_total_posts_count = CustomCallback::new(&total_posts_count);
    let cb_total_pages_count = CustomCallback::new(&total_pages_count);
    let (store, _dispatch) = use_store::<PostModel>();

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            // Api::GetPage(1).call_2(posts).await;
            Api::GetPage(1).call(cb_posts).await;
            Api::GetPostsCount.call(cb_total_posts_count).await;
            Api::GetTotalPagesCount.call(cb_total_pages_count).await;
        });

        || {} // on destroy
    });

    html! {
        <section class={style}>
            <div class="posts">
                <Pager />
                {for posts.iter().enumerate().map(|(idx, post)| html! {
                    <Post post={post.clone()} post_number={1}/>
                    // <Post post={post.clone()} post_number={*total_posts_count - 5 * (1) - idx as u32}/>
                })}
                <Pager />
            </div>
        </section>
    }
}
