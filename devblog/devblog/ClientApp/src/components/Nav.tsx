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
            <Link className="logo-link" to="/">
                <div className="logo">
                    <span>The</span>
                    <br />
                    <span>DevBlog</span>
                </div>
            </Link>


            <div className="nav-items">
                <Link to="/" className={isActive("/")}>Home</Link>
                <Link to="/posts" className={isActive("/posts")}>Posts</Link>

                {loggedIn ? (
                    <span className="accounts">
                        <SignOut />
                        <span>Welcome {userName}</span>
                    </span>
                ) : (
                    <span className="accounts">
                        <Link to="/signin" className={isActive("/signin")}>Login</Link>
                        <Link to="/signup" className={isActive("/signup")}>SignUp</Link>
                    </span>
                )}
            </div>

            <div className="mobile-nav">
                {loggedIn && <><span>Welcome</span><span>{userName}</span></>}
                <Link to="/" className={isActive("/")}>Home</Link>
                <Link to="/posts" className={isActive("/posts")}>Posts</Link>
                {loggedIn && <SignOut />}

                {!loggedIn &&
                    <span className="mobile-nav-accounts">
                        <Link to="/signin" className={isActive("/signin")}>Login</Link>
                        <Link to="/signup" className={isActive("/signup")}>SignUp</Link>
                    </span>
                }
            </div>
        </nav >
    );
}

export default Nav;