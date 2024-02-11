use chrono::NaiveDateTime;
use serde::{Deserialize, Serialize};

#[derive(Clone, Serialize, Deserialize, PartialEq, Debug, Default)]
pub struct PostModel {
    pub id: u32,
    pub description: String,
    pub date: NaiveDateTime,
    pub imgs: Vec<ImgModel>,
    pub comments: Vec<CommentModel>,
    #[serde(rename = "upVotes")]
    pub up_votes: Vec<UpVoteModel>,
    #[serde(rename = "downVotes")]
    pub down_votes: Vec<DownVoteModel>,
}

#[derive(Clone, Serialize, Deserialize, PartialEq, Debug, Default)]
pub struct CommentModel {
    pub id: u32,
    #[serde(rename = "postId")]
    pub post_id: u32,
    pub content: String,
    pub date: NaiveDateTime,
    #[serde(rename = "userName")]
    pub username: String,
}

#[derive(Clone, Serialize, Deserialize, PartialEq, Debug, Default)]
pub struct ImgModel {
    #[serde(rename = "postId")]
    pub post_id: u32,
    pub url: String,
}

#[derive(Clone, Serialize, Deserialize, PartialEq, Debug, Default)]
pub struct UpVoteModel {
    #[serde(rename = "postId")]
    pub post_id: u32,
    #[serde(rename = "userName")]
    pub username: String,
}

#[derive(Clone, Serialize, Deserialize, PartialEq, Debug, Default)]
pub struct DownVoteModel {
    #[serde(rename = "postId")]
    pub post_id: u32,
    #[serde(rename = "userName")]
    pub username: String,
}

#[derive(Clone, Serialize, Deserialize, PartialEq, Debug, Default)]
pub struct User {
    #[serde(rename = "userName")]
    pub username: String,
    pub email: String,
    pub password: String,
    #[serde(rename = "passwordHash")]
    pub password_hash: String,
}

impl User {
    pub fn set(mut self, value: String, field: &UserField) -> Self {
        match field {
            UserField::Username => self.username = value,
            UserField::Email => self.email = value,
            UserField::Password => self.password = value,
            UserField::PasswordHash => self.password_hash = value,
        }
        self
    }
}

pub enum UserField {
    Username,
    Password,
    PasswordHash,
    Email,
}

#[derive(Clone, Serialize, Deserialize, PartialEq, Debug, Default)]
pub struct UserInfo {
    #[serde(rename = "userName")]
    pub username: String,
    pub subscribed: bool,
    pub email: String,
}
