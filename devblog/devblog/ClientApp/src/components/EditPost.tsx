import { FormEvent, useEffect, useState } from "react";
import ReactMarkdown from "react-markdown";

interface IEditPostProps {
    id?: number,
    description?: string
}

const EditPost = (props: IEditPostProps) => {
    const [description, setDescription] = useState(props.description);
    const [charCount, setCharCount] = useState<number>();

    const updatePost = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        console.log(description);
        console.log(props.id);

        await fetch(`api/posts/${props.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify(description)
        });
    }

    useEffect(() => {
        setCharCount(description?.length);
    }, []);

    useEffect(() => {
        setCharCount(description?.length);
    }, [description]);

    return (
        <>
            <div>
                <form onSubmit={updatePost}>
                    <label>Description</label>
                    <p>Mastodon char count: {charCount}/500</p>
                    <div>
                        <textarea value={description} onChange={(e) => setDescription(e.currentTarget.value)} />
                        <ReactMarkdown className="description-result" children={description || ""} />
                    </div>

                    <button>Update Post</button>
                </form>
            </div>
        </>
    )
}

export default EditPost;