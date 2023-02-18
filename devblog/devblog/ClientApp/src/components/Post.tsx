import IPost from "../interfaces/IPost";
import "./Post.css";

const Post = (props: IPost) => {
    return (
        <div className="post">
            <div className="post-info">
                <span>{props.UpdateNum}</span>
                <span>{props.Date}</span>
            </div>
            <img style={{ display: "block" }} src={props.ImgURL} alt="image of post" />
            <p>{props.Description}</p>
        </div>
    );
};

export default Post;