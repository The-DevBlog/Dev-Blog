import IToken from "../interfaces/IToken";
import jwtDecode from "jwt-decode";

const token = localStorage.getItem("token")!;

const GetIsAdmin = () => {
    if (token) {
        const decodedToken: IToken = jwtDecode(token);
        return decodedToken.role === "Admin";
    }

    return false;
}

const IsLoggedIn = () => {
    return token != null;
}

const GetUserName = () => {
    if (token) {
        const decodedToken: IToken = jwtDecode(token);
        return decodedToken.userName || "";
    }

    return "";
}

export { GetIsAdmin, IsLoggedIn, GetUserName };