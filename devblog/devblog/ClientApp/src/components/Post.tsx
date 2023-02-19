import IPost from "../interfaces/IPost";
import "./Post.css";

const Post = (props: IPost) => {
    return (
        <div className="post">
            <div className="post-info">
                <span>{props.updateNum}</span>
                <span>{props.date}</span>
            </div>
            <img style={{ display: "block" }} src={props.imgURL} alt="image of post" />
            <p>{props.description}</p>
        </div>
    );
};

export default Post;