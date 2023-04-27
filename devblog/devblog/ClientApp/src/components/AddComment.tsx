import { FormEvent, useState, useEffect } from "react";
import { GetUserName, IsLoggedIn } from "./AuthenticationService";
import "./AddComment.css";

interface IAddCommentProps {
    postId?: number;
    onCommentAdd: () => void;
}

const AddComment = (props: IAddCommentProps) => {
    const [loggedIn, setLoggedIn] = useState(false);
    const [content, setContent] = useState("");
    const [userName, setUsername] = useState("");

    useEffect(() => {
        setLoggedIn(IsLoggedIn);
        setUsername(GetUserName);
    }, []);

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        let postId = props.postId;;

        await fetch("api/comments", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify({ content, userName, postId })
        }).then(() => {
            setContent("");
            props.onCommentAdd();
        });
    }

    return (
        <div className="create-comment">
            <form onSubmit={handleSubmit}>

                {loggedIn ? (
                    <>
                        <textarea
                            placeholder="your comment here..."
                            required
                            value={content}
                            onChange={(e) => setContent(e.target.value)}>
                        </textarea>
                        <button>Add Comment</button>
                    </>
                ) : (
                    <textarea
                        placeholder="sign in to comment..."
                        disabled>
                    </textarea>
                )}
            </form>
        </div>
    )
}

export default AddComment;