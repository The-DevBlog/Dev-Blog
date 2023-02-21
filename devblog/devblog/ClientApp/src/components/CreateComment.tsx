import { FormEvent, useState } from "react";
import IPost from "../interfaces/IPost";
import "./CreateComment.css";

const CreateComment = (post: IPost) => {
    const [content, setContent] = useState("");
    const [userName, setUserName] = useState("");

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        let postId = post.id;
        const newPost = { content, userName, postId };

        fetch("api/comments", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(newPost)
        }).then(() => {
            setUserName("");
            setContent("");
        });
    }

    return (
        <div className="create-comment">
            <form onSubmit={handleSubmit}>
                <input
                    placeholder="username"
                    required
                    value={userName}
                    onChange={(e) => setUserName(e.target.value)}
                />

                <textarea
                    placeholder="your comment here..."
                    required
                    value={content}
                    onChange={(e) => setContent(e.target.value)}>
                </textarea>

                <button>Add Comment</button>
            </form>
        </div>
    )
}

export default CreateComment;