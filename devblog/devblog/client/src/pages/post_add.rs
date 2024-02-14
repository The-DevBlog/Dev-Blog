use stylist::Style;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/signUp.css");

#[function_component(AddPost)]
pub fn add_post() -> Html {
    let style = Style::new(STYLE).unwrap();

    html! {
        <section class={style}>
            <div class="create-post">
                <h1>{"Add Post Page"}</h1>
            </div>
        </section>
    }
}
