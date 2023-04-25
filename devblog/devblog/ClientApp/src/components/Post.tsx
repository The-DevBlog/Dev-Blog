import { useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
import Comment from "./Comment";
import AddComment from "./AddComment";
import DeletePost from "./DeletePost";
import ICommentProps from "../interfaces/ICommentProps";
import "./Post.css";

const Post = (props: IPost) => {
    const [comments, setComments] = useState<ICommentProps[]>();

    const getComments = async () => {
        const response = await fetch(`/api/comments/posts/${props.id}`);
        const data = await response.json();
        setComments(data);
    };

    const handleCommentChange = async () => {
        getComments();
    };

    useEffect(() => {
        getComments();
    }, []);

    return (
        <div className="post">
            <div className="post-info">
                <DeletePost id={props.id} />
                <span>{props.updateNum}</span>
                <span>{props.date}</span>
            </div>
            <img src={props.imgURL} alt="development update" />
            <p>{props.description}</p>
            <div>
                {comments?.map((c) => <Comment key={c.id} {...c} handleCommentChange={handleCommentChange} />)}
            </div>
            <AddComment postId={props.id} onCommentAdd={handleCommentChange} />
        </div>
    );
};

export default Post;