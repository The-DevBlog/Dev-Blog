import IComment from "./ICommentProps";

export default interface IPostProps {
    id?: number;
    comments?: IComment[];
    description?: string;
    date: Date;
    imgs?: ImgProps[];
    postNumber: number
}

interface ImgProps {
    postId?: number,
    url: string
}
