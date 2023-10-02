import ICommentProps from "../interfaces/ICommentProps";
import { GetUserName, GetIsAdmin } from "../components/AuthenticationService";
import DeleteComment from "./DeleteComment";
import { useEffect, useState } from "react";
import EditComment from "./EditComment";
import "./styles/Comment.css";

const Comment = (props: ICommentProps) => {
    const [userName, setUserName] = useState("");
    const [isAdmin, setIsAdmin] = useState<boolean>();
    const [date, setDate] = useState<string>();
    const [time, setTime] = useState<string>();
    const [isEditing, setIsEditing] = useState(false);

    useEffect(() => {
        setUserName(GetUserName);
        setIsAdmin(GetIsAdmin);

        // convert UTC time to user local time
        let utcDate = new Date(props.date);
        let localDate = new Date(utcDate.getTime() - utcDate.getTimezoneOffset() * 60000)

        const formattedDate = localDate?.toLocaleString('en-US', {
            month: '2-digit',
            day: '2-digit',
            year: 'numeric',
        });

        const formattedTime = localDate?.toLocaleString('en-US', {
            hour: 'numeric',
            minute: 'numeric'
        });

        setDate(formattedDate.toLocaleString())
        setTime(formattedTime.toLocaleString());
    }, [props.date]);

    return (
        <div className="comment">
            <div className="comment-info">
                <span>{props.userName}</span>

                {(!isEditing && userName === props.userName) &&
                    <EditComment {...props}
                        handleCommentEdit={props.handleCommentChange}
                        isEditing={isEditing} setIsEditing={setIsEditing} />
                }
                {(userName === props.userName || isAdmin) && <DeleteComment id={props.id} onCommentDelete={props.handleCommentChange} />}
                <div className="date">
                    <span>{date}</span>
                    <span>{time}</span>
                </div>

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
