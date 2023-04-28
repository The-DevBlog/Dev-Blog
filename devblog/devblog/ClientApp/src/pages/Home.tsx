import { useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
import Post from "../components/Post";
import "./Home.css"
import { ThreeDots } from 'react-loader-spinner'
const Home = () => {
    const [latestPost, setLatestPost] = useState<IPost>();

    const getLatestPost = async () => {
        await fetch(`api/posts/${-1}`)
            .then((res) => { return res.json(); })
            .then((data) => setLatestPost(data));
    }

    useEffect(() => {
        getLatestPost();
    }, []);

    return (
        <section className="latest-post">
            {latestPost ? (
                < Post {...latestPost} />
            ) : (
                <>
                    <h2>Loading</h2>
                    <ThreeDots color="grey" />
                </>
            )}


            <iframe className="youtube-video"
                width="925"
                height="520"
                src="https://www.youtube.com/embed?listType=playlist&list=PLp0sjyxOq4ASGN_YTLo2tObmkwXgvl6IX&index=1"
                title="YouTube video player"
                frameBorder="0"
                allowFullScreen
            ></iframe>

        </section>
    )
}

export default Home;