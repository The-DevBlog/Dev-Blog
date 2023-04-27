import { JwtPayload } from "jwt-decode";

export default interface Token extends JwtPayload {
    userName: string,
    email: string,
    role: string
}