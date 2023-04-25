import { useEffect, useState } from "react";
import IPost from "../interfaces/IPost";
import Comment from "./Comment";
import AddComment from "./AddComment";
import DeletePost from "./DeletePost";
import "./Post.css";

const Post = (props: IPost) => {
    const [comments, setComments] = useState(props.comments);

    const addComment = (comment: any) => {
        setComments([...comments!, comment]);
    };

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
                {comments?.map((c) => {
                    return <Comment id={c.id} content={c?.content} date={c?.date} userName={c?.userName} />
                })}
            </div>
            <AddComment postId={props.id!} addComment={addComment} />
        </div>
    );
};

export default Post;