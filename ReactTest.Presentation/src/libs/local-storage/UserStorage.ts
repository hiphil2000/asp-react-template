import {IUser} from "../apis/Interfaces";
import {IStorageHelper} from "./Interfaces";

export default class UserStorage implements IStorageHelper<IUser> {
    private static readonly USER_KEY = "UserData";
    
    constructor() {
    }

    /**
     * 사용자 데이터를 조회합니다.
     * 존재하지 않는다면 null을 반환합니다.
     * @return {IUser | null} 사용자 정보
     */
    public get(): IUser | null {
        const data = localStorage.getItem(UserStorage.USER_KEY);

        return data ?
            JSON.parse(data) :
            null;
    }

    /**
     * 사용자 정보를 저장합니다.
     * 만약 null이라면 정보를 삭제합니다.
     * @param {IUser | null} data 사용자 정보
     */
    public set(data: IUser | null): void {
        if (data != null) {
            localStorage.setItem(UserStorage.USER_KEY, JSON.stringify(data));
        } else {
            localStorage.removeItem(UserStorage.USER_KEY);
        }
    }
    
    /**
     * 사용자 정보 존재 여부를 확인합니다.
     * @return {boolean} 사용자 정보 존재 여부입니다.
     */
    public exists(): boolean {
        return this.get() !== null;
    }
}