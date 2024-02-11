use gloo::console::log;
use gloo_net::http::{Headers, Method, RequestBuilder, Response};
use wasm_bindgen::JsValue;

const URL: &str = "https://localhost:44482/api/";

#[derive(Clone, Copy, PartialEq)]
pub enum Api {
    GetPage(u32),
    GetPost(i32),
    GetPostsCount,
    GetPagesCount,
    GetUsers,
    GetCurrentUser,
    SignIn,
    SignUp,
}

impl Api {
    pub async fn fetch(
        &self,
        hdrs: Option<Headers>,
        body: Option<JsValue>,
        method: Method,
    ) -> Option<Response> {
        let request_builder = RequestBuilder::new(&self.uri())
            .headers(hdrs.unwrap_or_default())
            .method(method)
            .body(body.unwrap_or_default());

        match request_builder {
            Ok(req) => {
                let req_result = req.send().await;
                match req_result {
                    Ok(response) => Some(response),
                    Err(e) => {
                        log!("Error sending request: ", e.to_string());
                        None
                    }
                }
            }
            Err(e) => {
                log!("Error building request: ", e.to_string());
                None
            }
        }
    }

    fn uri(&self) -> String {
        match self {
            Api::GetPage(num) => format!("{}posts/?page={}", URL, num),
            Api::GetPost(id) => format!("{}posts/{}", URL, id),
            Api::GetPostsCount => format!("{}posts/countPosts", URL),
            Api::GetPagesCount => format!("{}posts/countPages", URL),
            Api::GetUsers => format!("{}accounts", URL),
            Api::GetCurrentUser => format!("{}accounts/user", URL),
            Api::SignIn => format!("{}accounts/signin", URL),
            Api::SignUp => format!("{}accounts/signup", URL),
        }
    }
}
