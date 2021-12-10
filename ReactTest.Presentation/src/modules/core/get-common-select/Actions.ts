import {createAsyncAction} from "typesafe-actions";
import {AxiosError} from "axios";
import {IGetCommonCodePayload, IGetCommonCodeResponse} from "../../../libs/apis/core-bak/GetCommonCode";

// Request UserData from API
export const GET_COMMON_CODE = 'core/GET_COMMON_CODE';
export const GET_COMMON_CODE_SUCCESS = 'core/GET_COMMON_CODE_SUCCESS';
export const GET_COMMON_CODE_ERROR = 'core/GET_COMMON_CODE_ERROR';
export const getCommonCodeAsync = createAsyncAction(
    GET_COMMON_CODE,
    GET_COMMON_CODE_SUCCESS,
    GET_COMMON_CODE_ERROR
)<IGetCommonCodePayload, IGetCommonCodeResponse, AxiosError>();