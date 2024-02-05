use crate::{
    components::{comment::Comment, vote::Vote},
    PostModel,
};
use chrono::{Local, TimeZone};
use stylist::Style;
use yew::{function_component, html, Html, Properties};

const STYLE: &str = include_str!("styles/post.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub post: PostModel,
    pub post_number: i32,
}

#[function_component(Post)]
pub fn post(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();

    html! {
        <div class={style}>
            <div class="post" id={format!("post{}", props.post.id)}>
                // POST INFO
                <div class="post-info">
                    <span>{"Log "}{props.post_number}</span>
                    <span>{Local.from_local_datetime(&props.post.date).unwrap().to_string()}</span>
                </div>

                // IMAGES
                <div>
                    {for props.post.imgs.iter().map(|img| {
                        html! {<img src={img.url.clone()} alt={"dev pic"}/>}
                    })}
                </div>

                // LIKE / DISLIKE
                <Vote up_votes={props.post.upVotes.len()} down_votes={props.post.downVotes.len()} />

                // DESCRIPTION
                <div>{&props.post.description}</div>

                // COMMENTS
                <div>
                    {for props.post.comments.iter().map(|comment| {
                        html! {<Comment comment={comment.clone()} />}
                    })}
                </div>
            </div>
        </div>
    }
}
