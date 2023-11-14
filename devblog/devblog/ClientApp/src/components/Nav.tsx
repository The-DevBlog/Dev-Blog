import { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import Notification from "./Notifications";
import { IsLoggedIn } from "../components/AuthenticationService";
import "./styles/Nav.css";
import NavLinks from "./NavLinks";

const Nav = () => {
    const [loggedIn, setLoggedIn] = useState(false);
    const [isMenuClicked, setIsMenuClicked] = useState(false)
    const [bellDisplay, setBellDisplay] = useState("none")
    const [isBellClicked, setIsBellClicked] = useState(false)
    const location = useLocation();

    // Listen for route changes and close the menu
    useEffect(() => {
        setIsMenuClicked(false);
    }, [location.pathname]);

    useEffect(() => {
        var isLoggedIn = IsLoggedIn();
        setLoggedIn(isLoggedIn);
    }, []);

    const handleMenuClick = () => {
        setIsMenuClicked(!isMenuClicked);
        setIsBellClicked(false);
        setBellDisplay('none');
    };

    const handleBellClick = () => {
        setIsBellClicked(!isBellClicked);
        setIsMenuClicked(false);
    };

    return (
        <nav className="navbar">
            <Link className="logo-link" to="/">
                <div className="logo">
                    <span>The DevBlog</span>
                </div>
            </Link>

            <div className="nav-menus-container">
                {loggedIn &&
                    <Notification
                        setIsBellClicked={setIsBellClicked}
                        bellDisplay={bellDisplay}
                        loggedIn={loggedIn}
                        setBellDisplay={setBellDisplay}
                        isBellClicked={isBellClicked}
                        isMenuClicked={isMenuClicked}
                        setIsMenuClicked={setIsMenuClicked}
                        handleBellClick={handleBellClick} />}
                <NavLinks
                    loggedIn={loggedIn}
                    isBellClicked={isBellClicked}
                    isMenuClicked={isMenuClicked}
                    setBellDisplay={setBellDisplay}
                    setIsBellClicked={setIsBellClicked}
                    setIsMenuClicked={setIsMenuClicked}
                    handleMenuClick={handleMenuClick} />
            </div>
        </nav >
    );
}

export default Nav;