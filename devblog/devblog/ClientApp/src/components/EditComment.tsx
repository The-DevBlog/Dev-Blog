import { useState } from "react";
import { MdModeEditOutline as Edit } from "react-icons/md";

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
            {!props.isEditing && <Edit onClick={handleEdit} />}

            {props.isEditing && (
                <div style={{}}>
                    <textarea value={content} onChange={(e) => setContent(e.target.value)} style={{ width: "80%", height: "30px" }} />

                    <div style={{ display: "flex" }}>
                        <button onClick={handleSave}>Save</button>
                        <button onClick={handleCancel}>Cancel</button>
                    </div>
                </div>
            )}
        </>
    )
}

export default EditComment;

