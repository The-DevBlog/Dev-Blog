import { useState } from "react";
import IPost from "../interfaces/IPost";

const CreatePost = () => {
    const [newPost, setNewPost] = useState<IPost>();

    const handleSubmit = () => {
        // e.preventDefault();
    }

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <label>Update Number</label>
                <input type="text" required value={newPost?.updateNum} />

                <label>Description</label>
                <input type="text" required value={newPost?.description} />

                <label>Image</label>
                <input type="text" required value={newPost?.imgURL} />

                <button>Create Post</button>
            </form>
        </div>
    )
}

export default CreatePost;