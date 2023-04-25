import IComment from "./ICommentProps";

export default interface IPostProps {
    id?: number;
    updateNum?: string;
    description?: string;
    date?: string;
    imgURL?: string;
    comments?: IComment[];
}
