import { useEffect, useState } from 'react';
import { Card, CardMedia, CardContent, Typography, Button, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { THING_ROUTE } from '../utils/consts';
import { CatalogItem } from '../types/types';
import { getImageUrl } from '../http/catalogAPI';
import noImage from '../assets/no-image.jpeg';

export default function ItemCard({ item }: { item: CatalogItem }) {
    const [imageUrl, setImageUrl] = useState<string>(noImage);

    const navigate = useNavigate();

    useEffect(() => {
        const fetchImageUrl = async () => {
            if (item.images.length > 0) {
                try {
                    const url = await getImageUrl(item.images[0].name);
                    setImageUrl(url);
                } catch (error) {
                    setImageUrl(noImage);
                }
            } else {
                setImageUrl(noImage);
            }
        };

        fetchImageUrl();
    }, [item.images]);

    return (
        <Card
            sx={{
                borderRadius: '0',
                width: '16.5rem',
                height: '22.5rem',
                backgroundColor: '#1c1c1c',
                color: 'primary.contrastText'
            }}
        >
            <CardMedia
                component="img"
                image={imageUrl}
                alt={item.name}
                style={{
                    width: '100%',
                    height: '14.8rem',
                }}
            />
            <CardContent>
                <Typography
                    color="primary"
                    fontSize='0.875rem'
                >
                    {item.name}
                </Typography>
                <Typography
                    color="primary.light"
                    fontSize='0.8125rem'
                    minHeight='3.2em'
                >
                    {item.description.length > 60 ? item.description.slice(0, 60) + '...' : item.description}
                </Typography>
                <Box display='flex'>
                    <Typography
                        fontFamily='Fahkwang, sans-serif'
                        fontWeight='bold'
                        mt={1}
                    >
                        {item.price} USD
                    </Typography>
                    <Button
                        sx={{
                            marginTop: '0.125rem',
                            ml: '3rem',
                            color: 'primary',
                            textTransform: 'none',
                            fontWeight: 'bold',
                        }}
                        onClick={() => navigate(THING_ROUTE + '/' + item.id)}
                    >
                        See more
                    </Button>
                </Box>
            </CardContent>
        </Card>
    );
};
