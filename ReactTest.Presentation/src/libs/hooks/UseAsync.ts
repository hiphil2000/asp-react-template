import {useCallback, useReducer, useState, ReducerAction, Reducer} from "react";
import {AxiosError} from "axios";

// interface IAsyncState<TData> {
//     loading: boolean;
//     data: TData | null;
//     error: AxiosError | null;
// }
//
//
//
//
// function reducer<TData>(state: IAsyncState<TData>, action: IAction<TData>): IAsyncState<TData> {
//     switch(action.type) {
//         case LOADING:
//             return {
//                 ...state,
//                 loading: true
//             }
//         case SUCCESS:
//             return {
//                 ...state,
//                 loading: false,
//                 data: action.payload as TData
//             }
//         case ERROR:
//             return {
//                 ...state,
//                 loading: false,
//                 error: action.payload as AxiosError
//             }
//         default:
//             return state;
//     }
// }
//
//
// export default function useAsync<TPayload, TResult>(
//     action: (param: TPayload) => Promise<TResult>, 
//     payload: TPayload
// ) {
//     const [state, dispatch] = useReducer<typeof reducer, IAction<TResult>, >(reducer, initializeState);
//    
//     const fetchData = useCallback(async () => {
//         dispatch({type: LOADING});
//         try {
//             const response = await action(payload);
//             dispatch({type: SUCCESS, payload: response});
//         } catch (e) {
//             dispatch({type: ERROR, payload: e as AxiosError});
//         }
//     }, [payload]);
//    
//     return [
//         state,
//         fetchData
//     ]
// }

export interface IAsyncState<TData> {
    loading: boolean;
    data: TData | null;
    error: AxiosError | null;
}

const LOADING: string = "ASYNC_LOADING";
const SUCCESS: string = "ASYNC_SUCCESS";
const ERROR: string = "ASYNC_ERROR";

export interface IAction<TData> {
    type: string,
    payload?: TData | AxiosError
}

const initializeState: IAsyncState<any> = {
    loading: false,
    data: null,
    error: null
}

export default function useAsync<TPayload, TResult>(
    api: (payload: TPayload) => Promise<TResult>,
    payload: TPayload
): [IAsyncState<TResult>, () => Promise<void>] {
    // 동적으로 리듀서를 생성합니다.
    type reducerType = (state: IAsyncState<TResult>, action: IAction<TResult>) => IAsyncState<TResult>;
    const reducer = useCallback<reducerType>((state, action) => {
        switch(action.type) {
            case LOADING:
                return {
                    ...state,
                    loading: true
                }
            case SUCCESS:
                return {
                    ...state,
                    loading: false,
                    data: action.payload as TResult
                }
            case ERROR:
                return {
                    ...state,
                    loading: false,
                    error: action.payload as AxiosError
                }
            default:
                return state;
        }
    }, [api]);

    // useReducer 사용
    const [state, dispatch] = useReducer(reducer, initializeState);

    // Api를 호출할 수 있는 함수를 생성합니다.
    const fetchData = useCallback(async () => {
        dispatch({type: LOADING});
        try {
            const response = await api(payload);
            dispatch({type: SUCCESS, payload: response});
        } catch (e) {
            dispatch({type: ERROR, payload: e as AxiosError});
        }
    }, [payload]);
    
    // 결과를 반환합니다.
    return [state, fetchData];
}