import Axios from "axios";
import apiPaths from "./Paths";
import {IUser} from "./Interfaces";

export interface ILoginPayload {
    userId: string,
    password: string
}

export interface ILoginResponse {
    success: boolean;
    user?: IUser;
    token?: string;
    message?: string;
}

export default async function Login(payload: ILoginPayload): Promise<ILoginResponse> {
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