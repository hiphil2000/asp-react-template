import Axios from "axios";
import {getCommonCodePath} from "./Path";

export interface ICommonCode {
    groupId: string;
    codeId: string;
    codeName: string;
}

/**
 * GetCommonCode 요청의 페이로드 타입입니다.
 */
export interface IGetCommonCodePayload {
    groupId: string;
}

/**
 * GetCommonCode 요청의 반환 타입입니다.
 */
export interface IGetCommonCodeResponse { 
    groupId: string;
    codeList: ICommonCode[];
}

/**
 * CommonCode를 조회합니다.
 * @param payload 조회 페이로드
 * @constructor
 */
export async function GetCommonCode(payload: IGetCommonCodePayload): Promise<IGetCommonCodeResponse> {
    const response = await Axios.get<IGetCommonCodeResponse>(getCommonCodePath, {
        params: {
            ...payload
        }
    });

    return response.data;
}