import { Box, Button, Typography } from "@mui/material";
import { useAuth0 } from "@auth0/auth0-react";
import { useNavigate } from "react-router-dom";
import { ABOUT_ROUTE, CATALOG_ROUTE, OFFERS_ROUTE } from "../utils/consts";
import { MenuButton } from "../styled/MenuButton";

const menuItems = [
    { text: 'Catalog', route: CATALOG_ROUTE },
    { text: 'Offers', route: OFFERS_ROUTE },
    { text: 'About us', route: ABOUT_ROUTE },
];

export default function MenuList() {
    const navigate = useNavigate();

    const { loginWithRedirect, logout, isAuthenticated } = useAuth0();

    return (
        <Box sx={{ width: 250, backgroundColor: "#202020" }}>
            <Box display='flex' flexDirection='column'>
                {menuItems.map((item, index) => (
                    <Button onClick={() => navigate(item.route)} key={index}>
                        <Typography>
                            {item.text}
                        </Typography>
                    </Button>
                ))}
                <Box>
                    {!isAuthenticated && (
                        <MenuButton onClick={() => loginWithRedirect()} >
                            Log In
                        </MenuButton>
                    )}
                    {isAuthenticated && (
                        <MenuButton
                            onClick={() => logout({
                                logoutParams: {
                                    returnTo: window.location.origin
                                }
                            })}
                        >
                            Log Out
                        </MenuButton>
                    )}
                </Box>
            </Box>
        </Box>
    );
}