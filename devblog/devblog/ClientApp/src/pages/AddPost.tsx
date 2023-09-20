import { FormEvent, useEffect, useState } from "react";
import ReactMarkdown from "react-markdown";
import { GetIsAdmin } from "../components/AuthenticationService";
import { PulseLoader } from "react-spinners";
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
    const [devBlogUploadStatus, setDevBlogUploadStatus] = useState<number>(0);
    const [devBlogErrMessage, setDevBlogErrMessage] = useState("");
    const [postToDiscord, setPostToDiscord] = useState(false);
    const [postToMastodon, setPostToMastodon] = useState(false);
    const [postToDevBlog, setPostToDevBlog] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setIsLoading(true);

        const formData = new FormData();
        if (files != null) {
            files.forEach(f => formData.append("files", f));
            formData.append("description", description);
            formData.append("postToDiscord", postToDiscord.toString());
            formData.append("postToMastodon", postToMastodon.toString());
            formData.append("postToDevBlog", postToDevBlog.toString());
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
                if (postToDevBlog) {
                    setDevBlogUploadStatus(data.devBlogStatus.statusCode);
                    setDevBlogErrMessage(data.devBlogStatus.reasonPhrase);
                }

                if (postToDiscord) {
                    setDiscordUploadStatus(data.discordStatus.statusCode);
                    setDiscordErrMessage(data.discordStatus.reasonPhrase)
                }

                if (postToMastodon) {
                    setMastodonUploadStatus(data.mastodonStatus.statusCode);
                    setMastodonErrMessage(data.mastodonStatus.reasonPhrase)
                }
            }
        })
            .catch(e => console.log("Error uploading file: ", e));

        setIsLoading(false);
    }

    useEffect(() => {
        setIsAdmin(GetIsAdmin)
    }, []);

    useEffect(() => {
        setCharCount(description.length);
    }, [description]);

    return (
        < div className="create-post" >
            {isAdmin &&
                <form onSubmit={handleSubmit}>
                    <div className="upload-platforms">
                        <p>Upload to:</p>
                        <ul>
                            <li>
                                <label>
                                    <input
                                        type="checkbox"
                                        checked={postToDiscord}
                                        onChange={(e) => setPostToDiscord(e.target.checked)} />
                                    Discord
                                </label>
                            </li>

                            <li>
                                <label>
                                    <input
                                        type="checkbox"
                                        checked={postToMastodon}
                                        onChange={(e) => setPostToMastodon(e.target.checked)} />
                                    Mastodon
                                </label>
                            </li>

                            <li>
                                <label>
                                    <input
                                        type="checkbox"
                                        checked={postToDevBlog}
                                        onChange={(e) => setPostToDevBlog(e.target.checked)} />
                                    DevBlog
                                </label>
                            </li>
                        </ul>

                        <PulseLoader className="loader" color="WHITE" loading={isLoading} />

                        {/* Upload Statuses */}
                        <div className="upload-status">
                            {/* discord */}
                            <h4 style={{ display: postToDiscord ? 'block' : 'none' }}>Discord Upload Status:
                                <span style={{ color: discordUploadStatus.toString().startsWith('2') ? 'green' : 'red' }}>
                                    {discordUploadStatus}
                                </span>
                                {discordUploadStatus !== 200 &&
                                    <p>{discordErrMessage}</p>
                                }
                            </h4>
                            {/* mastodon */}
                            <h4 style={{ display: postToMastodon ? 'block' : 'none' }}>Mastodon Upload Status:
                                <span style={{ color: mastodonUploadStatus.toString().startsWith('2') ? 'green' : 'red' }}>
                                    {mastodonUploadStatus}
                                </span>
                                {mastodonUploadStatus !== 200 &&
                                    <p>{mastodonErrMessage}</p>
                                }
                            </h4>
                            {/* thedevblog */}
                            <h4 style={{ display: postToDevBlog ? 'block' : 'none' }}>TheDevBlog Upload Status:
                                <span style={{ color: devBlogUploadStatus.toString().startsWith('2') ? 'green' : 'red' }}>
                                    {devBlogUploadStatus}
                                </span>
                                {devBlogUploadStatus !== 200 &&
                                    <p>{devBlogErrMessage}</p>
                                }
                            </h4>
                        </div>
                    </div>

                    <div className="new-post-info">
                        <label>Image
                            <input
                                type="file"
                                required
                                multiple
                                onChange={(e) => e.target.files && setFile(Array.from(e.target.files))} />
                        </label>

                        <label>Description
                            <p>Mastodon character limit: {charCount}/500</p>
                            <p>Discord character limit: {charCount}/2000</p>
                            <div className="addpost-description">
                                <textarea placeholder="Write description here..." onChange={(e) => setDescription(e.currentTarget.value)} />
                            </div>
                        </label>

                        <p>Preview:</p>
                        <div className="post-preview">
                            <ReactMarkdown children={description} />
                        </div>
                        <button>Create Post</button>
                    </div>
                </form>
            }
        </div >
    )
}

export default AddPost;