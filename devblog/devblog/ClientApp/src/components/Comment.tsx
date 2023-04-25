import "./Comment.css";
import ICommentProps from "../interfaces/ICommentProps";
import { GetUserName, GetIsAdmin } from "../components/AuthenticationService";
import DeleteComment from "./DeleteComment";
import { useEffect, useState } from "react";
import EditComment from "./EditComment";

const Comment = (props: ICommentProps) => {
    const [userName, setUserName] = useState("");
    const [isAdmin, setIsAdmin] = useState<boolean>();
    const [date, setDate] = useState<string>();
    const [isEditing, setIsEditing] = useState(false);

    useEffect(() => {
        setUserName(GetUserName);
        setIsAdmin(GetIsAdmin);

        // convert UTC time to user local time
        let newDate = new Date(props.date);
        let newDate2 = new Date(newDate.getTime() - newDate.getTimezoneOffset() * 60000)

        const formattedDate = newDate2?.toLocaleString('en-US', {
            month: '2-digit',
            day: '2-digit',
            year: 'numeric',
            hour: 'numeric',
            minute: 'numeric'
        });

        setDate(formattedDate.toLocaleString())
    }, []);

    return (
        <div className="comment">
            <div className="comment-info">
                <span>{props.userName}</span>
                {(userName === props.userName || isAdmin) && <DeleteComment id={props.id} onCommentDelete={props.handleCommentChange} />}

                {(!isEditing && userName === props.userName) &&
                    <EditComment {...props}
                        handleCommentEdit={props.handleCommentChange}
                        isEditing={isEditing} setIsEditing={setIsEditing} />
                }
                <span>{date}</span>

            </div>
            {(isEditing && userName === props.userName) &&
                <EditComment {...props}
                    handleCommentEdit={props.handleCommentChange}
                    isEditing={isEditing} setIsEditing={setIsEditing} />
            }

            {!isEditing && <p>{props.content}</p>}
        </div>
    );
};

export default Comment;
