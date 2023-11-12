import { useEffect, useState } from "react";
import { IsLoggedIn } from "../components/AuthenticationService";
import DeleteAccount from "./DeleteAccount";
import "./styles/Footer.css";



const Footer = () => {
    const [userName, setUsername] = useState("");
    const [loggedIn, setLoggedIn] = useState(false);

    useEffect(() => {
        setLoggedIn(IsLoggedIn);
        setUsername(localStorage.getItem("userName")!);
    }, []);

    return (
        <div className="footer">
            <DeleteAccount />
            {loggedIn &&
                <span>
                    Welcome {userName}
                </span>
            }
        </div>
    )
}

export default Footer;