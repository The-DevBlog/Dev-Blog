use chrono::{Local, TimeZone};
use yew::{function_component, html, Html, Properties};

use crate::CommentModel;

#[derive(Properties, PartialEq)]
pub struct Props {
    pub comment: CommentModel,
}

#[function_component(Comment)]
pub fn comment(props: &Props) -> Html {
    html! {
        <div class="comment">
            <div class="comment-info">
                // USERNAME
                <span>{&props.comment.userName}</span>

                // DATE / TIME
                <div class={"date"}>
                    <span>{Local.from_local_datetime(&props.comment.date).unwrap().to_string()}</span>
                </div>

                // CONTENT
                <p>{&props.comment.content}</p>
            </div>
        </div>
    }
}
