import { useEffect, useState } from "react";
import IPost from "../interfaces/IPostProps";
import Post from "../components/Post";
import "./Posts.css";
import { GetIsAdmin } from "../components/AuthenticationService";
import { Link } from "react-router-dom";

const Posts = () => {
    const [posts, setPosts] = useState<IPost[]>([]);
    const [isAdmin, setIsAdmin] = useState(false);

    // fetch posts from server
    useEffect(() => {
        setIsAdmin(GetIsAdmin);
        fetch(`api/posts`)
            .then((res) => { return res.json(); })
            .then((data) => setPosts(data));
    }, []);

    return (
        <section className="posts-container">
            <h1>POSTS</h1>
            {isAdmin && <Link className="create-post-btn" to="/posts/create">Create Post</Link>}
            {posts.map((p) => <Post {...p} />)}
        </section>
    );

}

export default Posts;