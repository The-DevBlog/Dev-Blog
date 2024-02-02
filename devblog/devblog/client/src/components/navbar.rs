use crate::router::Route;
use yew::prelude::*;
use yew_router::prelude::*;

#[function_component(Navbar)]
pub fn navbar() -> Html {
    html! {
        <nav>
            <Link<Route> to={Route::Home}>{"Home"}</Link<Route>>
            <Link<Route> to={Route::Posts}>{"Posts"}</Link<Route>>
            <Link<Route> to={Route::AddPost}>{"AddPost"}</Link<Route>>
            <Link<Route> to={Route::About}>{"About"}</Link<Route>>
            <Link<Route> to={Route::Insights}>{"Insights"}</Link<Route>>
            <Link<Route> to={Route::SignIn}>{"SignIn"}</Link<Route>>
            <Link<Route> to={Route::SignOut}>{"SignOut"}</Link<Route>>
            <Link<Route> to={Route::SignUp}>{"SignUp"}</Link<Route>>
        </nav>
    }
}
