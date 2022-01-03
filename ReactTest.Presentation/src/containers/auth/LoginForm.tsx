import React, {useEffect, useState} from "react";
import {Button, styled, TextField, Typography} from "@mui/material";
import {useDispatch, useSelector} from "react-redux";
import {ILoginPayload} from "../../libs/apis/Auth";
import {useHistory} from "react-router";
import {loginSelector} from "../../modules/auth/Seletors";
import {requestLoginAsync} from "../../modules/auth";

export interface ILoginFormProps {
    
}

export default function LoginForm({
    
}: ILoginFormProps) {
    const dispatch = useDispatch();
    const history = useHistory();
    const login = useSelector(loginSelector);
        
    const [formState, setFormState] = useState<ILoginPayload>({
        userId: "",
        password: ""
    });
    
    const handleLogin = () => {
        dispatch(requestLoginAsync.request(formState));
    }
    
    const handleInput = (e: React.FormEvent<HTMLInputElement>) => {
        const target = e.target as HTMLInputElement; 
        const id = target.id;
        const value = target.value;
        
        // 유효한 입력이면 State를 업데이트합니다.
        if (Object.keys(formState).includes(id)) {
            setFormState(prev => ({
                ...prev,
                [id]: value
            }))
        }
    }
    
    useEffect(() => {
        console.log(login);
        
        if (login.loading === false && login.data?.success === true) {
            history.push("/");
        }
    }, [login]);
    
    return (
        <FormContainer>
            <Typography variant="h5">Login</Typography>
            <TextField id="userId" 
                       value={formState.userId}
                       onInput={handleInput}
            />
            <TextField id="password" type="password" 
                       value={formState.password}
                       onInput={handleInput}
            />
            <Button id="loginButton" onClick={handleLogin} disabled={login.loading}>Login</Button>
        </FormContainer>
    )
}

const FormContainer = styled("form")({
    display: "flex",
    flexDirection: "column",
    flex: 1,
    margin: "1em",
    "& > *": {
        flex: 1
    }
});