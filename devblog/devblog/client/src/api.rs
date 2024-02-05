use gloo::console::log;
use gloo_net::http::Request;
use serde::{de::DeserializeOwned, Serialize};
use serde_json::to_string_pretty;
use yew::{Callback, UseStateHandle};

const URL: &str = "https://localhost:44482/api/";

pub enum Api {
    GetPost(i32),
    GetPage(u32),
    GetPostsCount,
    GetTotalPagesCount,
}

impl Api {
    pub async fn call_2<T>(&self) -> T
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

        log!(to_string_pretty(&response).unwrap());
        // state.set(response);
        response
        // callback.clone().emit(response);
    }

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

        log!(to_string_pretty(&response).unwrap());
        callback.clone().emit(response);
    }

    pub fn uri(&self) -> String {
        match self {
            Api::GetPost(id) => format!("{}posts/{}", URL, id),
            Api::GetPage(num) => format!("{}posts/?page={}", URL, num),
            Api::GetPostsCount => format!("{}posts/countPosts", URL),
            Api::GetTotalPagesCount => format!("{}posts/countPages", URL),
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
