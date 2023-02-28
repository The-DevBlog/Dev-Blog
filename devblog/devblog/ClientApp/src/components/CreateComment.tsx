import { FormEvent, useState, useEffect } from "react";
import IPost from "../interfaces/IPost";
import "./CreateComment.css";

const CreateComment = (post: IPost) => {
    const [loggedIn, setLoggedIn] = useState(false);
    const [content, setContent] = useState("");
    const [userName, setUsername] = useState("");

    useEffect(() => {
        const token = localStorage.getItem("token")!;

        if (token) {
            setLoggedIn(true);
            setUsername(localStorage.getItem("userName")!);
        }
    }, []);

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        let postId = post.id;

        fetch("api/comments", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify({ content, userName, postId })
        }).then(() => {
            setUsername("");
            setContent("");
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

export default CreateComment;