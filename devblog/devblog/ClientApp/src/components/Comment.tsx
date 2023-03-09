import "./Comment.css";
import IComment from "../interfaces/IComment";
import { GetUserName, IsAdmin } from "../components/AuthenticationService";
import DeleteComment from "./DeleteComment";
import { useEffect, useState } from "react";

const Comment = (props: IComment) => {
    const [userName, setUserName] = useState("");
    const [isAdmin, setIsAdmin] = useState(false);

    useEffect(() => {
        setUserName(GetUserName);
        // console.log("from props: " + props.userName);
        // console.log("from token: " + userName);
        setIsAdmin(IsAdmin);
    }, []);

    return (
        <div className="comment">
            <div className="comment-info">

                {userName === props.userName || isAdmin &&
                    < DeleteComment id={props.id} />
                }

                <span>{props.userName}</span>
                <span>{props.date}</span>
            </div>
            <p>{props.content}</p>
        </div>
    );
};

export default Comment;