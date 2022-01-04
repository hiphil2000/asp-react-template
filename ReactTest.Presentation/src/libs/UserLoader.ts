﻿import {Store} from "@reduxjs/toolkit";
import {IUser} from "./apis/Interfaces";
import {GetCurrentUser} from "./apis/Auth";
import {setUser} from "../modules/auth";
import {StorageHelper} from "./local-storage";
import {USER_KEY} from "./local-storage/StorageHelper";

/**
 * 최초 실행 시 사용자를 불러오기 위한 함수입니다.
 * @param {Store} store Redux Store
 * @return {Promise<void>}
 */
export default async function loadUser(store: Store): Promise<void> {
    const storage = StorageHelper.getInstance();
    let user: IUser | null = null;

    // 서버에서 현재 유저를 조회합니다.
    try {
        user = await GetCurrentUser();
    } catch(e) {
        user = null;
    }

    // 스토어와 LocalStorage에 넣습니다.
    store.dispatch(setUser(user));
    storage.set(USER_KEY, user);
}