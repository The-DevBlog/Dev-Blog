import IPost from "../interfaces/IPost";
import Comment from "./Comment";
import "./Post.css";

const Post = (props: IPost) => {
    const handleDelete = () => {
        fetch(`api/posts/${props.id}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });
    };

    return (
        <div className="post">
            <button onClick={handleDelete}>Delete</button>
            <div className="post-info">
                <span>{props.updateNum}</span>
                <span>{props.date}</span>
            </div>
            <img src={props.imgURL} alt="image of post" />
            <p>{props.description}</p>
            <div>
                {props.comments?.map((c) => {
                    return <Comment content={c?.content} date={c?.date} userName={c?.userName} />
                })}
            </div>
        </div>
    );
};

export default Post;