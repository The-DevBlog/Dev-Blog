import { useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
import Post from "../components/Post";

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
        <section className="posts-container">
            <h1>HOME</h1>
            {latestPost && <Post {...latestPost} />}
        </section>
    )
}

export default Home;