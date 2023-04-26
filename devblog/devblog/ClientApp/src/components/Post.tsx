import { useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
import Comment from "./Comment";
import AddComment from "./AddComment";
import DeletePost from "./DeletePost";
import ICommentProps from "../interfaces/ICommentProps";
import "./Post.css";
import Vote from "./Vote";

const Post = (props: IPost) => {
    const [comments, setComments] = useState<ICommentProps[]>();
    const [showAllComments, setShowAllComments] = useState(false);
    const [date, setDate] = useState<string>();
    const displayedComments = showAllComments ? comments : comments?.slice(0, 5);

    const getComments = async () => {
        const response = await fetch(`/api/comments/posts/${props.id}`);

        // sort comments by date - descending
        const data: ICommentProps[] = await response.json();
        const sortedComments = data.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
        setComments(sortedComments);
    };

    const handleCommentChange = async () => {
        getComments();
    };

    useEffect(() => {
        setDate(new Date(props.date).toLocaleDateString());
        getComments();
    }, []);

    return (
        <div className="post">
            <div className="post-info">
                <DeletePost {...props} />
                <span>{props.updateNum}</span>
                <span className="date">{date}</span>
            </div>
            <img src={props.imgURL} alt="development update" />

            <Vote postId={props.id} />
            <p className="post-description">{props.description}</p>

            <AddComment postId={props.id} onCommentAdd={handleCommentChange} />
            <div>
                {displayedComments?.map((c) => <Comment key={c.id} {...c} handleCommentChange={handleCommentChange} />)}
                {(comments && comments.length > 5) && (
                    <button onClick={() => setShowAllComments(!showAllComments)}>
                        {showAllComments ? 'Hide Comments' : 'Show All Comments'}
                    </button>
                )}
            </div>
        </div>
    );
};

export default Post;