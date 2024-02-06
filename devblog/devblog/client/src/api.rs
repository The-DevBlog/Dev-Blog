use gloo::{console::log, utils::format::JsValueSerdeExt};
use gloo_net::http::{Headers, Method, Request};
use serde::{de::DeserializeOwned, Serialize};
use serde_json::Value;
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
}

impl Api {
    pub async fn call<T>(
        &self,
        callback: Callback<T>,
        hdrs: Option<Headers>,
        method: Method,
        body: Option<T>,
        // body: Value,
    ) where
        T: DeserializeOwned + Serialize,
    {
        // let mut parsed_body = JsValue::default();
        let mut parsed_body = String::default();
        if let Some(obj) = body {
            // parsed_body = <JsValue as JsValueSerdeExt>::from_serde(&obj).unwrap();
            parsed_body = serde_json::to_string(&obj).unwrap();
        } else {
            parsed_body = serde_json::to_string(&String::default()).unwrap();
        }

        log!("body: ", parsed_body.clone());

        // log!(body.to_string());
        // let request: RequestBuilder = Request::get(&self.uri())
        //     .headers(hdrs.unwrap_or_default())
        //     .method(method.clone());

        // if method == Method::POST {
        //     let response = request
        //         .body(body.to_string())
        //         .unwrap()
        //         .send()
        //         .await
        //         .unwrap()
        //         .json()
        //         .await
        //         .unwrap();
        //     callback.clone().emit(response);
        // } else {
        //     let response = request.send().await.unwrap().json().await.unwrap();
        //     callback.clone().emit(response);
        // }
        // log!(to_string_pretty(&response).unwrap());

        // let mut parsed_body = JsValue::default();
        // if !body.to_string().is_empty() {
        //     log!("NOT EMPTY");
        //     parsed_body = JsValue::from_str(&body.to_string());
        // } else {
        //     log!("EMPTY");
        // }

        let response = Request::get(&self.uri())
            .headers(hdrs.unwrap_or_default())
            .method(method.clone())
            .body(parsed_body)
            .unwrap()
            .send()
            .await
            .unwrap()
            .json()
            .await
            .unwrap();

        // Emit the callback
        callback.clone().emit(response);
        // }
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
