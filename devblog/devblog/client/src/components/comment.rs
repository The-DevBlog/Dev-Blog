use std::ops::Deref;

use crate::{
    components::{comment_delete::DeleteComment, comment_edit::EditComment},
    store::Store,
    CommentModel,
};
use chrono::{Local, TimeZone};
use gloo::console::log;
use stylist::Style;
use yew::{function_component, html, use_state, Callback, Html, Properties};
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/comment.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub comment: CommentModel,
    pub on_comment_delete: Callback<u32>,
}

#[function_component(Comment)]
pub fn comment(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();
    let is_editing = use_state(|| false);
    let content = use_state(|| props.comment.content.clone());
    let store = use_store_value::<Store>();
    let date = Local
        .from_utc_datetime(&props.comment.date)
        .format("%x %I:%M %p");

    let is_editing_clone = is_editing.clone();
    let on_is_editing = move |value: bool| {
        is_editing_clone.set(value);
    };

    let content_clone = content.clone();
    let on_edit_save = move |value: String| {
        log!("SAVE EDIT COMMENT");
        content_clone.set(value);
    };

    html! {
        <div class={{style}}>
            <div class="comment">
                <div class="comment-info">
                    // USERNAME
                    <span>{&props.comment.username}</span>

                    // EDIT COMMENT
                    if !*is_editing && props.comment.username == store.username {
                        <EditComment id={props.comment.id}
                            content={props.comment.content.clone()}
                            is_editing={*is_editing}
                            on_is_editing={on_is_editing.clone()}
                            on_edit_save={on_edit_save.clone()}/>
                    }

                    // DELETE COMMENT
                    if props.comment.username == store.username || store.admin{
                        <DeleteComment id={props.comment.id.clone()} on_comment_delete={&props.on_comment_delete}/>
                    }

                    // DATE / TIME
                    <div class={"date"}>
                        <span>{date.to_string()}</span>
                    </div>

                    // EDIT COMMENT
                    if *is_editing && props.comment.username == store.username {
                        <EditComment id={props.comment.id}
                            content={props.comment.content.clone()}
                            is_editing={*is_editing}
                            on_is_editing={on_is_editing}
                            on_edit_save={on_edit_save}/>
                    }

                    // CONTENT
                    <p>{content.deref()}</p>
                </div>
            </div>
        </div>
    }
}
