#![allow(warnings)]
use chrono::NaiveDateTime;
use gloo::console::log;
use serde::{Deserialize, Serialize};
use serde_json::Value;
use yewdux::Store;

use crate::Api;

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct PostModel {
    pub id: u32,
    pub description: String,
    pub date: NaiveDateTime,
    pub imgs: Vec<ImgModel>,
    pub comments: Vec<CommentModel>,
    pub upVotes: Vec<UpVoteModel>,
    pub downVotes: Vec<DownVoteModel>,
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

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct User {
    pub userName: String,
    pub email: String,
    pub subscribed: bool,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct Login {
    pub username: String,
    pub password: String,
}
