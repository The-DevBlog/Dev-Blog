import { MdMenu } from "react-icons/md";
import { Link, useLocation } from "react-router-dom";
import SignOut from "../pages/SignOut";
import { useEffect, useState } from "react";
import { GetIsAdmin } from "./AuthenticationService";
import "./styles/NavLinks.css"

interface IProps {
    handleMenuClick: () => void,
    isMenuClicked: boolean,
    loggedIn: boolean,
}

const NavLinks = ({ handleMenuClick, isMenuClicked, loggedIn }: IProps) => {
    const [isAdmin, setIsAdmin] = useState(false);
    const [navDisplay, setNavDisplay] = useState("none")
    const location = useLocation();

    const isActive = (path: string) => {
        return location.pathname === path ? "active" : "non-active";
    };

    useEffect(() => {
        if (isMenuClicked) {
            setNavDisplay("flex")
        }
        else {
            setNavDisplay("none")
        }
    }, [isMenuClicked])

    useEffect(() => {
        setIsAdmin(GetIsAdmin)
    }, []);

    return (
        <div className="nav-drop-down">
            <MdMenu className="nav-icon" onClick={handleMenuClick} />

            <div className="nav-links" style={{ display: navDisplay }}>
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
    )
}

export default NavLinks;