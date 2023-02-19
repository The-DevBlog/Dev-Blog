import { useEffect, useState } from "react";
import IPost from "../interfaces/IPost";
import Post from "../components/Post";
import "./Posts.css";

const Posts = () => {
    const [posts, setPosts] = useState<IPost[]>([]);

    // USING JSON SERVER
    // fetch posts data from local json server
    // useEffect(() => {
    //     fetch("http://localhost:8000/posts")
    //         .then(res => {
    //             return res.json();
    //         })
    //         .then((data) => {
    //             console.log(data);
    //             setPosts(data);
    //         });
    // }, []);

    useEffect(() => {
        fetch(`/posts/get`)
            .then((res) => {
                return res.json();
            })
            .then((data) => {
                setPosts(data);
            })
    }, []);

    return (
        <section className="posts-container">
            <h1>POSTS</h1>
            {posts.map((p) => {
                return <Post id={p.id} updateNum={p.updateNum} date={p.date} description={p.description} imgURL={p.imgURL} />
            })}
        </section>
    );

}

export default Posts;