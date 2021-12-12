import Axios from "axios";
import apiPaths from "./Paths";

export interface ILoggableItem {
    createDateTime: Date;
    createUserId: number;
    updateDateTime: Date | null;
    updateUserId: number | null;
}


export interface ICommonGroup extends ILoggableItem {
    groupId: string;
    groupName: string | null;
    description: string | null;
    useYn: boolean;
} 

export interface ICommonCode extends ILoggableItem {
    groupId: string;
    codeId: string;
    codeName: string;
    description: string | null;
    displayOrder: number;
    custom1: string | null;
    custom2: string | null;
    custom3: string | null;
    custom4: string | null;
    custom5: string | null;
    useYn: boolean;
}

export interface IGetCommonCodeResponse {
    groupId: string;
    codeList: ICommonCode[];
}

/**
 * GroupId에 해당하는 CommonCode를 조회합니다.
 * @param groupId CommonCode의 GroupId
 * @constructor
 */
export async function GetCommonCodes(groupId: string): Promise<ICommonCode[]> {
    const response = await Axios.get<IGetCommonCodeResponse>(apiPaths.GetCommonCodes, {
        params: {
            groupId
        }
    });
    
    return response.data?.codeList;
}

/**
 * CommonGroup 목록을 조회합니다.
 * @constructor
 */
export async function GetCommonGroups(): Promise<ICommonGroup[]> {
    const response = await Axios.get<ICommonGroup[]>(apiPaths.GetCommonGroups);
    
    return response.data;
} 