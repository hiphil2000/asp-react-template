import Axios from "axios";
import apiPaths from "./Paths";
import {IJwtPayload, IUser} from "./Interfaces";

/**
 * 로그인 요청의 페이로드 타입입니다.
 */
export interface ILoginPayload {
    userId: string,
    password: string
}

/**
 * 로그인 요청의 반환 타입입니다.
 */
export interface ILoginResponse {
    success: boolean;
    user: IUser | null;
    token?: string;
    message?: string;
}

/**
 * 로그인을 진행하고 토큰을 설정합니다.
 * @param payload 로그인 정보
 * @constructor
 */
export async function Login(payload: ILoginPayload): Promise<ILoginResponse> {
    const response = await Axios.request<ILoginResponse>({
        url: apiPaths.Login,
        method: "POST",
        data: {
            ...payload
        },
        withCredentials: true
    });
    
    return response.data;
}

/**
 * 현재 사용자의 정보를 조회합니다.
 * @constructor
 */
export async function GetCurrentUser(): Promise<IUser> {
    const response = await Axios.get<IUser>(apiPaths.GetCurrentUser);
    
    return response.data;
}

/**
 * 현재 설정된 쿠키가 유효한지 확인합니다.
 * @constructor
 */
export async function ValidateToken(): Promise<IJwtPayload> {
    const response = await Axios.post<IJwtPayload>(apiPaths.ValidateToken);
    
    return response.data;
}

/**
 * 로그아웃을 처리합니다.
 * @return {Promise<void>}
 * @constructor
 */
export async function Logout(): Promise<void> {
    await Axios.post(apiPaths.Logout);
}