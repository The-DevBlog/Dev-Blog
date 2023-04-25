import { MdDelete as Trash } from "react-icons/md";

interface IDeleteCommentProps {
    id?: number;
    onCommentDelete: () => void;
}

const DeleteComment = (props: IDeleteCommentProps) => {
    const handleDelete = async () => {
        await fetch(`api/comments/${props.id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            }
        });

        props.onCommentDelete();
    };

    return <Trash onClick={handleDelete} />
}

export default DeleteComment;