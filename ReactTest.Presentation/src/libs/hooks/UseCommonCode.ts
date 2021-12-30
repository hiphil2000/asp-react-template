import useFetch from "./UseFetch";
import {GetCommonCodes, ICommonCode} from "../apis/Core";
import React, {useEffect, useState} from "react";
import {useSelector} from "react-redux";

export default function useCommonCode(groupId: string) {
    const [resultList, fetch] = useFetch<string, ICommonCode[]>(GetCommonCodes);
    const [value, setValue] = useState<string>("");
    
    useEffect(() => {
        fetch(groupId);
    }, [fetch, groupId]);
    
    const onChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const target = e.target as HTMLSelectElement;
        setValue(target.value);
    }
    
    return {
        resultList,
        value,
        onChange
    };
}