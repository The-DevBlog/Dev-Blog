import { FormEvent } from "react";
import "./SignOut.css";

const SignOut = () => {
    const handleSignOut = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        fetch(`api/accounts/signout`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
        }).then(() => {
            localStorage.clear();
            window.location.reload();
        });
    };

    return (
        <form onSubmit={handleSignOut}>
            <button className="sign-out">Sign Out</button>
        </form>
    );
};

export default SignOut;