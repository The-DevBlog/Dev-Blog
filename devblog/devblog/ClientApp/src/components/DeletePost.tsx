import IPost from "../interfaces/IPost";

const DeletePost = (props: IPost) => {
    const handleDelete = () => {
        fetch(`api/posts/${props.id}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });
    };

    return (
        <button onClick={handleDelete}>Delete</button>
    )
}

export default DeletePost;