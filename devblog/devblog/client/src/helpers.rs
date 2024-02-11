use crate::store::Store;
use crate::{helpers, Api};
use crate::{router::Route, User, UserField};
use gloo::console::log;
use gloo_net::http::{Headers, Method, Response};
use serde::de::DeserializeOwned;
use serde::Serialize;
use std::ops::Deref;
use std::rc::Rc;
use wasm_bindgen::JsValue;
use web_sys::{Event, HtmlInputElement, MouseEvent, SubmitEvent};
use yew::{Callback, TargetCast, UseStateHandle};
use yew_router::navigator::Navigator;
use yewdux::Dispatch;

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

pub fn on_click(
    token: String,
    api: Rc<Api>,
    method: Method,
    navigator: Option<Navigator>,
) -> Callback<MouseEvent> {
    let api = Rc::clone(&api);
    Callback::from(move |_| {
        let method = method.clone();
        let navigator = navigator.clone();
        let auth = format!("Bearer {}", token);
        let hdrs = Headers::new();
        hdrs.append("Authorization", &auth);
        let api = Rc::clone(&api);

        wasm_bindgen_futures::spawn_local(async move {
            if let Some(res) = api.fetch(Some(hdrs), None, method).await {
                if res.status() == 200 {
                    navigator.map(|nav| nav.push(&Route::Home));
                }
            }
        });
    })
}

pub fn on_submit(
    user: &UseStateHandle<User>,
    nav: Navigator,
    api: Rc<Api>,
    dispatch: Dispatch<Store>,
) -> Callback<SubmitEvent> {
    let user = user.clone();
    let api = Rc::clone(&api);
    Callback::from(move |e: SubmitEvent| {
        e.prevent_default();
        let dispatch_clone = dispatch.clone();
        let nav = nav.clone();
        let mut user = user.deref().clone();
        user.subscribed = true;
        let hdrs = Headers::new();
        hdrs.append("content-type", "application/json");
        let api = Rc::clone(&api);
        wasm_bindgen_futures::spawn_local(async move {
            let body = Some(helpers::to_jsvalue(user));

            // navigate home if the submission is successful
            if let Some(res) = api.fetch(Some(hdrs), body, Method::POST).await {
                if res.status() == 200 {
                    let obj: Store = serde_json::from_str(&res.text().await.unwrap()).unwrap();
                    dispatch_clone.reduce_mut(move |store| {
                        store.token = obj.token;
                        store.username = obj.username;
                    });
                    nav.push(&Route::Home);
                }
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

pub struct CustomCallback;

impl CustomCallback {
    pub fn new<T: 'static>(state: &UseStateHandle<T>) -> Callback<T> {
        let state = state.clone();
        Callback::from(move |req: T| {
            state.set(req);
        })
    }
}

pub async fn emit<T>(callback: &Callback<T>, response: Response)
where
    T: DeserializeOwned,
{
    let txt = response.text().await.unwrap();
    log!("Response: ", &txt);
    callback.emit(serde_json::from_str::<T>(&txt).unwrap());
}
