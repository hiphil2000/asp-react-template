import Axios from "axios";
import apiPaths from "./Paths";
import {IUser} from "./Interfaces";

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
    user?: IUser;
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
 * 현재 사용자 조회 요청의 반환 타입입니다.
 */
export interface IGetCurrentUserResponse {
    success: boolean;
    user?: IUser;
    message?: string;
}

/**
 * 현재 사용자의 정보를 조회합니다.
 * @constructor
 */
export async function GetCurrentUser(): Promise<IGetCurrentUserResponse> {
    const response = await Axios.get<IGetCurrentUserResponse>(apiPaths.GetCurrentUser);
    
    return response.data;
}