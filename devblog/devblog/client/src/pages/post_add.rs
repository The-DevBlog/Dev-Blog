use std::{fs::File, ops::Deref};

use crate::{helpers, store::Store, Api};
use gloo_net::http::Method;
// use gloo::file::File;
use stylist::{css, Style};
use web_sys::HtmlTextAreaElement;
use yew::prelude::*;
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/postAdd.css");

#[function_component(AddPost)]
pub fn add_post() -> Html {
    let style = Style::new(STYLE).unwrap();
    let description = use_state(|| String::default());
    let char_count = use_state(|| 0);
    // let files = use_state(|| vec![File::open("Hello.txt").unwrap()]);
    let loading = use_state(|| false);
    let devblog = use_state(|| false);
    let devblog_status = use_state(|| 0);
    let devblog_err = use_state(|| String::default());
    let mastodon = use_state(|| false);
    let mastodon_status = use_state(|| 0);
    let mastodon_err = use_state(|| String::default());
    let discord = use_state(|| false);
    let discord_status = use_state(|| 0);
    let discord_err = use_state(|| String::default());
    let store = use_store_value::<Store>();

    let char_count_clone = char_count.clone();
    let description_clone = description.clone();
    use_effect_with(description_clone.clone(), move |_| {
        char_count_clone.set(description_clone.len());
    });

    let onsubmit = {
        let token = store.token.clone();
        let discord = *discord.clone();
        let mastodon = *mastodon.clone();
        let devblog = *devblog.clone();
        Callback::from(move |e: SubmitEvent| {
            e.prevent_default();
            loading.set(true);
            let hdrs = helpers::create_auth_header(&token);

            wasm_bindgen_futures::spawn_local(async move {
                let response = Api::AddPost.fetch(Some(hdrs), None, Method::POST).await;

                if let Some(res) = response {
                    if discord {}
                    if mastodon {}
                    if devblog {}
                };
            });
        })
    };

    let upload_status = |platform: String, upload: bool, status: i32, err: String| -> Html {
        html! {
            <h4 class={match upload {true => css!("color: green;"), false => css!("color: red;")}}>{platform}{" Upload Status: "}
                <span class={if (status).to_string().starts_with('2') {css!("color: green;")} else {css!("color: red;")}}>
                    {status}
                </span>
                if status != 200 {
                    <p>{err}</p>
                }
            </h4>
        }
    };

    let upload_to = |platform: String, upload: bool, callback: Callback<Event>| -> Html {
        html! {
          <li>
              <label>
                  <input
                  type="checkbox"
                  checked={upload}
                  onchange={callback}/>
                  {platform}
              </label>
          </li>
        }
    };

    let update_checkbox = {
        Callback::from(move |e| {
            // let input = e.target_dyn_into::<HtmlElement>().unwrap();
        })
    };

    let update_char_count = {
        let description = description.clone();
        Callback::from(move |e: InputEvent| {
            let input = e.target_dyn_into::<HtmlTextAreaElement>();
            description.set(input.unwrap().value());
        })
    };

    html! {
        if store.admin {
            <section class={style}>
                <div class="create-post">
                    <form {onsubmit}>
                        <div class="upload-platforms">

                            // PLATFORM UPLOAD OPTIONS
                            <p>{"Upload to:"}</p>
                            <ul>
                                {upload_to("Discord".to_string(), *discord, update_checkbox.clone())}
                                {upload_to("Mastodon".to_string(), *mastodon, update_checkbox.clone())}
                                {upload_to("DevBlog".to_string(), *devblog, update_checkbox)}
                            </ul>

                            <span class="loader">{"Loading..."}</span>

                            // UPLOAD STATUSES
                            <div class="upload-status">
                                {upload_status("Discord".to_string(), *discord.clone(), *discord_status, discord_err.deref().clone())}
                                {upload_status("Mastodon".to_string(), *mastodon.clone(), *mastodon_status, mastodon_err.deref().clone())}
                                {upload_status("DevBlog".to_string(), *devblog.clone(), *devblog_status, devblog_err.deref().clone())}
                            </div>
                        </div>

                        // NEW POST INFO
                        <div class="new-post-info">
                            <label>{"Image"}
                                <input type="file"
                                    required=true
                                    multiple=true/>
                            </label>

                            <label>{"Description"}
                                <p>{"Mastodon character limit: "}{*char_count}{"/500"}</p>
                                <p>{"Discord character limit: "}{*char_count}{"/2000"}</p>
                                <div class="addpost-description">
                                    <textarea oninput={update_char_count} placeholder="Write description here..."/>
                                </div>
                            </label>

                            <p>{"Preview:"}</p>
                            <div class="post-preview">
                                <span>{"preview content:"}</span>
                            </div>
                            <button>{"Create Post"}</button>
                        </div>
                    </form>
                </div>
            </section>
        }
    }
}
