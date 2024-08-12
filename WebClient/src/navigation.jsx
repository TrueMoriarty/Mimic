import {
    AppBar,
    Avatar,
    Button,
    Grid,
    Tab,
    Tabs,
    Typography,
} from '@mui/material';
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import useRouteMatch from './hooks/useRouteMatch';
import { getAsync } from './axios';

const init_user = {
    isAuth: false,
};

const Navigation = () => {
    const [user, setUser] = useState(init_user);
    const routeMatch = useRouteMatch([
        '/rooms',
        '/library',
        '/workshop',
        '/characters',
    ]);
    const currentTab = routeMatch?.pattern?.path;

    useEffect(() => {
        (async () => {
            const { isOk, data } = await getAsync(
                'https://localhost/api/users/info'
            );
            isOk && setUser({ ...data, isAuth: true });
        })();
    }, [currentTab]);

    return (
        <AppBar position='static' color='mimic'>
            <Grid
                container
                direction='row'
                justifyContent='space-between'
                alignItems='center'
                sx={{ m: 0 }}
            >
                <Grid item sx={{ ml: 1 }}>
                    <Grid container spacing={1}>
                        <Grid item xs sx={{ my: 'auto' }}>
                            <Typography
                                variant='h4'
                                to='/'
                                align='center'
                                color='mimic.contrastText'
                                component={Link}
                                sx={{
                                    textDecoration: 'none',
                                    boxShadow: 'none',
                                }}
                            >
                                Mimic
                            </Typography>
                        </Grid>
                        <Grid item>
                            <Tabs
                                value={currentTab}
                                sx={{ mb: 0.5 }}
                                textColor='mimic'
                                TabIndicatorProps={{
                                    style: {
                                        background: '#6E8190',
                                        height: '3px',
                                    },
                                }}
                            >
                                <Tab
                                    label={<Typography>Персонажи</Typography>}
                                    value='/characters'
                                    to='/characters'
                                    component={Link}
                                />
                                <Tab
                                    label={<Typography>Комнаты</Typography>}
                                    value='/rooms'
                                    to='/rooms'
                                    component={Link}
                                />
                                <Tab
                                    label={<Typography>Мастерская</Typography>}
                                    value='/workshop'
                                    to='/workshop'
                                    component={Link}
                                />
                                <Tab
                                    label={<Typography>Библиотека</Typography>}
                                    value='/library'
                                    to='/library'
                                    component={Link}
                                />
                            </Tabs>
                        </Grid>
                    </Grid>
                </Grid>
                <Grid item sx={{ mr: 1 }}>
                    {user.isAuth ? (
                        <Grid container spacing={1}>
                            <Grid item>
                                <Avatar alt='user' />
                            </Grid>
                            <Grid item sx={{ my: 'auto' }}>
                                <Typography variant='h6'>
                                    {user?.userName}
                                </Typography>
                            </Grid>
                        </Grid>
                    ) : (
                        <Button
                            color='inherit'
                            to='/user/login'
                            component={Link}
                        >
                            <Typography variant='h6'>Login</Typography>
                        </Button>
                    )}
                </Grid>
            </Grid>
        </AppBar>
    );
};

export default Navigation;
