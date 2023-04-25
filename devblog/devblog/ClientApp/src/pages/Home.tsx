import { useEffect, useState } from "react";
import IPost from "../interfaces/IPost";
import Post from "../components/Post";

const Home = () => {
    const [latestPost, setLatestPost] = useState<IPost>();

    // fetch latest post from server
    useEffect(() => {
        fetch(`api/posts/${-1}`)
            .then((res) => { return res.json(); })
            .then((data) => setLatestPost(data));
    }, []);

    return (
        <section className="posts-container">
            <h1>HOME</h1>
            <Post {...latestPost} />
        </section>
    )
}

export default Home;