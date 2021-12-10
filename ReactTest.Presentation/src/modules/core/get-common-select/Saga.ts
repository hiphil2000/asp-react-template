import {call, put, takeEvery} from "redux-saga/effects";
import {GET_COMMON_CODE, getCommonCodeAsync} from "./Actions";
import {GetCommonCode, IGetCommonCodeResponse} from "../../../libs/apis/core-bak/GetCommonCode";
import {AxiosError} from "axios";

/**
 * GET_COMMON_CODE 액션에 의해 호출되는 Worker Saga입니다.
 * @param action
 */
function* getCommonCodeWorkerSaga(action: ReturnType<typeof getCommonCodeAsync.request>) {
    try {
        const result: IGetCommonCodeResponse = yield call(GetCommonCode, action.payload);
        yield put(getCommonCodeAsync.success(result));
    } catch (error) {
        yield put(getCommonCodeAsync.failure(error as AxiosError));
    }
}

/**
 *
 */
export function* getCommonCodeSaga() {
    yield takeEvery(GET_COMMON_CODE, getCommonCodeWorkerSaga);
}