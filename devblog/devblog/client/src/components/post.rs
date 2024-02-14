use crate::{
    components::{comment::Comment, comment_add::AddComment, post_delete::DeletePost, vote::Vote},
    CommentModel, PostModel,
};
use chrono::{Local, TimeZone};
use std::ops::Deref;
use stylist::Style;
use yew::{function_component, html, use_effect_with, use_state, Callback, Html, Properties};

const STYLE: &str = include_str!("styles/post.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub post: PostModel,
    pub post_number: i32,
    pub on_post_delete: Callback<u32>,
}

#[function_component(Post)]
pub fn post(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();
    let comments = use_state(|| props.post.comments.clone());

    let post_comments = props.post.comments.clone();
    let comments_clone = comments.clone();
    use_effect_with(props.post.comments.clone(), move |_| {
        comments_clone.set(post_comments.clone());
    });

    let comments_clone = comments.clone();
    let on_comment_add = {
        Callback::from(move |comment: CommentModel| {
            let mut new_comments = comments_clone.deref().clone();
            new_comments.insert(0, comment);
            comments_clone.set(new_comments);
        })
    };

    let comments_clone = comments.clone();
    let on_comment_delete = {
        Callback::from(move |id| {
            let mut new_comments = comments_clone.deref().clone();
            if let Some(idx) = new_comments.iter().position(|c| c.id == id) {
                new_comments.remove(idx);
                comments_clone.set(new_comments);
            }
        })
    };

    html! {
        <div class={style}>
            <div class="post" id={format!("post{}", props.post.id)}>
                // POST INFO
                <div class="post-info">
                    <span>{"Log "}{props.post_number}</span>
                    <DeletePost id={props.post.id} on_post_delete={&props.on_post_delete}/>
                    <span>{Local.from_utc_datetime(&props.post.date).format("%x").to_string()}</span>
                </div>

                // IMAGES
                <div>
                    {for props.post.imgs.iter().map(|img| {
                        html! {<img src={img.url.clone()} alt={"dev pic"}/>}
                    })}
                </div>

                // LIKE / DISLIKE
                <Vote up_votes={props.post.up_votes.len()} down_votes={props.post.down_votes.len()} />

                // DESCRIPTION
                <div>{&props.post.description}</div>

                // COMMENTS
                <AddComment post_id={props.post.id} on_comment_add={&on_comment_add}/>
                <div>
                    {for comments.iter().map(|comment| {
                        html! {<Comment comment={comment.clone()} on_comment_delete={&on_comment_delete} />}
                    })}
                </div>
            </div>
        </div>
    }
}
