use crate::icons::icons::{DownVoteIcon, UpVoteIcon};
use stylist::Style;
use yew::{function_component, html, Html, Properties};

const STYLE: &str = include_str!("styles/vote.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub up_votes: usize,
    pub down_votes: usize,
}

#[function_component(Vote)]
pub fn vote(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();

    html! {
        <div class={style}>
            <div class="votes">
                <UpVoteIcon />
                <span>{props.up_votes}</span>

                <DownVoteIcon />
                <span>{props.down_votes}</span>
            </div>
        </div>
    }
}
