import IComment from "../interfaces/IComment";

const Comment = (props: IComment) => {
    return (
        <div>
            <div>
                <span>{props.userName}</span>
                <span>{props.date}</span>
            </div>
            <p>{props.content}</p>
        </div>
    );
};

export default Comment;