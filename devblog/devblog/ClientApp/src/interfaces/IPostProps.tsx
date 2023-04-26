import IComment from "./ICommentProps";

export default interface IPostProps {
    id?: number;
    updateNum?: string;
    description?: string;
    date: Date;
    imgURL?: string;
    comments?: IComment[];
}
