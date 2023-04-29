import "./SignUp.css";
import { TextField } from "@fluentui/react/lib/TextField";
import { FormEvent, useState } from "react";
import { Hash, useNavigate } from "react-router-dom";
import IToken from "../interfaces/IToken";
import jwtDecode from "jwt-decode";

const SignUp = () => {
    const [userName, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [passwordHash, setPassword] = useState<Hash>("");
    const [confirmPasswordHash, setConfirmPassword] = useState<Hash>("");
    const [userExists, setUserExists] = useState<boolean>();
    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        // check if username already exists
        await fetch(`/api/accounts/${userName}`, {
            method: "GET",
            headers: { "Content-Type": "application/json" },
        }).then(async (res) => {
            const data = await res.json();
            setUserExists(data);
            console.log(data);
        });

        // proceed to create user only if username is unique
        if (!userExists) {
            await fetch("api/accounts", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ userName: userName, email, passwordHash, confirmPasswordHash })
            }).then(async (res) => {
                const data = await res.json();
                const decodedToken: IToken = jwtDecode(data.token);

                localStorage.setItem("token", data.token)
                localStorage.setItem("userName", decodedToken.userName);
                localStorage.setItem("email", decodedToken.email);

                navigate("/");
                window.location.reload();
            }).catch(e => {
                console.log("Error Creating Account: ", e);
            });
        }
    }

    return (
        <>
            <div className="sign-up-container">
                <form onSubmit={handleSubmit}>
                    {userExists && <span>Username already exists.</span>}
                    <TextField
                        label="UserName"
                        value={userName}
                        type="text"
                        onChange={(e) => setUsername(e.currentTarget.value)}
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
        </>
    )
}

export default SignUp;