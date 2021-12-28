import {useSelector} from "react-redux";
import {loginSelector} from "../../modules/auth/LoginReducer";

export default function useAuth() {
    const login = useSelector(loginSelector);
    
    return {
        loggedIn: login.data?.user != null,
        currentUser: login.data?.user
    }
}