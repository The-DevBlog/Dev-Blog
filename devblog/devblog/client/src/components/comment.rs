use crate::CommentModel;
use chrono::{Local, TimeZone};
use stylist::Style;
use yew::{function_component, html, Html, Properties};

const STYLE: &str = include_str!("styles/comment.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub comment: CommentModel,
}

#[function_component(Comment)]
pub fn comment(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();

    html! {
        <div class={style}>
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
        </div>
    }
}
