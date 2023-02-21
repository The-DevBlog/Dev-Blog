import "./Login.css";
import { TextField } from "@fluentui/react/lib/TextField";

const Login = () => {
    return (
        <div className="login">
            <form>
                <TextField
                    label="UserName"
                    validateOnFocusOut
                    required
                />

                <TextField
                    label="Password"
                    validateOnFocusOut
                    required
                />

                <button>Login</button>
            </form>
        </div>
    )
}

export default Login;