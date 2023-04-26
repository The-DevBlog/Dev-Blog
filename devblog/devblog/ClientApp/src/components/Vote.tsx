import { useEffect, useState } from "react";
import { GetUserName } from "./AuthenticationService";
import { FaFrownOpen as DownVote, FaSmile as UpVote } from "react-icons/fa";
import "./Vote.css";

interface IVoteProps {
    postId?: number;
}

const Vote = (props: IVoteProps) => {
    const [username, setUsername] = useState("");

    useEffect(() => {
        setUsername(GetUserName);
    }, []);

    const handleVote = async (vote: string) => {
        // e.preventDefault();

        let postId = props.postId;;

        await fetch(`api/posts/${props.postId}/upvote`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                // "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify({ id: postId, username: "devmaster" })
        })
    }

    return (
        <div>
            <UpVote onClick={() => handleVote("upvote")} className="up-vote" />
            <DownVote onClick={() => handleVote("downvote")} className="down-vote" />
        </div>
    )
}

export default Vote;