import { useState } from "react";
import { MdModeEditOutline as Edit } from "react-icons/md";

interface IEditCommentProps {
    id?: number;
    content?: string;
    handleCommentEdit: () => void;
}

const EditComment = (props: IEditCommentProps) => {
    const [content, setContent] = useState(props.content);
    const [isEditing, setIsEditing] = useState(false);

    const handleEdit = () => {
        setIsEditing(true);
    };

    const handleSave = async () => {
        setIsEditing(false);

        await fetch(`api/comments/${props.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify(content)
        }).then(() => props.handleCommentEdit());

    };

    const handleCancel = () => {
        setIsEditing(false);
        setContent(props.content);
    };

    return (
        <>
            {!isEditing && <button onClick={handleEdit}><Edit /></button>}
            {isEditing && (
                <div>
                    <textarea value={content} onChange={(e) => setContent(e.target.value)} />
                    <button onClick={handleSave}>Save</button>
                    <button onClick={handleCancel}>Cancel</button>
                </div>
            )}
        </>
    )
}

export default EditComment;

