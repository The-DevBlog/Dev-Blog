import "./Login.css";
import { TextField } from "@fluentui/react/lib/TextField";
import { FormEvent, useState } from "react";

const Login = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const signInContent = {username, password}

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        fetch(`api/accounts/signin`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body:JSON.stringify(signInContent)
        }).then((res) => {
            console.log(res.body);
        });
    }
    
    return (
        <div className="login">
            <form onSubmit={handleSubmit}>
                <TextField
                    label="Username"
                    value={username}
                    type="text"
                    onChange={(e) => setUsername(e.currentTarget.value)}
                    validateOnFocusOut
                    required
                />

                <TextField
                    label="Password"
                    value={password}
                    type="password"
                    onChange={(e) => setPassword(e.currentTarget.value)}
                    validateOnFocusOut
                    required
                />

                <button>Login</button>
            </form>
        </div>
    )
}

export default Login;