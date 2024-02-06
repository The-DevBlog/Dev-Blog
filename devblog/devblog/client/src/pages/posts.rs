use crate::{
    components::{pager::Pager, post::Post},
    Api, CustomCallback, PostModel,
};
use gloo_net::http::Method;
// use gloo::console::log;
use stylist::Style;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/posts.css");

#[function_component(Posts)]
pub fn posts() -> Html {
    let style = Style::new(STYLE).unwrap();
    let loading = use_state(|| true);
    let page_num = use_state(|| 1i32);
    let posts = use_state(|| vec![PostModel::default()]);
    let total_posts_count = use_state(|| i32::default());
    let total_pages_count = use_state(|| i32::default());
    let posts_cb = CustomCallback::new(&posts);
    let total_posts_count_cb = CustomCallback::new(&total_posts_count);
    let total_pages_count_cb = CustomCallback::new(&total_pages_count);

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async {
            Api::GetPostsCount
                .call(total_posts_count_cb, None, Method::GET, None)
                .await;
            Api::GetPagesCount
                .call(total_pages_count_cb, None, Method::GET, None)
                .await;
        });
    });

    let page_num_clone = page_num.clone();
    let loading_clone = loading.clone();
    use_effect_with(page_num_clone.clone(), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            loading_clone.set(false);
            Api::GetPage(*page_num_clone as u32)
                .call(posts_cb, None, Method::GET, None)
                .await;
            loading_clone.set(true);
        });
    });

    let on_pager_click = {
        let page_num = page_num.clone();
        Callback::from(move |page: i32| page_num.set(page))
    };

    html! {
        <section class={style}>
            <div class="posts">
                <Pager page_num={*page_num} on_click={&on_pager_click} total_pages={*total_pages_count}/>

                // ALL POSTS
                if *loading {
                    {for posts.iter().enumerate().map(|(idx, post)| html! {
                        <Post post={post.clone()} post_number={*total_posts_count - 5 * (*page_num as i32 - 1) - idx as i32}/>
                    })}
                } else {
                    <h1>{"loading..."}</h1>
                }

                <Pager page_num={*page_num} on_click={&on_pager_click} total_pages={*total_pages_count}/>
            </div>
        </section>
    }
}
