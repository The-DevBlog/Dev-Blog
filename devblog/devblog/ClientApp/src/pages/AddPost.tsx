import { FormEvent, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import ReactMarkdown from "react-markdown";
import { GetIsAdmin } from "../components/AuthenticationService";
import "./AddPost.css";

const AddPost = () => {
    const [updateNum, setUpdateNum] = useState("");
    const [description, setDescription] = useState("");
    const [isAdmin, setIsAdmin] = useState(false);
    const [files, setFile] = useState<File[]>([]);
    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const formData = new FormData();
        if (files != null) {
            files.forEach(f => formData.append("files", f));
            // formData.append("file", file);
            formData.append("description", description);
            formData.append("updateNum", updateNum);
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

    return (
        < div className="create-post" >
            {isAdmin &&
                <form onSubmit={handleSubmit}>
                    <label>Update Number</label>
                    <input
                        type="text"
                        required
                        value={updateNum}
                        onChange={(e) => setUpdateNum(e.target.value)} />

                    <label>Image</label>
                    <input
                        type="file"
                        required
                        multiple
                        onChange={(e) => e.target.files && setFile(Array.from(e.target.files))} />

                    <label>Description</label>
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