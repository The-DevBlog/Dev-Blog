import IComment from "../interfaces/IComment";
import "./Comment.css";
import DeleteComment from "./DeleteComment";

const Comment = (props: IComment) => {
    return (
        <div className="comment">
            <div className="comment-info">
                <DeleteComment id={props.id} />
                <span>{props.userName}</span>
                <span>{props.date}</span>
            </div>
            <p>{props.content}</p>
        </div>
    );
};

export default Comment;