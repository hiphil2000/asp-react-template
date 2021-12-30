import {useDispatch, useSelector} from "react-redux";
import {loginSelector} from "../../modules/auth/LoginReducer";
import useFetch from "./UseFetch";
import {IJwtPayload} from "../apis/Interfaces";
import {ValidateToken} from "../apis/Auth";
import {useEffect} from "react";
import {getUserAsync} from "../../modules/auth/UserReducer";

export default function useAuth() {
    const dispatch = useDispatch();
    const login = useSelector(loginSelector);
    const [state, validateToken] = useFetch<void, IJwtPayload>(ValidateToken);
    
    useEffect(() => {
        if (login.data != null) {
            validateToken();
        } else {
            dispatch(getUserAsync.request());
        }
    }, []);
    
    return {
        loggedIn: login.data?.user != null,
        currentUser: login.data?.user,
        state,
        validateToken
    }
}