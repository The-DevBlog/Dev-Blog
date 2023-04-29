import { FormEvent, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { GetIsAdmin } from "../components/AuthenticationService";
import "./AddPost.css";

const AddPost = () => {
    const [updateNum, setUpdateNum] = useState("");
    const [description, setDescription] = useState("");
    const [isAdmin, setIsAdmin] = useState(false);
    const [file, setFile] = useState<File>();
    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const formData = new FormData();
        if (file != null) {
            formData.append("file", file);
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
                        type="file"
                        required
                        // onChange={(e) => handleFileInput(e)}
                        onChange={(e) => e.target.files && setFile(e.target.files[0])}
                    />

                    <button>Create Post</button>
                </form>
            }
        </div >
    )
}

export default AddPost;