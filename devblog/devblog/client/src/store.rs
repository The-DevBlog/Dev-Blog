use serde::{Deserialize, Serialize};
// use web_sys::js_sys::Date;
use yewdux::Store;

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct Post {
    pub id: u32,
    pub comments: Vec<Comment>,
    pub description: String,
    // pub date: Date,
    pub imgs: Vec<Imgs>,
    pub post_number: u32,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct Comment {
    pub id: u32,
    pub content: String,
    // pub date: Date,
    pub username: String,
}

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
pub struct Imgs {
    pub post_id: u32,
    pub url: String,
}
