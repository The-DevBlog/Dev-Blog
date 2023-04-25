import { FormEvent, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { GetIsAdmin } from "../components/AuthenticationService";
import "./CreatePost.css";

const CreatePost = () => {
    const [updateNum, setUpdateNum] = useState("");
    const [description, setDescription] = useState("");
    const [imgURL, setImgURL] = useState("");
    const [isAdmin, setIsAdmin] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        fetch("api/posts", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify({ updateNum, description, imgURL })
        }).then(() => {
            navigate("/posts");
        });
    }

    useEffect(() => {
        setIsAdmin(GetIsAdmin)
    }, []);

    return (
        < div className="create-post" >
            {isAdmin &&
                <form onSubmit={handleSubmit}>
                    <label>Update Number</label>
                    <input
                        type="text"
                        required
                        value={updateNum}
                        onChange={(e) => setUpdateNum(e.target.value)}
                    />

                    <label>Description</label>
                    <input
                        type="text"
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
            }
        </div >
    )
}

export default CreatePost;