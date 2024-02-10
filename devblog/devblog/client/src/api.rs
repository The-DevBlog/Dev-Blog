use gloo::console::log;
use gloo_net::{
    http::{Headers, Method, RequestBuilder, Response},
    Error,
};
use serde::{de::DeserializeOwned, Serialize};
use serde_json::to_string_pretty;
// use serde_json::to_string_pretty;
use crate::store::Store;
use wasm_bindgen::JsValue;
use yew::{Callback, UseStateHandle};
use yewdux::prelude::*;

const URL: &str = "https://localhost:44482/api/";

#[derive(Clone, Copy, PartialEq)]
pub enum Api {
    GetPage(u32),
    GetPost(i32),
    GetPostsCount,
    GetPagesCount,
    GetUsers,
    SignIn,
    SignUp,
}

impl Api {
    pub async fn fetch<T>(
        &self,
        callback: Option<Callback<T>>,
        hdrs: Option<Headers>,
        body: Option<JsValue>,
        method: Method,
    ) -> Result<Response, Error>
    where
        T: DeserializeOwned + Serialize + Clone,
    {
        let request = RequestBuilder::new(&self.uri())
            .headers(hdrs.unwrap_or_default())
            .method(method)
            .body(body.unwrap_or_default())
            .unwrap();

        let result = request.send().await;
        match &result {
            Ok(_res) => {
                log!("Successfully sent request");
                let txt = _res.text().await.unwrap();
                // let t: T = serde_json::from_str(&txt).unwrap();
                log!("Response: ", &txt);

                if self == &Api::SignIn {
                    // let store_obj: Store = serde_json::from_str(&txt).unwrap();
                    // log!("Token: ", obj.username);
                }

                if let Some(cb) = callback {
                    cb.emit(serde_json::from_str::<T>(&txt).unwrap());
                }
            }
            Err(e) => log!("Error sending request: ", e.to_string()),
        }

        result
    }

    fn uri(&self) -> String {
        match self {
            Api::GetPage(num) => format!("{}posts/?page={}", URL, num),
            Api::GetPost(id) => format!("{}posts/{}", URL, id),
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
