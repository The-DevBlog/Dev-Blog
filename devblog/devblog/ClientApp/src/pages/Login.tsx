import "./Login.css";
import { TextField } from "@fluentui/react/lib/TextField";
import { FormEvent, useState } from "react";

const Login = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        fetch(`api/accounts/signin`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        }).then(async (res) => {
            console.log(res.body);
            const data = await res.json();
            localStorage.setItem("token", data.token)
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