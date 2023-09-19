import { useEffect, useState } from "react";
import { GetUserName, IsLoggedIn } from "./AuthenticationService";

const DeleteAccount = () => {
    const [loggedIn, setLoggedIn] = useState(false);
    const [userName, setUsername] = useState("");

    useEffect(() => {
        setLoggedIn(IsLoggedIn);
        setUsername(GetUserName);
    }, []);

    const handleDelete = async () => {
        const shouldDelete = window.confirm("Are you sure?");

        if (shouldDelete) {
            await fetch(`api/accounts/${userName}`, {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                }
            }).then(async () => {
                await fetch(`api/accounts/signout`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                }).then(() => {
                    localStorage.clear();
                    window.location.reload();
                });
            })
        }
    };

    return <>{loggedIn && <button onClick={handleDelete} > Delete Account</button >}</>
}

export default DeleteAccount;