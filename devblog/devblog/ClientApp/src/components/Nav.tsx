import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import SignOut from "../pages/SignOut";
import { IsLoggedIn } from "../components/AuthenticationService";
import "./Nav.css";

const Nav = () => {
    const [loggedIn, setLoggedIn] = useState(false);
    const [username, setUsername] = useState("");

    useEffect(() => {
        setLoggedIn(IsLoggedIn);
        setUsername(localStorage.getItem("username")!);
    }, []);

    return (
        <nav className="navbar">
            <Link to="/">Home</Link>
            <Link to="/posts">Posts</Link>
            <Link to="/about">About</Link>


            {loggedIn ? (
                <span className="accounts">
                    <span>Welcome {username}</span>
                    <SignOut />
                </span>
            ) : (
                <span className="accounts">
                    <Link to="/signin">Login</Link>
                    <Link to="/signup">Sign Up</Link>
                </span>
            )}

        </nav>
    );
}

export default Nav;