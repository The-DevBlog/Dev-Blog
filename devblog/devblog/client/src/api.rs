use gloo::console::log;
use gloo_net::http::{Headers, Method, RequestBuilder, Response};
use wasm_bindgen::JsValue;

const URL: &str = "https://localhost:44482/api/";

#[derive(Clone, PartialEq)]
pub enum Api {
    AddComment,
    AddPost,
    DeleteAccount(String),
    DeleteComment(u32),
    DeleteCurrentAccount,
    DeletePost(u32),
    EditComment(u32),
    GetPage(u32),
    GetPagesCount,
    GetPost(i32),
    GetPostsCount,
    GetCurrentUser,
    GetUsers,
    SignIn,
    SignOut,
    SignUp,
    ToggleSubscribe,
    Vote(u32, String),
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
            Api::AddComment => format!("{}comments", URL),
            Api::AddPost => format!("{}posts", URL),
            Api::DeleteAccount(username) => format!("{}accounts/adminDelete/{}", URL, username),
            Api::DeleteComment(id) => format!("{}comments/{}", URL, id),
            Api::DeleteCurrentAccount => format!("{}accounts", URL),
            Api::DeletePost(id) => format!("{}posts/{}", URL, id),
            Api::EditComment(id) => format!("{}comments/{}", URL, id),
            Api::GetPage(num) => format!("{}posts/?page={}", URL, num),
            Api::GetPagesCount => format!("{}posts/countPages", URL),
            Api::GetPost(id) => format!("{}posts/{}", URL, id),
            Api::GetPostsCount => format!("{}posts/countPosts", URL),
            Api::GetCurrentUser => format!("{}accounts/user", URL),
            Api::GetUsers => format!("{}accounts", URL),
            Api::SignIn => format!("{}accounts/signin", URL),
            Api::SignOut => format!("{}accounts/signout", URL),
            Api::SignUp => format!("{}accounts/signup", URL),
            Api::ToggleSubscribe => format!("{}accounts/subscribe", URL),
            Api::Vote(post_id, vote) => format!("{}posts/{}/{}", URL, post_id, vote),
        }
    }
}
