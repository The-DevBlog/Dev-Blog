// use gloo::console::log;
use yew::prelude::*;

use crate::{components::post::Post, Api, CustomCallback, PostModel};

#[function_component(Home)]
pub fn home() -> Html {
    let latest_post = use_state(|| PostModel::default());
    let callback = CustomCallback::new(&latest_post);

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            Api::GetPost(-1).call(callback).await;
        });

        || {} // cleanup
    });

    html! {
        <section>
            <h1>{"Home Page"}</h1>
            <Post post={(*latest_post).clone()}/>
        </section>
    }
}
