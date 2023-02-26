import "./SignUp.css";
import { TextField } from "@fluentui/react/lib/TextField";
import { FormEvent, useState } from "react";
import { Hash, useNavigate } from "react-router-dom";

const SignUp = () => {
    const [userName, setUserName] = useState("");
    const [email, setEmail] = useState("");
    const [passwordHash, setPassword] = useState<Hash>("");
    const [confirmPasswordHash, setConfirmPassword] = useState<Hash>("");
    const navigate = useNavigate();

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        fetch("api/accounts", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ userName, email, passwordHash, confirmPasswordHash })
        }).then(() => {
            navigate("/");
        });
    }

    return (
        <div className="sign-up">
            <form onSubmit={handleSubmit}>
                <TextField
                    label="UserName"
                    value={userName}
                    type="text"
                    onChange={(e) => setUserName(e.currentTarget.value)}
                    validateOnFocusOut
                    validateOnLoad={false}
                    required
                />

                <TextField
                    label="Email"
                    value={email}
                    type="text"
                    onChange={(e) => setEmail(e.currentTarget.value)}
                    validateOnFocusOut
                    required
                />

                <TextField
                    label="Password"
                    value={passwordHash}
                    type="password"
                    onChange={(e) => setPassword(e.currentTarget.value)}
                    validateOnFocusOut
                    canRevealPassword
                    required
                />

                <TextField
                    label="Confirm Password"
                    value={confirmPasswordHash}
                    type="password"
                    onChange={(e) => setConfirmPassword(e.currentTarget.value)}
                    validateOnFocusOut
                    canRevealPassword
                    required
                />

                <button>Sign Up</button>
            </form>
        </div>
    )
}

export default SignUp;