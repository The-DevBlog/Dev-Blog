use crate::Post;
use gloo::console::log;
use gloo_net::http::Request;
use yew::prelude::*;

#[function_component(Posts)]
pub fn posts() -> Html {
    let first_load = use_state(|| true);

    use_effect_with((), move |_| {
        wasm_bindgen_futures::spawn_local(async move {
            if *first_load {
                log!("FETCHING POSTS");
                first_load.set(false);

                // let request: Vec<Post> = Request::get("http://127.0.0.1:8080/api/posts/-1")
                let request: Vec<Post> = Request::get("https://localhost:44482/posts/-1")
                    // .header("Access-Control-Allow-Origin", "*")
                    .send()
                    .await
                    .unwrap()
                    .json()
                    .await
                    .unwrap();

                let json = serde_json::value::to_value(request).unwrap_or_default();
                log!("body:", json.to_string());
            }
        });

        || {}
    });

    html! {
        <section>
            <h1>{"Posts Page"}</h1>
        </section>
    }
}
