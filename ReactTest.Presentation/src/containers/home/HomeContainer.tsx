import React, {useEffect} from "react";
import CommonCodeSelect from "../../components/core/CommonCodeSelect";
import useCommonCodeSelect from "../../libs/hooks/UseCommonCodeSelect";
import useCommonCode from "../../libs/hooks/UseCommonCode";
import {ICommonCode} from "../../libs/apis/Core";

interface IHomeContainerProps {
    
}

export default function HomeContainer({
    
}: IHomeContainerProps) {
    // const g1_code = useCommonCodeSelect("G001", "001");
    // const g2_code = useCommonCodeSelect("G002", "001");
    // const g1_code2 = useCommonCodeSelect("G001", "001");
    
    const [g001] = useCommonCode("G001");
    const [g002] = useCommonCode("G002");
    
    useEffect(() => {
        if (g001.data !== null) {
            console.log(g001.data);
        }
    }, [g001]);
    
    const render = (datas: ICommonCode[]) => {
        return (
            datas.map(data => (
                <option value={data.codeId}>{data.codeName}</option>
            ))
        )
    }
    
    return (
        <div>
            <h3>Home</h3>
            <select>
                {g001.data !== null && render(g001.data)}
            </select>
            {/*<CommonCodeSelect {...g1_code}/>*/}
            {/*<CommonCodeSelect {...g2_code}/>*/}
            {/*<CommonCodeSelect {...g1_code2}/>*/}
        </div>
    )
}