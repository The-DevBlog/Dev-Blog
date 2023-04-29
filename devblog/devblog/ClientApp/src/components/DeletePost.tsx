import IPost from "../interfaces/IPostProps";
import { MdDelete as Trash } from "react-icons/md";
import { GetIsAdmin } from "../components/AuthenticationService";
import { useEffect, useState } from "react";

const DeletePost = (props: IPost) => {
    const [isAdmin, setIsAdmin] = useState(false);

    const handleDelete = () => {
        fetch(`api/posts/${props.id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            }
        });
    };

    useEffect(() => setIsAdmin(GetIsAdmin), []);

    return <>{isAdmin && <Trash onClick={handleDelete} />}</>
}

export default DeletePost;