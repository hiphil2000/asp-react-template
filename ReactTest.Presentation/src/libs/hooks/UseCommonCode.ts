import useFetch from "./UseFetch";
import {GetCommonCodes, ICommonCode} from "../apis/Core";
import {useEffect} from "react";

export default function useCommonCode(groupId: string) {
    const [state, fetch] = useFetch<string, ICommonCode[]>(GetCommonCodes);
    
    useEffect(() => {
        fetch(groupId);
    }, [fetch, groupId]);
    
    return [state];
}