use stylist::Style;
use yew::{function_component, html, Callback, Html, Properties};

use crate::icons::icons::{ArrowLeft, ArrowRight};
// use gloo::console::log;

const STYLE: &str = include_str!("styles/pager.css");

#[derive(Properties, PartialEq)]
pub struct Props {
    pub page_num: i32,
    pub total_pages: i32,
    pub on_click: Callback<i32>,
}

#[function_component(Pager)]
pub fn pager(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();

    // pass the updated page number back to the posts page
    let page = |num: i32| {
        let on_click = props.on_click.clone();
        let page_num = props.page_num.clone();
        Callback::from(move |_| {
            on_click.emit(page_num as i32 + num);
        })
    };

    html! {
        <div class={style}>
            <div class="pager">
                // PAGE LEFT
                if props.page_num > 1 {
                    <span onclick={page(-1)}><ArrowLeft /></span>
                }

                // <span>{props.page_num}</span>

                // PAGE RIGHT
                if props.page_num < props.total_pages {
                    <span onclick={page(1)}><ArrowRight /></span>
                }
            </div>
        </div>
    }
}
