import {useDispatch, useSelector} from "react-redux";
import useFetch from "./UseFetch";
import {IJwtPayload} from "../apis/Interfaces";
import {Logout, ValidateToken} from "../apis/Auth";
import {useCallback, useEffect} from "react";
import {UserStorage} from "../local-storage";
import {IRootState} from "../../modules";
import {getCurrentUserAsync, setUser} from "../../modules/auth";

export default function useAuth() {
    const dispatch = useDispatch();
    const authStore = useSelector((state: IRootState) => state.auth);
    const userStorage = new UserStorage(); 
    
    const [tokenState, validateToken] = useFetch<void, IJwtPayload>(ValidateToken);
    const [logoutState, logout] = useFetch<void, void>(Logout);
    
    // 로그아웃 함수입니다.
    const handleLogout = useCallback(() => {
        const effect = async () => {
            await logout();
            
            // 스토리지 삭제
            userStorage.set(null);
            
            // 스토어 삭제
            dispatch(setUser(null));
        }
        
        effect();
    }, []);
    
    useEffect(() => {
        if (authStore.user === null) {
            // 최초 조회 시, 사용자 정보가 없다면 로컬스토리지와 서버를 조회합니다.
            
            if (userStorage.exists()) {
                // TODO: LocalStorage 검증 방식 확정
                // 로그인 정보가 LocalStorage에 있다면, 토큰을 검증합니다.
                // validateToken();
                
                // LocalStorage에 있다면 그대로 사용합니다.
                dispatch(setUser(userStorage.get()!));
            } else {
                // 아니면 서버에서 현재 사용자를 조회합니다.
                dispatch(getCurrentUserAsync.request());
            }
        }
    }, []);
    
    useEffect(() => {
        // 서버에 사용자 정보가 있다면 사용자를 설정합니다.
        if (authStore.getCurrentUser.data) {
            const user = authStore.getCurrentUser.data; 
            
            dispatch(setUser(user));
            userStorage.set(user)
        }
    }, [authStore.getCurrentUser])
    
    // TODO: LocalStorage 검증 방식 확정
    // useEffect(() => {
    //     // 로컬 스토리지의 정보가 유효하다면 그대로 사용합니다.
    //     if (tokenState.loading === false && tokenState.data) {
    //         const user = userStorage.get();
    //        
    //         if (user !== null && tokenState.data.issuer === user.userNo) {
    //             dispatch(setUser(user))
    //         }
    //     }
    // }, [tokenState])
    
    return {
        loggedIn: authStore.user !== null,
        currentUser: authStore.user,
        tokenState,
        validateToken,
        logout: handleLogout
    }
}