import IPost from "../interfaces/IPost";
import { MdDelete as Trash } from "react-icons/md";

const DeletePost = (props: IPost) => {
    const handleDelete = () => {
        fetch(`api/posts/${props.id}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });
    };

    return (
        <Trash onClick={handleDelete} />
    )
}

export default DeletePost;