// use gloo::console::log;
// use serde_json::to_string_pretty;
use gloo_net::http::Request;
use serde::{de::DeserializeOwned, Serialize};
use yew::{Callback, UseStateHandle};

const URL: &str = "https://localhost:44482/api/";

pub enum Api {
    GetPost(i32),
    GetPage(u32),
    GetPostsCount,
    GetPagesCount,
    GetUsers,
}

impl Api {
    pub async fn call<T>(&self, callback: Callback<T>)
    where
        T: DeserializeOwned + Serialize,
    {
        let response = Request::get(&self.uri())
            .send()
            .await
            .unwrap()
            .json()
            .await
            .unwrap();

        // log!(to_string_pretty(&response).unwrap());
        callback.clone().emit(response);
    }

    pub fn uri(&self) -> String {
        match self {
            Api::GetPost(id) => format!("{}posts/{}", URL, id),
            Api::GetPage(num) => format!("{}posts/?page={}", URL, num),
            Api::GetPostsCount => format!("{}posts/countPosts", URL),
            Api::GetPagesCount => format!("{}posts/countPages", URL),
            Api::GetUsers => format!("{}accounts", URL),
        }
    }
}

pub struct CustomCallback;

impl CustomCallback {
    pub fn new<T: 'static>(state: &UseStateHandle<T>) -> Callback<T> {
        let state = state.clone();
        Callback::from(move |req: T| {
            state.set(req);
        })
    }
}
