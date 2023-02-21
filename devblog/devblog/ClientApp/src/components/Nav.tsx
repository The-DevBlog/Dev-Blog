import { Link } from "react-router-dom";
import "./Nav.css";

const Nav = () => {
    return (
        <nav className="navbar">
            <Link to="/">Home</Link>
            <Link to="/posts">Posts</Link>
            <Link to="/about">About</Link>
            <span className="accounts">
                <Link to="/login">Login</Link>
                <Link to="/signup">Sign Up</Link>
            </span>
        </nav>
    );
}

export default Nav;