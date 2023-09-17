import { FormEvent, useEffect, useState } from "react";
import ReactMarkdown from "react-markdown";
import { GetIsAdmin } from "../components/AuthenticationService";
import "./styles/AddPost.css";

const AddPost = () => {
    const [description, setDescription] = useState("");
    const [charCount, setCharCount] = useState<number>();
    const [isAdmin, setIsAdmin] = useState(false);
    const [files, setFile] = useState<File[]>([]);
    const [discordUploadStatus, setDiscordUploadStatus] = useState<number>(0);
    const [discordErrMessage, setDiscordErrMessage] = useState("");
    const [mastodonUploadStatus, setMastodonUploadStatus] = useState<number>(0);
    const [mastodonErrMessage, setMastodonErrMessage] = useState("");

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const formData = new FormData();
        if (files != null) {
            files.forEach(f => formData.append("files", f));
            formData.append("description", description);
        }

        await fetch("api/posts", {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: formData
        }).then(async (res) => {
            if (res.ok) {
                let data = await res.json();
                setDiscordUploadStatus(data.discordStatus.statusCode);
                setDiscordErrMessage(data.discordStatus.reasonPhrase)
                setMastodonUploadStatus(data.mastodonStatus.statusCode);
                setMastodonErrMessage(data.mastodonStatus.reasonPhrase)
            }
        })
            .catch(e => console.log("Error uploading file: ", e));
    }

    useEffect(() => {
        setIsAdmin(GetIsAdmin)
    }, []);

    useEffect(() => {
        setCharCount(description.length);
    }, [description]);

    return (
        < div className="create-post" >
            {isAdmin && <>
                <form onSubmit={handleSubmit}>
                    <label>Image</label>
                    <input
                        type="file"
                        required
                        multiple
                        onChange={(e) => e.target.files && setFile(Array.from(e.target.files))} />

                    <label>Description </label>
                    <p>Mastodon char count: {charCount}/500</p>
                    <div className="addpost-description">
                        <textarea onChange={(e) => setDescription(e.currentTarget.value)} />
                        <ReactMarkdown className="description-result" children={description} />
                    </div>
                    <button>Create Post</button>

                </form>

                {/* Upload Statuses */}
                <div className="upload-status">
                    <h4>Discord Upload Status:
                        <span style={{ color: discordUploadStatus.toString().startsWith('2') ? 'green' : 'red' }}>
                            {discordUploadStatus}
                        </span>
                        {discordUploadStatus !== 200 &&
                            <p>{discordErrMessage}</p>
                        }
                    </h4>
                    <h4>Mastodon Upload Status:
                        <span style={{ color: mastodonUploadStatus.toString().startsWith('2') ? 'green' : 'red' }}>
                            {mastodonUploadStatus}
                        </span>
                        {mastodonUploadStatus !== 200 &&
                            <p>{mastodonErrMessage}</p>
                        }
                    </h4>
                </div>

            </>
            }
        </div >
    )
}

export default AddPost;