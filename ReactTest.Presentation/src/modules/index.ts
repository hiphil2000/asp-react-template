import {all} from "redux-saga/effects";
import LoginReducer, {ILoginState, loginSaga} from "./auth/LoginReducer";

// 모든 리듀서의 목록입니다.
export const reducers = {
    LoginReducer,
}

// 스토어의 RootState 인터페이스입니다.
export interface IRootState {
    LoginReducer: ILoginState,
}

export function* rootSaga() {
    yield all([
        loginSaga(),
    ]);
}