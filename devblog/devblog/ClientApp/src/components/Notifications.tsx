import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { MdNotifications } from "react-icons/md";
import INotificationProps from "../interfaces/INotificationProps";
import { GetUserName } from "./AuthenticationService";
import { HashLink } from "react-router-hash-link";
import { useLocation } from "react-router-dom";
import "./styles/Notifications.css";

interface IProps {
    setBellDisplay: Dispatch<SetStateAction<string>>,
    handleBellClick: () => void,
    bellDisplay: string,
    isBellClicked: boolean,
    isMenuClicked: boolean,
    loggedIn: boolean,
}

const Notification = ({ setBellDisplay, handleBellClick,
    bellDisplay, isBellClicked, loggedIn }: IProps) => {

    const [notifications, setNotifications] = useState<INotificationProps[]>()
    const [userName, setUsername] = useState("");
    const location = useLocation();
    const [dismissedNotifications, setDismissedNotifications] = useState<number[]>([]);


    const getNotifications = async () => {
        await fetch(`api/notifications/${userName}`, {
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            }
        }).then((res) => { return res.json(); })
            .then((data) => {
                setNotifications(data);
            })
            .catch((e) => console.log("Error retrieving notifications: " + e));
    };

    const deleteNotification = async (postId: number, userName: string) => {
        setDismissedNotifications([...dismissedNotifications, postId]);

        setTimeout(async () => {
            await fetch(`api/notifications/${postId}/${userName}`, {
                method: "DELETE",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                },
            }).catch((e) => console.log("Error deleting notification: " + e));
        }, 300);
    }

    useEffect(() => {
        if (isBellClicked) {
            setBellDisplay("flex")
        }
        else {
            setBellDisplay("none")
        }
    }, [isBellClicked, setBellDisplay, location.pathname])

    useEffect(() => {
        setUsername(GetUserName);
    }, []);

    useEffect(() => {
        if (loggedIn) {
            getNotifications();
        }
    }, [loggedIn]);

    return (
        <div className="notification-drop-down" style={{ display: (notifications?.length ?? 0) > 0 ? "inline" : "none" }}>
            <MdNotifications className="notification-icon" onClick={handleBellClick} />
            <span className="notification-count">{notifications?.length}</span>
            <div className="notifications" style={{ display: bellDisplay }} >
                {notifications?.map((n) => <>
                    <div className={`notification-item ${dismissedNotifications.includes(n.postId) ? 'dismissed' : ''}`}>
                        <HashLink smooth to={`/posts/#post${n.postId}`}><img src={n.imgUrl} alt="post thumbnail" /></HashLink>
                        <div className="notification-txt">
                            <HashLink smooth to={`/posts/#post${n.postId}`}>{n.userName} posted</HashLink>
                            <span onClick={() => deleteNotification(n.postId, n.userName)}>dismiss</span>
                        </div>
                    </div>
                </>)}
            </div>
        </div>
    )
}

export default Notification;