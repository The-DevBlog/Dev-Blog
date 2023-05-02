import IComment from "./ICommentProps";

export default interface IPostProps {
    id?: number;
    updateNum?: string;
    description?: string;
    date: Date;
    imgs?: ImgProps[];
    comments?: IComment[];
}

interface ImgProps {
    postId?: number,
    url: string
}
