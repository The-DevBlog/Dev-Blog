export interface IPostProps {
    Id: number;
    UpdateNum: string;
    Description: string;
    Date: string;
    ImgURL: string;
}

const Post = (props: IPostProps) => {
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