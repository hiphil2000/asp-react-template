import Axios from "axios";
import apiPaths from "./Paths";

export interface ILoginPayload {
    id: string,
    password: string
}

export interface ILoginResponse {
    success: boolean;
    token?: string;
    message?: string;
}

export async function Login(payload: ILoginPayload): Promise<ILoginResponse> {
    const response = await Axios.post<ILoginResponse>(apiPaths.Login, payload);
    
    return response.data;
}