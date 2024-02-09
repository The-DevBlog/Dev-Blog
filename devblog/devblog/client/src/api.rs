use chrono::NaiveDate;
use gloo::console::log;
use gloo_net::{
    http::{Headers, Method, Request, Response},
    Error,
};
use serde::{de::DeserializeOwned, Deserialize, Serialize};
use serde_json::to_string_pretty;
// use serde_json::to_string_pretty;
use wasm_bindgen::JsValue;
use yew::{Callback, UseStateHandle};

const URL: &str = "https://localhost:44482/api/";

#[derive(Clone, Copy)]
pub enum ApiPost {
    SignIn,
    SignUp,
}

pub enum ApiGet {
    Page(u32),
    Post(i32),
    PostsCount,
    PagesCount,
    Users,
}

#[derive(Deserialize, Serialize)]
struct Token {
    pub token: String,
    pub expiration: String,
}

impl ApiPost {
    pub async fn fetch<T>(&self, hdrs: Option<Headers>, body: T) -> Result<Response, Error>
    where
        T: DeserializeOwned + Serialize + Clone,
    {
        log!("HELLO");
        let parsed = serde_json::to_string(&body).unwrap();
        let parsed_body = JsValue::from_str(&parsed);

        // log!("request body: ", parsed_body.clone());
        let request = Request::get(&self.uri())
            .headers(hdrs.unwrap_or_default())
            .method(Method::POST)
            .body(parsed_body)
            .unwrap();

        let result = request.send().await;

        match &result {
            Ok(_res) => {
                // log!("Successfully sent request");
                // let json: Token = _res.json().await.unwrap();
                // let str_json = to_string_pretty(&json).unwrap();
                log!("HELLO");
                // log!("body: ", str_json);
                // log!("Response: ", _res.body());
            }
            Err(e) => log!("Error sending request: ", e.to_string()),
        }

        result
    }

    pub fn uri(&self) -> String {
        match self {
            ApiPost::SignIn => format!("{}accounts/signin", URL),
            ApiPost::SignUp => format!("{}accounts/signup", URL),
        }
    }
}

impl ApiGet {
    pub async fn fetch<T>(&self, callback: Callback<T>) -> Result<T, Error>
    where
        T: DeserializeOwned + Serialize + Clone,
    {
        let request = Request::get(&self.uri());
        let result: Result<T, Error> = request.send().await.unwrap().json().await;

        // log!("response body: ", to_string_pretty(&result).unwrap());

        match &result {
            Ok(val) => callback.clone().emit(val.clone()),
            Err(e) => log!("Error getting response body: ", e.to_string()),
        }

        result
    }

    pub fn uri(&self) -> String {
        match self {
            ApiGet::Page(num) => format!("{}posts/?page={}", URL, num),
            ApiGet::Post(id) => format!("{}posts/{}", URL, id),
            ApiGet::PostsCount => format!("{}posts/countPosts", URL),
            ApiGet::PagesCount => format!("{}posts/countPages", URL),
            ApiGet::Users => format!("{}accounts", URL),
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
