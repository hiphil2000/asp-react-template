import React from "react";
import {AppBar, Container, Toolbar, Typography} from "@mui/material";

interface INavBarProps {
    children?: React.ReactNode;
}

export default function NavBar({
    children
}: INavBarProps) {
    return (
        <AppBar position="static">
            <Container maxWidth="xl">
                <Toolbar disableGutters={true}>
                    {children}
                </Toolbar>
            </Container>
        </AppBar>
    );
}