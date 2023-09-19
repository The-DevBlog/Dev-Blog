import IPost from "../interfaces/IPostProps";
import { MdDelete as Trash } from "react-icons/md";
import { GetIsAdmin } from "../components/AuthenticationService";
import { useNavigate } from "react-router-dom";
import "./styles/DeletePost.css";
import { useEffect, useState } from "react";

const DeletePost = (props: IPost) => {
    const [isAdmin, setIsAdmin] = useState(false);
    const navigate = useNavigate();

    const handleDelete = () => {
        fetch(`api/posts/${props.id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            }
        }).then(() => {
            navigate("/");
        });
    };

    useEffect(() => setIsAdmin(GetIsAdmin), []);

    return <>{isAdmin && <Trash className="delete-post-btn" onClick={handleDelete} />}</>
}

export default DeletePost;