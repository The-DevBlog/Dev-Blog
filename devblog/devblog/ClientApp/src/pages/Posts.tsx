import { useEffect, useState } from "react";
import { MdChevronLeft } from "react-icons/md"
import { MdChevronRight } from "react-icons/md"
import IPost from "../interfaces/IPostProps";
import Post from "../components/Post";
import { GetIsAdmin } from "../components/AuthenticationService";
import { Link } from "react-router-dom";
import "./styles/Posts.css";

const Posts = () => {
    const [posts, setPosts] = useState<IPost[]>([]);
    const [isAdmin, setIsAdmin] = useState(false);
    const [pageNum, setPageNum] = useState(1);
    const [totalPages, setTotalPages] = useState(0);

    const pageDown = (() => {
        if (pageNum > 0) {
            setPageNum(pageNum - 1);
        }

        console.log(totalPages);
    });

    const pageUp = (() => {
        setPageNum(pageNum + 1);
        console.log(totalPages);
    });

    useEffect(() => {
        fetch(`api/posts/page/${pageNum}`)
            .then((res) => { return res.json(); })
            .then((data) => setPosts(data));
    }, [pageNum]);


    useEffect(() => {
        setIsAdmin(GetIsAdmin);
        fetch("api/posts/page/count")
            .then((res) => { return res.json(); })
            .then((data) => {
                setTotalPages(data)
            });
    }, []);

    const Pager = () => {
        return <div className="pager">
            {pageNum > 1 ? (
                <MdChevronLeft className="arrow-visible" onClick={pageDown} />
            ) : (
                <MdChevronLeft className="arrow-hidden" />
            )}

            <span>{pageNum}</span>

            {pageNum < totalPages ? (
                <MdChevronRight className="arrow-visible" onClick={pageUp} />
            ) : (
                <MdChevronRight className="arrow-hidden" />
            )}
        </div>
    }

    return (
        <section className="posts">
            <Pager />
            {isAdmin && <Link className="create-post-btn" to="/posts/create">Create Post</Link>}
            {posts.length === 0 ? <h1>Loading...</h1> : posts.map((p) => <Post key={p.id} {...p} />)}
            <Pager />
        </section>
    );
}

export default Posts;
