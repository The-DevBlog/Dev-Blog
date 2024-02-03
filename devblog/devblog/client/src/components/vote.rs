use yew::{function_component, html, use_effect_with, use_state, Callback, Html, Properties};

use crate::{Api, DownVoteModel, UpVoteModel};

#[derive(Properties, PartialEq)]
pub struct Props {
    post_id: i32,
}

#[function_component(Vote)]
pub fn vote(props: &Props) -> Html {
    // let up_votes = use_state(|| UpVoteModel::default());
    // let down_votes = use_state(|| DownVoteModel::default());

    // let callback_up_votes = {
    //     let up_votes = up_votes.clone();
    //     Callback::from(move |req: UpVoteModel| {
    //         up_votes.set(req);
    //     })
    // };

    // let callback_down_votes = {
    //     let down_votes = down_votes.clone();
    //     Callback::from(move |req: DownVoteModel| {
    //         down_votes.set(req);
    //     })
    // };

    // let props_clone = props.clone();
    // use_effect_with((), move |_| {
    //     wasm_bindgen_futures::spawn_local(async move {
    //         Api::GetVotes(props_clone.post_id.clone(), "upvotes".to_string())
    //             .call(callback_up_votes)
    //             .await;
    //     });

    //     || {} // cleanup
    // });

    // Callback::from(move |req: DownVoteModel| {
    //     down_votes.set(req);
    // });

    html! {
        <div class="votes">
            <span></span>
            <span></span>
        </div>
    }
}
