use stylist::Style;
use yew::{function_component, html, Html, Properties};

#[derive(Properties, PartialEq, Clone)]
pub struct Props {
    pub content: String,
}

const STYLE: &str = include_str!("styles/markdown.css");

#[function_component(Markdown)]
pub fn markdown(props: &Props) -> Html {
    let style = Style::new(STYLE).unwrap();

    let html = props.content.lines().map(|line| match line {
        l if l.starts_with("# ") => html! {<h1>{&l[1..]}</h1>},
        l if l.starts_with("## ") => html! {<h2>{&l[2..]}</h2>},
        l if l.starts_with("### ") => html! {<h3>{&l[3..]}</h3>},
        l if l.starts_with("#### ") => html! {<h4>{&l[4..]}</h4>},
        l if l.starts_with("```") => {
            html! {<p><code lang="rust">{line.replace("```", "")}</code></p>}
        }
        l if l.starts_with("---") => html! {<hr/>},
        l if l.starts_with("___") => html! {<hr/>},
        l if l.starts_with("-") => html! {<li>{&l[1..]}</li>},
        l if l.is_empty() => html! {<br/>},
        _ => html! {<p>{line}</p>},
    });

    html! {
        <div class={style}>
            <div class="markdown">
                {for html}
            </div>
        </div>
    }
}
