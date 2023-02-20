import IComment from "../interfaces/IComment";
import "./Comment.css";

const Comment = (props: IComment) => {
    return (
        <div className="comment">
            <div className="comment-info">
                <span>{props.userName}</span>
                <span>{props.date}</span>
            </div>
            <p>{props.content}</p>
        </div>
    );
};

export default Comment;