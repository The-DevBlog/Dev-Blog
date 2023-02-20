import { FormEvent, useState } from "react";
import IPost from "../interfaces/IPost";

const CreatePost = () => {
    // const [newPost, setNewPost] = useState<IPost>();
    const [updateNum, setUpdateNum] = useState("");
    const [description, setDescription] = useState("");
    const [imgURL, setImgURL] = useState("");

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const newPost = { updateNum, description, imgURL };

        fetch("api/posts", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(newPost)
        });

        // console.log(newPost);
    }

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <label>Update Number</label>
                <input
                    type="text"
                    required
                    value={updateNum}
                    onChange={(e) => setUpdateNum(e.target.value)}
                />

                <label>Description</label>
                <input type="text"
                    required
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />

                <label>Image</label>
                <input
                    type="text"
                    required
                    value={imgURL}
                    onChange={(e) => setImgURL(e.target.value)}
                />

                <button>Create Post</button>
            </form>
        </div>
    )
}

export default CreatePost;