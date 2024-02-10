use crate::{helpers, Api};
use crate::{router::Route, User, UserField};
// use gloo::console::log;
use gloo_net::http::{Headers, Method};
use serde::Serialize;
use std::ops::Deref;
use wasm_bindgen::JsValue;
use web_sys::{Event, HtmlInputElement, SubmitEvent};
use yew::{Callback, TargetCast, UseStateHandle};
use yew_router::navigator::Navigator;

pub fn onchange(user: &UseStateHandle<User>, field: UserField) -> Callback<Event> {
    let user = user.clone();
    Callback::from(move |e: Event| {
        let user_clone = user.deref().clone();
        let input = e.target_dyn_into::<HtmlInputElement>();
        if let Some(value) = input {
            let tmp = user_clone.set(value.value(), &field);
            user.set(tmp);
        }
    })
}

pub fn onsubmit(user: &UseStateHandle<User>, nav: Navigator, api: Api) -> Callback<SubmitEvent> {
    let user = user.clone();
    Callback::from(move |e: SubmitEvent| {
        e.prevent_default();
        let nav = nav.clone();
        let user = user.deref().clone();
        let hdrs = Headers::new();
        hdrs.append("content-type", "application/json");
        wasm_bindgen_futures::spawn_local(async move {
            let body = Some(helpers::to_jsvalue(user));
            let response = api
                .fetch::<User>(None, Some(hdrs), body, Method::POST)
                .await;

            if let Ok(_) = response {
                nav.push(&Route::Home);
            }
        });
    })
}

fn to_jsvalue<T>(body: T) -> JsValue
where
    T: Serialize,
{
    let parsed = serde_json::to_string(&body).unwrap();
    let parsed_body = JsValue::from_str(&parsed);
    parsed_body
}
