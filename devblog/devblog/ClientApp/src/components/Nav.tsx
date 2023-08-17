import { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import SignOut from "../pages/SignOut";
import { MdMenu } from "react-icons/md";
import { IsLoggedIn } from "../components/AuthenticationService";
import "./styles/Nav.css";

const Nav = () => {
    const [loggedIn, setLoggedIn] = useState(false);
    const [userName, setUsername] = useState("");
    const [isMenuClicked, setIsMenuClicked] = useState(false)
    const [display, setDisplay] = useState("none")
    const location = useLocation();

    const isActive = (path: string) => {
        return location.pathname === path ? "active" : "non-active";
    };

    const updateMenu = () => {
        console.log(isMenuClicked)
        if (!isMenuClicked) {
            setDisplay("flex")
        }
        else {
            setDisplay("none")
        }
        setIsMenuClicked(!isMenuClicked)
    }

    // Listen for route changes and close the menu
    useEffect(() => {
        setDisplay("none");
        setIsMenuClicked(false);
    }, [location.pathname]);

    useEffect(() => {
        setLoggedIn(IsLoggedIn);
        setUsername(localStorage.getItem("userName")!);
    }, []);

    return (
        <>
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
                    <Link to="/about" className={isActive("/about")}>About</Link>

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
            </nav >

            <div className="mobile-nav">
                <MdMenu className="mobile-nav-icon" onClick={updateMenu} size={75} />

                <div className="mobile-nav-links" style={{ display: display }}>
                    <Link to="/" className={isActive("/")}>Home</Link>
                    <Link to="/posts" className={isActive("/posts")}>Posts</Link>
                    <Link to="/about" className={isActive("/about")}>About</Link>
                    {loggedIn && <SignOut />}

                    {!loggedIn &&
                        <span className="mobile-nav-accounts">
                            <Link to="/signin" className={isActive("/signin")}>Login</Link>
                            <Link to="/signup" className={isActive("/signup")}>SignUp</Link>
                        </span>
                    }
                </div>
            </div>
        </>
    );
}

export default Nav;