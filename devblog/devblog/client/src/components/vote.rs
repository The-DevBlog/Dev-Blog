use crate::{
    helpers,
    icons::icons::{DownVoteIcon, UpVoteIcon},
    store::Store,
    Api,
};
use gloo::console::log;
use gloo_net::http::Method;
use serde::{Deserialize, Serialize};
use serde_json::to_string_pretty;
use stylist::Style;
use web_sys::MouseEvent;
use yew::{function_component, html, use_state, Callback, Html, Properties};
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/vote.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub up_votes: usize,
    pub down_votes: usize,
    pub post_id: u32,
}

#[derive(Serialize, Deserialize)]
pub struct Votes {
    pub upVotes: u32,
    pub downVotes: u32,
}

#[function_component(Vote)]
pub fn vote(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();
    let store = use_store_value::<Store>();
    let down_votes = use_state(|| props.down_votes);
    let up_votes = use_state(|| props.up_votes);

    let onclick = {
        let id = props.post_id.clone();
        let up_votes = up_votes.clone();
        let down_votes = down_votes.clone();
        Callback::from(move |vote: String| {
            let hdrs = helpers::create_auth_header(&store.token);
            let vote = vote.clone();
            let up_votes = up_votes.clone();
            let down_votes = down_votes.clone();
            wasm_bindgen_futures::spawn_local(async move {
                let response = Api::Vote(id, vote.clone())
                    .fetch(Some(hdrs), None, Method::POST)
                    .await;

                if let Some(res) = response {
                    let votes = res.text().await.unwrap();
                }
            });
        })
    };

    html! {
        <div class={style}>
            <div class="votes">
                <UpVoteIcon vote_onclick={onclick.clone()}/>
                // <span>{props.up_votes}</span>
                <span>{*up_votes}</span>

                <DownVoteIcon vote_onclick={onclick}/>
                // <span>{props.down_votes}</span>
                <span>{*down_votes}</span>
            </div>
        </div>
    }
}
