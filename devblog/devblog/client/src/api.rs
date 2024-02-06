use gloo::console::log;
use gloo_net::http::{Headers, Method, Request, RequestBuilder};
use serde::{de::DeserializeOwned, Serialize};
use serde_json::Value;
use yew::{Callback, UseStateHandle};

const URL: &str = "https://localhost:44482/api/";

pub enum Api {
    GetPost(i32),
    GetPage(u32),
    GetPostsCount,
    GetPagesCount,
    GetUsers,
    SignIn,
}

impl Api {
    pub async fn call<T>(
        &self,
        callback: Callback<T>,
        hdrs: Option<Headers>,
        method: Method,
        body: Value,
    ) where
        T: DeserializeOwned + Serialize,
    {
        log!(body.to_string());
        let request: RequestBuilder = Request::get(&self.uri())
            .headers(hdrs.unwrap_or_default())
            .method(method.clone());

        if method == Method::POST {
            let response = request
                .body(body.to_string())
                .unwrap()
                .send()
                .await
                .unwrap()
                .json()
                .await
                .unwrap();
            // let response = request.send().await.unwrap().json().await.unwrap();
            callback.clone().emit(response);
        } else {
            let response = request.send().await.unwrap().json().await.unwrap();
            callback.clone().emit(response);
        }

        // let response = request.send().await.unwrap().json().await.unwrap();
        // callback.clone().emit(response);
        // log!(to_string_pretty(&response).unwrap());
    }

    pub fn uri(&self) -> String {
        match self {
            Api::GetPost(id) => format!("{}posts/{}", URL, id),
            Api::GetPage(num) => format!("{}posts/?page={}", URL, num),
            Api::GetPostsCount => format!("{}posts/countPosts", URL),
            Api::GetPagesCount => format!("{}posts/countPages", URL),
            Api::GetUsers => format!("{}accounts", URL),
            Api::SignIn => format!("{}accounts/signin", URL),
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
