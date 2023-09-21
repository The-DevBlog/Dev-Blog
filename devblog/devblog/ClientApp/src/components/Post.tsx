import { useCallback, useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
import Comment from "./Comment";
import AddComment from "./AddComment";
import DeletePost from "./DeletePost";
import ICommentProps from "../interfaces/ICommentProps";
import ReactMarkdown from "react-markdown";
import Vote from "./Vote";
import { Carousel } from 'react-responsive-carousel';
import "react-responsive-carousel/lib/styles/carousel.min.css"; // requires a loader
import "./styles/Post.css";
import EditPost from "./EditPost";
import { GetIsAdmin } from "./AuthenticationService";

const Post = (props: IPost) => {
    const [comments, setComments] = useState<ICommentProps[]>();
    const [showAllComments, setShowAllComments] = useState(false);
    const [isAdmin, setIsAdmin] = useState(false);
    const [date, setDate] = useState<string>();
    const [description, setDescription] = useState(props.description);
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

    const handlePostEdit = (newDescription: string) => {
        setDescription(newDescription);
    }

    useEffect(() => setIsAdmin(GetIsAdmin), []);

    useEffect(() => {
        setDate(new Date(props.date).toLocaleDateString());
        getComments();
    }, [getComments, props.date, props.imgs]);

    return (
        <div className="post">
            <div>
                {/* DATE */}
                <div className="post-info">
                    <span>Log {props.postNumber}</span>
                    {isAdmin &&
                        <EditPost {...props} onPostEdit={handlePostEdit} />
                    }
                    <DeletePost {...props} />
                    <span className="date">{date}</span>
                </div>

                {/* IMAGES */}
                <Carousel
                    showThumbs={false}
                    showStatus={false}
                    dynamicHeight={true}>

                    {props.imgs && props.imgs?.map(img => (
                        <div ><img src={img.url} alt="development update img" /></div>
                    ))}
                </Carousel>

                {/* LIKE / DISLIKE */}
                <Vote postId={props.id} />

                {/* DESCRIPTION */}
                {props.description && <ReactMarkdown className="description" children={description || ""} />}

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
        </div >
    );
};

export default Post;