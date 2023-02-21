import IComment from "../interfaces/IComment";

const DeleteComment = (props: IComment) => {
    const handleDelete = () => {
        console.log(props);

        fetch(`api/comments/${props.id}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });
    };

    return (
        <button onClick={handleDelete}>Delete</button>
    )
}

export default DeleteComment;