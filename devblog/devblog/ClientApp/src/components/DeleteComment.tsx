import IComment from "../interfaces/IComment";
import { MdDelete as Trash } from "react-icons/md";

const DeleteComment = (props: IComment) => {
    const handleDelete = () => {
        console.log(props);

        fetch(`api/comments/${props.id}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });
    };

    return <Trash onClick={handleDelete} />

}

export default DeleteComment;