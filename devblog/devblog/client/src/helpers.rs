use crate::{router::Route, ApiPost, User, UserField};
use gloo_net::http::Headers;
use std::ops::Deref;
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

pub fn onsubmit(
    user: &UseStateHandle<User>,
    nav: Navigator,
    api: ApiPost,
) -> Callback<SubmitEvent> {
    let user = user.clone();
    Callback::from(move |e: SubmitEvent| {
        e.prevent_default();
        let nav = nav.clone();
        let user = user.deref().clone();
        let hdrs = Headers::new();
        hdrs.append("content-type", "application/json");
        wasm_bindgen_futures::spawn_local(async move {
            let response = api.fetch(Some(hdrs), user).await;

            if let Ok(_) = response {
                nav.push(&Route::Home);
            }
        });
    })
}
