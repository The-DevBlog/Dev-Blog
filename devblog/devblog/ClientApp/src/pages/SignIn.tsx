import "./SignIn.css";
import { TextField } from "@fluentui/react/lib/TextField";
import { FormEvent, useState } from "react";
import { useNavigate } from "react-router-dom";
import jwtDecode from "jwt-decode";
import IToken from "../interfaces/IToken";

const SignIn = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        fetch(`api/accounts/signin`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        }).then(async (res) => {
            const data = await res.json();
            const decodedToken: IToken = jwtDecode(data.token);

            localStorage.setItem("token", data.token)
            localStorage.setItem("username", decodedToken.username);
            localStorage.setItem("email", decodedToken.email);

            navigate("/");
            window.location.reload();
        });
    }

    return (
        <div className="signIn">
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

export default SignIn;