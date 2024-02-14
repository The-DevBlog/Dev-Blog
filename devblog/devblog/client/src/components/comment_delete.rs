use crate::{store::Store, Api};
use gloo_net::http::{Headers, Method};
use stylist::Style;
use yew::{function_component, html, Callback, Html, Properties};
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/commentDelete.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub id: u32,
    pub on_comment_delete: Callback<u32>,
}

#[function_component(DeleteComment)]
pub fn delete_comment(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();
    let store = use_store_value::<Store>();

    let delete = {
        let id = props.id.clone();
        let callback = props.on_comment_delete.clone();
        Callback::from(move |_| {
            let auth = format!("Bearer {}", store.token.clone());
            let hdrs = Headers::new();
            hdrs.append("Authorization", &auth);
            let callback = callback.clone();

            wasm_bindgen_futures::spawn_local(async move {
                // delete comment
                let response = Api::DeleteComment(id)
                    .fetch(Some(hdrs), None, Method::DELETE)
                    .await;

                // invoke on_comment_delete callback
                if let Some(res) = response {
                    if res.status() == 200 {
                        callback.emit(id);
                    }
                };
            });
        })
    };

    html! {
        <span class={style}>
            <button class="delete-comment-btn" onclick={delete}>{"X"}</button>
        </span>
    }
}
