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
    #[serde(rename = "postId")]
    pub post_id: u32,
    pub content: String,
    pub date: NaiveDateTime,
    #[serde(rename = "userName")]
    pub username: String,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct ImgModel {
    #[serde(rename = "postId")]
    pub post_id: u32,
    pub url: String,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct UpVoteModel {
    #[serde(rename = "postId")]
    pub post_id: u32,
    #[serde(rename = "userName")]
    pub username: String,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct DownVoteModel {
    #[serde(rename = "postId")]
    pub post_id: u32,
    #[serde(rename = "userName")]
    pub username: String,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct User {
    #[serde(rename = "userName")]
    pub username: String,
    pub email: String,
    pub subscribed: bool,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct Login {
    pub username: String,
    pub password: String,
}

impl Login {
    pub fn set(mut self, value: String, field: &LoginField) -> Self {
        match field {
            LoginField::Username => self.username = value,
            LoginField::Password => self.password = value,
        }
        self
    }
}

#[derive(Clone, Serialize, Deserialize, PartialEq, Debug)]
pub enum LoginField {
    Username,
    Password,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct Register {
    #[serde(rename = "userName")]
    pub username: String,
    pub email: String,
    #[serde(rename = "passwordHash")]
    pub password_hash: String,
    pub confirm_password: String,
}

impl Register {
    pub fn set(mut self, value: String, field: &RegisterField) -> Self {
        match field {
            RegisterField::Username => self.username = value,
            RegisterField::Email => self.email = value,
            RegisterField::PasswordHash => self.password_hash = value,
            RegisterField::ConfirmPassword => self.confirm_password = value,
        }
        self
    }
}

pub enum RegisterField {
    Username,
    Email,
    PasswordHash,
    ConfirmPassword,
}
