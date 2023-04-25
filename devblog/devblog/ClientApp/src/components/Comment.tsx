import "./Comment.css";
import ICommentProps from "../interfaces/ICommentProps";
import { GetUserName, GetIsAdmin } from "../components/AuthenticationService";
import DeleteComment from "./DeleteComment";
import { useEffect, useState } from "react";

const Comment = (props: { comment: ICommentProps, handleCommentChange: () => void }) => {
    const [userName, setUserName] = useState("");
    const [isAdmin, setIsAdmin] = useState<boolean>();

    useEffect(() => {
        setUserName(GetUserName);
        setIsAdmin(GetIsAdmin);
    }, []);

    return (
        <div className="comment">
            <div className="comment-info">
                {(userName === props.comment.userName || isAdmin) && < DeleteComment id={props.comment.id} onCommentDelete={props.handleCommentChange} />}

                <span>{props.comment.userName}</span>
                <span>{props.comment.date}</span>
            </div>
            <p>{props.comment.content}</p>
        </div>
    );
};

export default Comment;