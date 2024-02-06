use gloo::console::log;
use gloo_net::http::{Headers, Method, Request};
use serde::{de::DeserializeOwned, Serialize};
// use serde_json::to_string_pretty;
use wasm_bindgen::JsValue;
use yew::{Callback, UseStateHandle};

const URL: &str = "https://localhost:44482/api/";

pub enum Api {
    GetPost(i32),
    GetPage(u32),
    GetPostsCount,
    GetPagesCount,
    GetUsers,
    SignIn,
    SignUp,
}

impl Api {
    pub async fn call<T>(
        &self,
        callback: Callback<T>,
        hdrs: Option<Headers>,
        method: Method,
        body: Option<T>,
    ) where
        T: DeserializeOwned + Serialize,
    {
        let mut parsed_body = JsValue::default();
        if let Some(obj) = body {
            let parsed = serde_json::to_string(&obj).unwrap();
            parsed_body = JsValue::from_str(&parsed);
        }

        log!("request body: ", parsed_body.clone());
        let req = Request::get(&self.uri())
            .headers(hdrs.unwrap_or_default())
            .method(method)
            .body(parsed_body)
            .unwrap();

        let res: T = req.send().await.unwrap().json().await.unwrap();
        // log!("response body: ", to_string_pretty(&res).unwrap());

        callback.clone().emit(res);
    }

    pub fn uri(&self) -> String {
        match self {
            Api::GetPost(id) => format!("{}posts/{}", URL, id),
            Api::GetPage(num) => format!("{}posts/?page={}", URL, num),
            Api::GetPostsCount => format!("{}posts/countPosts", URL),
            Api::GetPagesCount => format!("{}posts/countPages", URL),
            Api::GetUsers => format!("{}accounts", URL),
            Api::SignIn => format!("{}accounts/signin", URL),
            Api::SignUp => format!("{}accounts/signup", URL),
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
