import { Box, Typography } from "@mui/material";
import notFound from '../assets/no-image.jpeg';

export default function NotFound() {
    return (
        <Box
            display='flex'
            flexDirection='column'
            justifyContent='center'
            alignItems='center'
        >
            <Typography
                variant='h5'
                color='primary'
                fontFamily='Fahkwang, sans-serif'
                fontWeight='bold'
                sx={{ mt: 8 }}
            >
                Not Found!
            </Typography>
            <img src={notFound} width='400em' />
        </Box>
    )
}
