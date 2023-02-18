import { useEffect, useState } from "react";
import IPost from "../interfaces/IPost";
import Post from "../components/Post";

const Posts = () => {
    const [posts, setPosts] = useState<IPost[]>([]);

    useEffect(() => {
        fetch("http://localhost:8000/posts")
            .then(res => {
                return res.json();
            })
            .then((data) => {
                console.log(data);
                setPosts(data);
            });
    }, []);

    return (
        <>
            <h1>POSTS</h1>
            {posts.map((p) => {
                return <Post Id={p.Id} UpdateNum={p.UpdateNum} Date={p.Date} Description={p.Description} ImgURL={p.ImgURL} />
            })}
        </>
    );

}

export default Posts;