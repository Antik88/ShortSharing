import { Card, CardContent, Typography, Button, Box } from '@mui/material';

export default function SkeletonItemCard() {
    return(
        <Card
            sx={{
                borderRadius: '0',
                width: '264px',
                height: '360px',
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
                            marginTop: '2px',
                            ml: 5,
                        }}
                    >
                    </Button>
                </Box>
            </CardContent>
        </Card>
    )
}
