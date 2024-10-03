import { Card, CardContent, Typography, Button, Box } from '@mui/material';

export default function SkeletonItemCard() {
    return (
        <Card
            sx={{
                borderRadius: '0',
                width: '16.5rem',
                height: '22.5rem',
                backgroundColor: '#1c1c1c',
            }}
        >
            <CardContent>
                <Box display='flex'>
                    <Typography
                        mt={1}
                    >
                    </Typography>
                    <Button
                        sx={{
                            marginTop: '0.125rem',
                            ml: '3rem',
                        }}
                    >
                    </Button>
                </Box>
            </CardContent>
        </Card>
    );
}
