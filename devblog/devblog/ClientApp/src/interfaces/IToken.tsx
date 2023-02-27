import { JwtPayload } from "jwt-decode";

export default interface Token extends JwtPayload {
    username: string,
    email: string,
    role: string
}