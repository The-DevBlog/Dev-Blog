import "./Comment.css";
import ICommentProps from "../interfaces/ICommentProps";
import { GetUserName, GetIsAdmin } from "../components/AuthenticationService";
import DeleteComment from "./DeleteComment";
import { useEffect, useState } from "react";
import EditComment from "./EditComment";

const Comment = (props: ICommentProps) => {
    const [userName, setUserName] = useState("");
    const [isAdmin, setIsAdmin] = useState<boolean>();

    useEffect(() => {
        setUserName(GetUserName);
        setIsAdmin(GetIsAdmin);
    }, []);

    return (
        <div className="comment">
            <div className="comment-info">
                <span>{props.userName}</span>

                {(userName === props.userName || isAdmin) && <DeleteComment id={props.id} onCommentDelete={props.handleCommentChange} />}
                {(userName === props.userName && <EditComment {...props} handleCommentEdit={props.handleCommentChange} />)}

                <span>{props.date}</span>
            </div>

            <p>{props.content}</p>
        </div>
    );
};

export default Comment;
