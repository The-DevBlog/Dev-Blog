import IPost from "../interfaces/IPost";

const Post = (props: IPost) => {
    return (
        <div>
            <span>{props.UpdateNum}</span>
            <span>{props.Date}</span>
            <img style={{ display: "block" }} src={props.ImgURL} alt="image of post" />
            <p>{props.Description}</p>
        </div>
    );
};

export default Post;