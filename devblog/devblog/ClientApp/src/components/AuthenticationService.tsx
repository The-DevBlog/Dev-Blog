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

const GetUserName = () => {
    if (token) {
        const decodedToken: IToken = jwtDecode(token);
        // console.log(decodedToken);
        // console.log("TOKEN " + decodedToken.userName);
        return decodedToken.userName || "";
    }

    return "";
}

export { IsAdmin, IsLoggedIn, GetUserName };