import {FC} from 'react';
import {AppBar, Box, Container, Toolbar} from '@mui/material';
import {Outlet} from 'react-router-dom';

export const Shell: FC = () => {
    return (
        <Container>
            <AppBar>
                <Toolbar>

                </Toolbar>
            </AppBar>
            <Box component={'main'} >
                <Toolbar />
                <Outlet />
            </Box>
        </Container>
    );
}
