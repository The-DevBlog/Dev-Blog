use crate::{helpers, store::Store, Api, CommentModel};
use gloo::console::log;
use gloo_net::http::{Headers, Method};
use std::ops::Deref;
use stylist::Style;
use web_sys::{Event, HtmlInputElement, HtmlTextAreaElement, SubmitEvent};
use yew::prelude::*;
use yewdux::prelude::*;

const STYLE: &str = include_str!("styles/addComment.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub post_id: u32,
}

#[function_component(AddComment)]
pub fn add_comment(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();
    let comment = use_state(CommentModel::default);
    let store = use_store_value::<Store>();

    let onchange = {
        let comment = comment.clone();
        Callback::from(move |e: Event| {
            let mut comment_clone = comment.deref().clone();
            let input = e.target_dyn_into::<HtmlTextAreaElement>();
            if let Some(value) = input {
                comment_clone.content = value.value();
                comment.set(comment_clone);
            }
        })
    };

    let onsubmit = {
        let comment = comment.clone();
        let post_id = props.post_id.clone();
        Callback::from(move |e: SubmitEvent| {
            e.prevent_default();
            let auth = format!("Bearer {}", store.token);
            let hdrs = Headers::new();
            hdrs.append("Authorization", &auth);
            hdrs.append("content-type", "application/json");

            let new_comment = CommentModel::new(
                0,
                post_id,
                comment.deref().content.clone(),
                store.username.clone(),
            );
            comment.set(new_comment.clone());

            let body = Some(helpers::to_jsvalue(new_comment.clone()));
            log!("Response: ", body.clone().unwrap());

            wasm_bindgen_futures::spawn_local(async move {
                let res = Api::AddComment.fetch(Some(hdrs), body, Method::POST).await;
            });
        })
    };

    html! {
        <div class={style}>
            <div class="create-comment">
                <form {onsubmit}>
                    <textarea placeholder="your comments here..."
                        type="text"
                        required={true}
                        value={comment.content.clone()}
                        onchange={onchange}>
                    </textarea>
                    <button>{"Add Comment"}</button>
                </form>
            </div>
        </div>
    }
}
