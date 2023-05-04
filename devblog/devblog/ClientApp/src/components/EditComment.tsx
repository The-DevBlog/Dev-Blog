import { useState } from "react";
import { MdModeEditOutline as Edit } from "react-icons/md";
import "./styles/EditComment.css";

interface IEditCommentProps {
    id?: number;
    content?: string;
    isEditing: boolean;
    setIsEditing: React.Dispatch<React.SetStateAction<boolean>>;
    handleCommentEdit: () => void;
}

const EditComment = (props: IEditCommentProps) => {
    const [content, setContent] = useState(props.content);

    const handleEdit = () => {
        props.setIsEditing(true);
    };

    const handleSave = async () => {
        props.setIsEditing(false);

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
        props.setIsEditing(false);
        setContent(props.content);
    };

    return (
        <>
            {!props.isEditing && <Edit className="edit-comment-btn" onClick={handleEdit} />}

            {props.isEditing && (
                <div className="edit-comment">
                    <textarea value={content} onChange={(e) => setContent(e.target.value)} style={{ width: "80%", height: "30px" }} />

                    <div>
                        <button onClick={handleSave}>Save</button>
                        <button onClick={handleCancel}>Cancel</button>
                    </div>
                </div>
            )}
        </>
    )
}

export default EditComment;

