use crate::{components::post::Post, Api, CustomCallback, PostModel};
use stylist::Style;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/home.css");

#[function_component(Home)]
pub fn home() -> Html {
    let style = Style::new(STYLE).unwrap();
    let latest_post = use_state(|| PostModel::default());
    let callback = CustomCallback::new(&latest_post);

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            Api::GetPost(-1).call(callback).await;
        });

        || {} // cleanup
    });

    html! {
        <section class={style}>
            <div class="home">
                <Post post={(*latest_post).clone()}/>
            </div>
        </section>
    }
}
