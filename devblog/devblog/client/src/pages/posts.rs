use crate::PostModel;
use gloo::console::log;
use gloo_net::http::Request;
use yew::prelude::*;

#[function_component(Posts)]
pub fn posts() -> Html {
    // let first_load = use_state(|| true);

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            // if *first_load {
            log!("FETCHING POSTS");
            // first_load.set(false);

            let request: Vec<PostModel> = Request::get("https://localhost:44482/api/posts/page/1")
                .send()
                .await
                .unwrap()
                .json()
                .await
                .unwrap();

            let json = serde_json::to_string_pretty(&request).unwrap_or_default();
            log!("body:", json.to_string());
            // }
        });

        || {} // on destroy
    });

    html! {
        <section>
            <h1>{"Posts Page"}</h1>
        </section>
    }
}
