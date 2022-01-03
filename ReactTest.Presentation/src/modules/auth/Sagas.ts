import {GET_CURRENT_USER, getCurrentUserAsync, REQUEST_LOGIN, requestLoginAsync} from "./Actions";
import {GetCurrentUser, ILoginResponse, Login} from "../../libs/apis/Auth";
import {call, put, takeLatest, takeEvery} from "redux-saga/effects";
import {AxiosError} from "axios";
import {IUser} from "../../libs/apis/Interfaces";

/**
 * GetCurrentUser Saga입니다.
 */
function* _GetCurrentUserSaga() {
    try {
        const result: IUser = yield call(GetCurrentUser);
        yield put(getCurrentUserAsync.success(result))
    } catch (e) {
        yield put(getCurrentUserAsync.failure(e as AxiosError))
    }
}

/**
 * RequestLogin Saga입니다.
 */
function* _RequestLoginSaga(action: ReturnType<typeof requestLoginAsync.request>) {
    try {
        const result: ILoginResponse = yield call(Login, action.payload);
        yield put(requestLoginAsync.success(result))
    } catch (e) {
        yield put(requestLoginAsync.failure(e as AxiosError))
    }
}

/**
 * GetCurrentUser Wrapper Saga입니다.
 */
export function* currentUserSaga() {
    yield takeLatest(GET_CURRENT_USER, _GetCurrentUserSaga); 
}

/**
 * RequestLogin Wrapper Saga입니다. 
 */
export function* requestLoginSaga() {
    yield takeEvery(REQUEST_LOGIN, _RequestLoginSaga); 
}