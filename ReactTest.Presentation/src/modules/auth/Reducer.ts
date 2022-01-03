﻿import {createReducer} from "typesafe-actions";
import {AuthAction, AuthState} from "./Types";
import {
    getCurrentUserAsync,
    REQUEST_LOGIN,
    REQUEST_LOGIN_FAILURE,
    REQUEST_LOGIN_SUCCESS,
    requestLoginAsync,
    SET_USER
} from "./Actions";
import {asyncStateHelper, createAsyncReducer} from "../ReducerUtils";

const initialState: AuthState = {
    user: null,
    getCurrentUser: asyncStateHelper.initial(),
    requestLogin: asyncStateHelper.initial()
}

/**
 * 현재 사용자 조회 액션의 리듀서 핸들러입니다.
 * @type {(state: AuthState, action: AnyAction) => AuthState}
 */
const getCurrentUserHandler = createAsyncReducer<AuthState, typeof getCurrentUserAsync, keyof AuthState>(getCurrentUserAsync, "getCurrentUser");
/**
 * 로그인 요청 액션의 리듀서 핸들러입니다.
 * @type {(state: AuthState, action: AnyAction) => AuthState}
 */
const loginHandler = createAsyncReducer<AuthState, typeof requestLoginAsync, keyof AuthState>(requestLoginAsync, "requestLogin");

const authReducer = createReducer<AuthState, AuthAction>(initialState, {
    [SET_USER]: (state, action) => ({
        ...state,
        user: action.payload
    }),
    ...getCurrentUserHandler,
    ...loginHandler
})

export default authReducer;