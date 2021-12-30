import {IUser} from "../apis/Interfaces";
import {GetCurrentUser} from "../apis/Auth";

const USER_DATA_KEY = "UserData";

export default class UserHelper {
    /**
     * LocalStorage에서 사용자 정보를 조회합니다.
     */
    public static GetUser(): IUser | null {
        const data = localStorage.getItem(USER_DATA_KEY);
        if (data === null) {
            return null;
        }
        
        try {
            return JSON.parse(data) as IUser;
        } catch (e) {
            return null;
        }
    }

    /**
     * LocalStorage에 사용자 정보를 입력합니다.
     * null이라면 삭제합니다.
     * @param user 사용자 정보
     */
    public static SetUser(user: IUser | null): void {
        if (user === null) {
            localStorage.removeItem(USER_DATA_KEY);
        } else {
            localStorage.setItem(USER_DATA_KEY, JSON.stringify(user));
        }
    } 
}