import { useEffect, useState } from "react";
import IPost from "../interfaces/IPost";
import Post from "../components/Post";
import "./Posts.css";
import { Link, Route, Routes } from "react-router-dom";

const Posts = () => {
    const [posts, setPosts] = useState<IPost[]>([]);

    // fetch posts from server
    useEffect(() => {
        fetch(`api/posts`)
            .then((res) => {
                return res.json();
            })
            .then((data) => {
                setPosts(data);
            })
    }, [posts]);

    return (
        <section className="posts-container">
            <h1>POSTS</h1>
            <Link to="/posts/create">Create Post</Link>
            {posts.map((p) => {
                return <Post id={p?.id} updateNum={p?.updateNum} date={p?.date} description={p?.description} imgURL={p?.imgURL} comments={p?.comments} />
            })}
        </section>
    );

}

export default Posts;