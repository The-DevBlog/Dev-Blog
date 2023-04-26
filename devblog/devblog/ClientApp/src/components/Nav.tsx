import { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import SignOut from "../pages/SignOut";
import { IsLoggedIn } from "../components/AuthenticationService";
import "./Nav.css";

const Nav = () => {
    const [loggedIn, setLoggedIn] = useState(false);
    const [userName, setUsername] = useState("");
    const location = useLocation();

    const isActive = (path: string) => {
        return location.pathname === path ? "active" : "non-active";
    };

    useEffect(() => {
        setLoggedIn(IsLoggedIn);
        setUsername(localStorage.getItem("userName")!);
    }, []);

    return (
        <nav className="navbar">
            <Link to="/">
                <div className="logo">
                    <span>The</span>
                    <br />
                    <span>DevBlog</span>
                </div>
            </Link>

            <Link to="/" className={isActive("/")}>Home</Link>
            <Link to="/posts" className={isActive("/posts")}>Posts</Link>
            <Link to="/about" className={isActive("/about")}>About</Link>


            {loggedIn ? (
                <span style={{ display: "flex" }} className="accounts">
                    <span>Welcome {userName}</span>
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