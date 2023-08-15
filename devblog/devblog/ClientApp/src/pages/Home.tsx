import { useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
// import IYtVideo from "../interfaces/IYtVideoProps";
import Post from "../components/Post";
import "./styles/Home.css"
import IYtVideoProps from "../interfaces/IYtVideoProps";

const Home = () => {
    const [latestPost, setLatestPost] = useState<IPost>();
    const [videoUrl, setVideoUrl] = useState<IYtVideoProps>();

    const getVideoUrl = async () => {
        await fetch(`api/YtVideo`)
            .then((res) => { return res.json(); })
            .then((data) => setVideoUrl(data));
    }

    const getLatestPost = async () => {
        await fetch(`api/posts/${-1}`)
            .then((res) => { return res.json(); })
            .then((data) => setLatestPost(data));
    }

    useEffect(() => {
        getVideoUrl();
        getLatestPost();
    }, []);

    return (
        <section className="latest-post">
            {latestPost ? < Post {...latestPost} /> : <h1>Loading...</h1>}

            {/* <div className="video-input-container">
                <label htmlFor="videoUrl">Video URL:</label>
                <input
                    type="text"
                    id="videoUrl"
                    value={videoUrl}
                    onChange={handleVideoUrlChange}
                />
            </div> */}

            <iframe className="youtube-video"
                width="925"
                height="520"
                src={videoUrl?.url}
                title="YouTube video player"
                allowFullScreen
            ></iframe>

        </section>
    )
}

export default Home;