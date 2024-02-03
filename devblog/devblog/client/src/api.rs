use gloo_net::http::Request;
use serde::de::DeserializeOwned;
use yew::{Callback, UseStateHandle};

pub enum Api {
    GetPost(i32),
    GetVotes(i32, String),
}

impl Api {
    pub async fn call<T>(&self, callback: Callback<T>)
    where
        T: DeserializeOwned,
    {
        let callback = callback.clone();
        let response = Request::get(&self.uri())
            .send()
            .await
            .unwrap()
            .json()
            .await
            .unwrap();

        callback.emit(response);
    }

    pub fn uri(&self) -> String {
        let prefix = "https://localhost:44482/api/";
        match self {
            Api::GetPost(id) => {
                format!("{}posts/{}", prefix, id)
            }
            Api::GetVotes(id, vote_type) => {
                format!("{}posts/{}/{}", prefix, id, vote_type)
            }
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
