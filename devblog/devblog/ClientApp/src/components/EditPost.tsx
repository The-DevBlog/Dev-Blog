import { FormEvent, useEffect, useState } from "react";
import { MdModeEditOutline as Edit } from "react-icons/md";
import "./styles/EditPost.css";

interface IEditPostProps {
    id?: number;
    description?: string;
    onPostEdit: (newDescription: string) => void;
}

const EditPost = (props: IEditPostProps) => {
    const [description, setDescription] = useState(props.description);
    const [charCount, setCharCount] = useState<number>();
    const [isEditing, setIsEditing] = useState(false);

    const updatePost = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        await fetch(`api/posts/${props.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify(description)
        }).then(() => setIsEditing(false));
    }

    const handleDescriptionChange = async (e: React.ChangeEvent<HTMLTextAreaElement>
    ) => {
        setDescription(e.currentTarget.value);
        props.onPostEdit(e.currentTarget.value || "");
    }

    useEffect(() => {
        setCharCount(description?.length);
    }, [description]);

    return (
        <>
            {!isEditing && <Edit className="edit-post-btn" onClick={() => setIsEditing(true)} />}

            {isEditing && (
                <div className="edit-post">
                    <form onSubmit={updatePost}>
                        <p style={{ fontFamily: "sans-serif" }}>Mastodon char count: {charCount}/500</p>
                        <textarea className="edit-post-description" value={description} onChange={handleDescriptionChange} />

                        <button>Update</button>
                        <button onClick={() => setIsEditing(false)}>Cancel</button>
                    </form>
                </div>
            )}
        </>
    )
}

export default EditPost;