import { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import SignOut from "../pages/SignOut";
import { MdMenu, MdNotifications } from "react-icons/md";
import { IsLoggedIn } from "../components/AuthenticationService";
import { GetIsAdmin } from "./AuthenticationService";
import "./styles/Nav.css";

const Nav = () => {
    const [loggedIn, setLoggedIn] = useState(false);
    const [isMenuClicked, setIsMenuClicked] = useState(false)
    const [display, setDisplay] = useState("none")
    const [isAdmin, setIsAdmin] = useState(false);
    const location = useLocation();

    useEffect(() => setIsAdmin(GetIsAdmin), []);

    const isActive = (path: string) => {
        return location.pathname === path ? "active" : "non-active";
    };

    const updateMenu = () => {
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
    }, []);

    return (
        <>
            <nav className="navbar">
                <Link className="logo-link" to="/">
                    <div className="logo">
                        <span>The DevBlog</span>
                    </div>
                </Link>


                <div className="notifications">
                    <MdNotifications />
                </div>
                <div className="nav-drop-down">
                    <MdMenu className="nav-icon" onClick={updateMenu} />

                    <div className="nav-links" style={{ display: display }}>
                        <Link to="/" className={isActive("/")}>Home</Link>
                        <Link to="/posts" className={isActive("/posts")}>Posts</Link>
                        <Link to="/about" className={isActive("/about")}>About</Link>
                        {isAdmin && <Link to="/insights" className={isActive("/insights")}>Insights</Link>}

                        {loggedIn && <SignOut />}

                        {!loggedIn &&
                            <span className="nav-accounts">
                                <Link to="/signin" className={isActive("/signin")}>Login</Link>
                                <Link to="/signup" className={isActive("/signup")}>SignUp</Link>
                            </span>
                        }
                    </div>
                </div>
            </nav >
        </>
    );
}

export default Nav;