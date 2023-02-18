import { useEffect, useState } from "react";
import { IPostProps } from "../components/Post";
import Post from "../components/Post";

const Posts = () => {
    const [posts, setPosts] = useState<IPostProps[]>([]);

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