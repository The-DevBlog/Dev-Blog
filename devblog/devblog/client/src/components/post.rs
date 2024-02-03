use chrono::{Local, TimeZone};
use yew::{function_component, html, Html, Properties};

use crate::{components::comment::Comment, PostModel};

#[derive(Properties, PartialEq)]
pub struct Props {
    pub post: PostModel,
}

#[function_component(Post)]
pub fn post(props: &Props) -> Html {
    html! {
        <div class="post" id={format!("post{}", props.post.id)}>
            // DATE
            <div class="post-info">
                <span>{"Log "}</span>
                <span>{Local.from_local_datetime(&props.post.date).unwrap().to_string()}</span>
            </div>

            // IMAGES
            <div>
                {for props.post.imgs.iter().map(|img| {
                    html! {
                        <img src={img.url.clone()} alt={"dev pic"}/>
                    }
                })}
            </div>

            // LIKE / DISLIKE

            // DESCRIPTION
            <div>{&props.post.description}</div>

            // COMMENTS
            <div>
                {for props.post.comments.iter().map(|comment| {
                    html! {
                        <Comment comment={comment.clone()} />
                    }
                })}
            </div>
        </div>
    }
}
