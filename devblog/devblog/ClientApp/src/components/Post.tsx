import IPost from "../interfaces/IPost";
import Comment from "./Comment";
import CreateComment from "./CreateComment";
import DeletePost from "./DeletePost";
import "./Post.css";

const Post = (props: IPost) => {
    return (
        <div className="post">
            <DeletePost id={props.id} />
            <div className="post-info">
                <span>{props.updateNum}</span>
                <span>{props.date}</span>
            </div>
            <img src={props.imgURL} alt="image of post" />
            <p>{props.description}</p>
            <div>
                {props.comments?.map((c) => {
                    return <Comment id={c.id} content={c?.content} date={c?.date} userName={c?.userName} />
                })}
            </div>
            <CreateComment id={props.id} />
        </div>
    );
};

export default Post;