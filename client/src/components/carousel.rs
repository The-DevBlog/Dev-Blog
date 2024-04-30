use crate::ImgModel;
use stylist::Style;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/carousel.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub imgs: Vec<ImgModel>,
}

#[function_component(Carousel)]
pub fn carousel(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();
    let idx: UseStateHandle<i32> = use_state(|| 0);
    let count = props.imgs.len();
    let img = &props.imgs[*idx as usize];

    let next = |num: i32| {
        let idx = idx.clone();
        Callback::from(move |_| {
            idx.set(*idx + num);
        })
    };

    html! {
        <div class={style}>
            <div class="img-container">
                if count > 1 && *idx > 0 {
                    <button class="active-btn" onclick={next(-1)}>{"<"}</button>
                } else if count > 1 {
                    <button class="inactive-btn">{"<"}</button>
                }

                <img src={img.url.clone()}/>

                if count > 1 && (*idx as usize) < count - 1 {
                    <button class="active-btn" onclick={next(1)}>{">"}</button>
                } else if count > 1 {
                    <button class="inactive-btn">{">"}</button>
                }
            </div>
        </div>
    }
}
