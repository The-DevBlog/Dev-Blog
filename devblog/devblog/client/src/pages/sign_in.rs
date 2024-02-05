use stylist::Style;
use yew::prelude::*;

const STYLE: &str = include_str!("styles/signIn.css");

#[function_component(SignIn)]
pub fn sign_in() -> Html {
    let style = Style::new(STYLE).unwrap();
    html! {
            <div class={style}>
                <div class="sign-in-container">
                    <label for="">{"username"}</label>
                    <input
                      type="text",
                      placeholder="username",
                      value="myusername"
                    />


                    // {error && <span>{error}</span>}

    // <form onSubmit={handleSubmit} className="sign-in">
    //     <TextField
    //         label="Username"
    //         value={userName}
    //         type="text"
    //         onChange={(e) => setUsername(e.currentTarget.value)}
    //         validateOnFocusOut
    //         required
    //     />

    //     <TextField
    //         label="Password"
    //         value={password}
    //         type="password"
    //         onChange={(e) => setPassword(e.currentTarget.value)}
    //         validateOnFocusOut
    //         required
    //     />

    //     <button>Login</button>
    // </form>
                </div>
            </div>
        }
}
