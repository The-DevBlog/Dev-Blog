import { FormEvent, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import ReactMarkdown from "react-markdown";
import { GetIsAdmin } from "../components/AuthenticationService";
import "./styles/AddPost.css";

const AddPost = () => {
    const [description, setDescription] = useState("");
    const [charCount, setCharCount] = useState<number>();
    const [isAdmin, setIsAdmin] = useState(false);
    const [files, setFile] = useState<File[]>([]);
    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const formData = new FormData();
        if (files != null) {
            files.forEach(f => formData.append("files", f));
            formData.append("description", description);
        }

        await fetch("api/posts", {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: formData
        }).then(() => navigate("/posts"))
            .catch(e => console.log("Error uploading file: ", e));
    }

    useEffect(() => {
        setIsAdmin(GetIsAdmin)
    }, []);

    useEffect(() => {
        setCharCount(description.length);
    }, [description]);

    return (
        < div className="create-post" >
            {isAdmin &&
                <form onSubmit={handleSubmit}>
                    <label>Image</label>
                    <input
                        type="file"
                        required
                        multiple
                        onChange={(e) => e.target.files && setFile(Array.from(e.target.files))} />

                    <label>Description </label>
                    <p>Mastodon char count: {charCount}/500</p>
                    <div className="addpost-description">
                        <textarea onChange={(e) => setDescription(e.currentTarget.value)} />
                        <ReactMarkdown className="description-result" children={description} />
                    </div>

                    <button>Create Post</button>
                </form>
            }
        </div >
    )
}

export default AddPost;