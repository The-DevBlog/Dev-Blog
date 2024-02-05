use yew::{function_component, html, Html, Properties};

#[derive(Properties, PartialEq)]
pub struct Props {
    pub up_votes: usize,
    pub down_votes: usize,
}

#[function_component(Vote)]
pub fn vote(props: &Props) -> Html {
    html! {
        <div class="votes">
            <span>{"Up"}{props.up_votes}</span>
            <span>{"Down"}{props.down_votes}</span>
        </div>
    }
}
