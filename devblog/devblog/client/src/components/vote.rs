use crate::{
    helpers,
    icons::icons::{DownVoteIcon, UpVoteIcon},
    store::Store,
    Api,
};
use gloo::console::log;
use gloo_net::http::Method;
use stylist::Style;
use yew::{function_component, html, Callback, Html, Properties};
use yewdux::use_store_value;

const STYLE: &str = include_str!("styles/vote.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub up_votes: usize,
    pub down_votes: usize,
    pub post_id: u32,
}

#[function_component(Vote)]
pub fn vote(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();
    let store = use_store_value::<Store>();

    let onclick = {
        let id = props.post_id.clone();
        Callback::from(move |vote: String| {
            let hdrs = helpers::create_auth_header(&store.token);
            wasm_bindgen_futures::spawn_local(async move {
                let _res = Api::Vote(id, vote)
                    .fetch(Some(hdrs), None, Method::POST)
                    .await;
            });
        })
    };

    html! {
        <div class={style}>
            <div class="votes">
                <UpVoteIcon vote_onclick={onclick.clone()}/>
                <span>{props.up_votes}</span>

                <DownVoteIcon vote_onclick={onclick}/>
                <span>{props.down_votes}</span>
            </div>
        </div>
    }
}
