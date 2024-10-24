import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AppBar, Toolbar, Typography, Button, Box, SwipeableDrawer, useMediaQuery } from '@mui/material';
import { ABOUT_ROUTE, CATALOG_ROUTE, OFFERS_ROUTE } from '../utils/consts';
import { useAuth0 } from "@auth0/auth0-react";
import MenuIcon from '@mui/icons-material/Menu';
import MenuList from './MenuList';
import { Guid } from 'guid-typescript';
import { getUser, postUser } from '../http/userAPI';
import useUserStore from '../store/useUserStore';

export default function NavBar() {
    const navigate = useNavigate();
    const userStore = useUserStore();
    const [drawer, setDrawer] = useState<boolean>(false);
    const { loginWithRedirect, logout, isAuthenticated, getAccessTokenSilently, user } = useAuth0();

    const isSmallScreen = useMediaQuery('(max-width:37.5em)');

    const toggleDrawer = (open: boolean) => (event: React.MouseEvent) => {
        if (
            event && event.type === 'keydown'
        ) {
            return;
        }

        setDrawer(open);
    };

    const isUserAuthenticated = userStore.user.authId !== '';

    useEffect(() => {
        const getToken = async () => {
            const token = await getAccessTokenSilently();

            if (user) {
                await postUser({
                    id: Guid.EMPTY,
                    authId: user.sub,
                    name: user.name,
                    email: user.email,
                    userPictureUrl: user.picture
                });

                const userData = await getUser(user.sub);
                userStore.setUser({ ...userData, token });
            }
        };

        if (isAuthenticated && user?.sub) {
            getToken();
        }
    }, [isAuthenticated, getAccessTokenSilently]);

    const menuItems = [
        { text: 'Catalog', route: CATALOG_ROUTE },
        { text: 'Offers', route: OFFERS_ROUTE },
        { text: 'About us', route: ABOUT_ROUTE },
    ];

    return (
        <AppBar position="static" sx={{ backgroundColor: '#000' }}>
            <Toolbar>
                <Box display='flex' flexGrow={1}>
                    <Button onClick={() => navigate('/')}>
                        <Typography
                            variant='h5'
                            color='primary'
                            fontFamily='Fahkwang, sans-serif'
                            fontWeight='bold'
                            marginRight={2}
                        >
                            SHORTSHARING
                        </Typography>
                    </Button>
                    <Box display='flex' alignItems='center'>
                        {!isSmallScreen && (
                            <Box display='flex'>
                                {menuItems.map((item, index) => (
                                    <Button
                                        key={index}
                                        onClick={() => navigate(item.route)}
                                        sx={{
                                            color: "primary.light",
                                            textTransform: 'none',
                                            textDecoration: 'none',
                                            '&:hover': {
                                                color: 'primary.main',
                                            },
                                        }}
                                    >
                                        {item.text}
                                    </Button>
                                ))}
                            </Box>
                        )}
                    </Box>
                </Box>
                <Box display='flex' alignItems='center'>
                    {isSmallScreen ? (
                        <>
                            <Button onClick={toggleDrawer(true)} sx={{
                                margin: 0,
                                minWidth: 0,
                                color: "primary.light"
                            }}>
                                <MenuIcon />
                            </Button>
                            <SwipeableDrawer
                                anchor='right'
                                open={drawer}
                                onClose={toggleDrawer(false)}
                                onOpen={toggleDrawer(true)}
                                PaperProps={{
                                    sx: {
                                        backgroundColor: 'secondary.dark'
                                    }
                                }}
                            >
                                <MenuList />
                            </SwipeableDrawer>
                        </>
                    ) : (
                        <>
                            {!isUserAuthenticated && (
                                <Button
                                    sx={{
                                        color: "primary.light",
                                        textTransform: 'none',
                                        ml: 2
                                    }}
                                    onClick={() => loginWithRedirect()}
                                >
                                    Log In
                                </Button>
                            )}
                            {isUserAuthenticated && (
                                <Button
                                    sx={{
                                        color: 'primary.light',
                                        textTransform: 'none',
                                        ml: 2
                                    }}
                                    onClick={() => {
                                        userStore.removeUser();
                                        logout({
                                            logoutParams: {
                                                returnTo: window.location.origin
                                            }
                                        });
                                    }}
                                >
                                    Log Out
                                </Button>
                            )}
                        </>
                    )}
                </Box>
            </Toolbar>
        </AppBar>
    );
}
