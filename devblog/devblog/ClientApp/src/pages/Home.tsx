import { FormEvent, useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
import Post from "../components/Post";
import { GetIsAdmin } from "../components/AuthenticationService";
import "./styles/Home.css"

const Home = () => {
    const [latestPost, setLatestPost] = useState<IPost>();
    const [isAdmin, setIsAdmin] = useState(false);
    const [url, setUrl] = useState<string>();
    const [totalPosts, setTotalPosts] = useState(0);

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
            .then((data) => setLatestPost(data))
            .catch((e) => console.log("Error retrieving latest post: " + e));
    }

    useEffect(() => {
        getVideoUrl();
        setIsAdmin(GetIsAdmin);
        getLatestPost();

        fetch("api/posts/count")
            .then((res) => { return res.json(); })
            .then((data) => {
                setTotalPosts(data)
            }).catch((e) => console.log("Error retrieving post count: " + e));
    }, []);

    return (
        <section className="latest-post">
            <div className="subscribe-to-email">
                <h1>Dont miss a thing!</h1>
                <button>Subscribe to the email list</button>
            </div>

            {latestPost ? < Post {...latestPost} key={latestPost.id} postNumber={totalPosts} /> : <h1>Loading...</h1>}

            {/* update YouTube video url */}
            <div className="youtube-video">
                {isAdmin &&
                    <form className="update-video" onSubmit={setVideoUrl}>
                        <label>URL </label>
                        <input type="text" value={url} onChange={(e) => setUrl(e.target.value)} />
                        <button>Update</button>
                    </form>}

                {/* YouTube video */}
                <iframe className="youtube-video"
                    src={url}
                    title="YouTube video player"
                    allowFullScreen
                ></iframe>
            </div>
        </section>
    )
}

export default Home;