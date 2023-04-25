import IComment from "../interfaces/IComment";
import { MdDelete as Trash } from "react-icons/md";

const DeleteComment = (props: IComment) => {
    const handleDelete = () => {
        fetch(`api/comments/${props.id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            }
        });
    };

    return <Trash onClick={handleDelete} />
}

export default DeleteComment;