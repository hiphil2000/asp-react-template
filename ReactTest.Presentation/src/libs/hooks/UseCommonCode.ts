import useAsync from "./UseAsync";
import {GetCommonCodes, ICommonCode} from "../apis/Core";
import {useEffect} from "react";

export default function useCommonCode(groupId: string) {
    const [state, fetch] = useAsync<string, ICommonCode[]>(GetCommonCodes, groupId);
    
    useEffect(() => {
        fetch();
    }, [fetch]);
    
    return [state];
}