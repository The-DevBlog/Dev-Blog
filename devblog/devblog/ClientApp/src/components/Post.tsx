import { useCallback, useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
import Comment from "./Comment";
import AddComment from "./AddComment";
import DeletePost from "./DeletePost";
import ICommentProps from "../interfaces/ICommentProps";
import ReactMarkdown from "react-markdown";
import Vote from "./Vote";
import "react-responsive-carousel/lib/styles/carousel.min.css"; // requires a loader
import { Carousel } from 'react-responsive-carousel';
import "./Post.css";

const Post = (props: IPost) => {
    const [comments, setComments] = useState<ICommentProps[]>();
    const [showAllComments, setShowAllComments] = useState(false);
    const [date, setDate] = useState<string>();
    const displayedComments = showAllComments ? comments : comments?.slice(0, 5);

    const getComments = useCallback(async () => {
        const response = await fetch(`/api/comments/posts/${props.id}`);

        // sort comments by date - descending
        const data: ICommentProps[] = await response.json();
        const sortedComments = data.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
        setComments(sortedComments);
    }, [props.id]);

    const handleCommentChange = async () => {
        getComments();
    };

    useEffect(() => {
        setDate(new Date(props.date).toLocaleDateString());
        getComments();
    }, [getComments, props.date, props.imgs]);

    return (
        <div className="post">
            {/* UPDATE NUMBER & DATE */}
            <div className="post-info">
                <DeletePost {...props} />
                <span>{props.updateNum}</span>
                <span className="date">{date}</span>
            </div>

            {/* IMAGES */}
            <Carousel
                showThumbs={false}
                showStatus={false}
                dynamicHeight={true}>

                {props.imgs && props.imgs?.reverse().map(img => (
                    <div><img src={img.url} alt="development update img" /></div>
                ))}
            </Carousel>

            {/* LIKE / DISLIKE */}
            <Vote postId={props.id} />

            {props.description && <ReactMarkdown className="description" children={props.description} />}

            {/* COMMENTS */}
            <AddComment postId={props.id} onCommentAdd={handleCommentChange} />
            <div>
                {displayedComments?.map((c) => <Comment key={c.id} {...c} handleCommentChange={handleCommentChange} />)}
                {(comments && comments.length > 5) && (
                    <button className="show-all-comments-btn" onClick={() => setShowAllComments(!showAllComments)}>
                        {showAllComments ? 'Hide Comments' : 'Show All Comments'}
                    </button>
                )}
            </div>
        </div>
    );
};

export default Post;