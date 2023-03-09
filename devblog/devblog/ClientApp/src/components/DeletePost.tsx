import IPost from "../interfaces/IPost";
import { MdDelete as Trash } from "react-icons/md";
import { IsAdmin } from "../components/AuthenticationService";
import { useEffect, useState } from "react";

const DeletePost = (props: IPost) => {
    const [isAdmin, setIsAdmin] = useState(false);

    const handleDelete = () => {
        fetch(`api/posts/${props.id}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });
    };

    useEffect(() => setIsAdmin(IsAdmin), []);

    return (
        <>
            {isAdmin &&
                <Trash onClick={handleDelete} />
            }
        </>
    )
}

export default DeletePost;