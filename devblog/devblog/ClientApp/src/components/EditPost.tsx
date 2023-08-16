import { FormEvent, useEffect, useState } from "react";

interface IEditPostProps {
    id?: number;
    description?: string;
    onPostEdit: (newDescription: string) => void;
}

const EditPost = (props: IEditPostProps) => {
    const [description, setDescription] = useState(props.description);
    const [charCount, setCharCount] = useState<number>();

    const updatePost = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        await fetch(`api/posts/${props.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify(description)
        });
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
            <div>
                <form onSubmit={updatePost}>
                    <p>Mastodon char count: {charCount}/500</p>
                    <div>
                        <textarea value={description} onChange={handleDescriptionChange} style={{ width: "550px", height: "200px" }} />
                    </div>

                    <button>Update Post</button>
                </form>
            </div>
        </>
    )
}

export default EditPost;