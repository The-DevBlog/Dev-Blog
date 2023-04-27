export default interface ICommentProps {
    id?: number,
    content?: string;
    date: Date;
    userName?: string;
    handleCommentChange: () => void
}
