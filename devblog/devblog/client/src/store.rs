#![allow(warnings)]
use chrono::NaiveDateTime;
use serde::{Deserialize, Serialize};
use yewdux::Store;

use crate::Api;

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
#[store(storage = "local")]
pub struct PostModel {
    pub id: u32,
    pub description: String,
    pub date: NaiveDateTime,
    pub imgs: Vec<ImgModel>,
    pub comments: Vec<CommentModel>,
    pub upVotes: Vec<UpVoteModel>,
    pub downVotes: Vec<DownVoteModel>,
}

impl PostModel {
    pub async fn get_posts(&self) -> Vec<PostModel> {
        let posts: Vec<PostModel> = Api::GetPost(1).call_2().await;
        posts
    }
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct CommentModel {
    pub id: u32,
    pub postId: u32,
    pub content: String,
    pub date: NaiveDateTime,
    pub userName: String,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct ImgModel {
    pub postId: u32,
    pub url: String,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct UpVoteModel {
    pub postId: u32,
    pub userName: String,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct DownVoteModel {
    pub postId: u32,
    pub userName: String,
}
