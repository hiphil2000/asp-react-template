import * as Actions from "./Actions";
import {ActionType} from "typesafe-actions";
import {AsyncState} from "../../ReducerUtils";
import {IGetCommonCodeResponse} from "../../../libs/apis/core-bak/GetCommonCode";

/**
 * GetCommonCode 요청의 ActionType 입니다.
 */
export type GetCommonCodeAction = ActionType<typeof Actions>;

/**
 * GetCommonCode 요청의 AsyncState 입니다.
 */
export type GetCommonCodeState = {
    data: AsyncState<IGetCommonCodeResponse, Error>;
}