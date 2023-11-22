import { FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import "./styles/SignOut.css";

const SignOut = () => {
    const navigate = useNavigate();

    const handleSignOut = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        fetch(`api/accounts/signout`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
        }).then(() => {
            localStorage.clear();
            navigate("/");
            window.location.reload();
        });
    };

    return (
        <form className="logout-form" onSubmit={handleSignOut}>
            <button className="logout">Logout</button>
        </form>
    );
};

export default SignOut;