import { FormEvent, useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
import Post from "../components/Post";
import { GetIsAdmin } from "../components/AuthenticationService";
import "./styles/Home.css"

const Home = () => {
    const [latestPost, setLatestPost] = useState<IPost>();
    const [isAdmin, setIsAdmin] = useState(false);
    const [url, setUrl] = useState<string>();

    const getVideoUrl = async () => {
        await fetch(`api/YtVideo`)
            .then((res) => { return res.json(); })
            .then((data) => setUrl(data.url));
    }

    const setVideoUrl = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        await fetch(`api/YtVideo/1`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify(url)
        });
    }

    const getLatestPost = async () => {
        await fetch(`api/posts/${-1}`)
            .then((res) => { return res.json(); })
            .then((data) => setLatestPost(data));
    }

    useEffect(() => {
        getVideoUrl();
        setIsAdmin(GetIsAdmin);
        getLatestPost();
    }, []);

    return (
        <section className="latest-post">
            {latestPost ? < Post {...latestPost} key={latestPost.id} /> : <h1>Loading...</h1>}

            {/* update YouTube video url */}
            {isAdmin &&
                <form className="update-video" onSubmit={setVideoUrl}>
                    <label>URL </label>
                    <input type="text" value={url} onChange={(e) => setUrl(e.target.value)} />
                    <button>Update</button>
                </form>
            }

            {/* YouTube video */}
            <iframe className="youtube-video"
                src={url}
                title="YouTube video player"
                allowFullScreen
            ></iframe>
        </section>
    )
}

export default Home;