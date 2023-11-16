import { useEffect, useState } from "react";
import "./styles/Account.css"

const Account = () => {
    const [userName, setUserName] = useState("");
    const [email, setEmail] = useState("");
    const [isSubscribed, setIsSubscribed] = useState<boolean>();

    const getUserInfo = async () => {
        await fetch("api/accounts/user", {
            headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` }
        }).then((res) => { return res.json(); })
            .then((data) => {
                setUserName(data.userName);
                setEmail(data.email);
                setIsSubscribed(data.subscribed);
            }).catch((e) => console.log("Error retrieving current user: " + e));
    }

    const handleSubscribeChange = async () => {
        await fetch("api/accounts/subscribe", {
            method: "PUT",
            headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` }
        }).then((res) => { return res.json(); })
            .then((data) => {
                setIsSubscribed(data);
            }).catch((e) => console.log("Error toggling user's email preferences: " + e));
    }

    useEffect(() => {
        getUserInfo()
    }, []);

    return (
        <section className="account">
            <div>
                <h1>Account Details</h1>
                <p>Username: {userName}</p>
                <p>Email: {email}</p>
            </div>
            <div>
                <h1>Email Preferences</h1>
                <input type="checkbox"
                    checked={isSubscribed}
                    onClick={handleSubscribeChange} />
                <span>Subscribed</span>
            </div>
        </section>
    )
}

export default Account;