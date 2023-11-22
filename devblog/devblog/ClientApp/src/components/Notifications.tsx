import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { MdNotifications } from "react-icons/md";
import INotificationProps from "../interfaces/INotificationProps";
import { GetUserName } from "./AuthenticationService";
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
    });

    const handleClick = async (postId: number) => {
        await fetch(`api/posts/getPageNum/${postId}`)
            .then((res) => { return res.json() })
            .then((data) => {
                window.location.href = `/posts?pageNum=${data}&postId=${postId}`
            }).catch((e) => console.log("Error getting page number: " + e));
    }

    return (
        <div className="notification-drop-down" style={{ display: (notifications?.length ?? 0) > 0 ? "inline" : "none" }}>
            <MdNotifications className="notification-icon" onClick={handleBellClick} />
            <span className="notification-count">{notifications?.length}</span>
            <div className="notifications" style={{ display: bellDisplay }} >
                {notifications?.map((n) => <>
                    <div className={`notification-item ${dismissedNotifications.includes(n.postId) ? 'dismissed' : ''}`}>
                        <span onClick={() => handleClick(n.postId)}><img src={n.imgUrl} alt="post thumbnail" /></span>
                        <div className="notification-txt">
                            <span onClick={() => handleClick(n.postId)}>{n.userName} posted</span>
                            <span onClick={() => deleteNotification(n.postId, n.userName)}>dismiss</span>
                        </div>
                    </div>
                </>)}
            </div>
        </div>
    )
}

export default Notification;