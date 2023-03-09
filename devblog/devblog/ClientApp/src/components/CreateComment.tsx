import { FormEvent, useState, useEffect } from "react";
import IPost from "../interfaces/IPost";
import "./CreateComment.css";
import { GetUserName, IsLoggedIn } from "../components/AuthenticationService";

const CreateComment = (props: { postId: number; addComment: (comment: any) => void }) => {
    const [loggedIn, setLoggedIn] = useState(false);
    const [content, setContent] = useState("");
    const [userName, setUsername] = useState("");

    useEffect(() => {
        setLoggedIn(IsLoggedIn);
        setUsername(GetUserName);
    }, []);

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        // let postId = post.id;
        let postId = props.postId;;

        fetch("api/comments", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify({ content, userName, postId })
        }).then(() => {
            setContent("");
            props.addComment({ content, userName, postId })
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