import IToken from "../interfaces/IToken";
import jwtDecode from "jwt-decode";

const token = localStorage.getItem("token")!;

const IsAdmin = () => {
    if (token) {
        const decodedToken: IToken = jwtDecode(token);
        return decodedToken.role === "Admin";
    }

    return false;
}

const IsLoggedIn = () => {
    return token != null;
}

export { IsAdmin, IsLoggedIn };