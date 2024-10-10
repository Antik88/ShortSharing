import { useState } from 'react';
import { Box } from '@mui/material';
import { Image } from '../types/types';
import { buildImageUrl } from '../http/catalogAPI';

interface ImageGalleryProps {
    images: Image[];
}

export default function ImageGallery({ images }: ImageGalleryProps) {
    const [selectedImageIndex, setSelectedImageIndex] = useState<number>(0);

    const handleImageClick = (index: number) => {
        setSelectedImageIndex(index);
    };

    return (
        <Box sx={{ display: 'flex', flexDirection: 'row', mb: 2 }}>
            <Box
                sx={{
                    width: '30vh',
                    direction: 'rtl',
                    maxHeight: '60vh',
                    overflow: 'auto',
                    scrollbarWidth: 'thin',
                    scrollbarColor: '#ffca8b #202020',
                    msScrollbarArrowColor: "white",
                    mr: 1
                }}
            >
                <Box sx={{ ml: 1, display: 'flex', flexDirection: 'column' }}>
                    {images.length > 1 && images.map((image, index) => (
                        <Box
                            key={index}
                            sx={{
                                marginBottom: '10px',
                                cursor: 'pointer',
                                opacity: selectedImageIndex === index ? 1 : 0.4,
                            }}
                            onClick={() => handleImageClick(index)}
                        >
                            <img
                            src={buildImageUrl(image.name)}
                            alt={`Image ${index + 1}`}
                            style={{ width: '100%' }}
                            />
                        </Box>
                    ))}
                </Box>
            </Box>
            <Box sx={{ width: '100%', display: 'flex', justifyContent: 'center' }}>
                <img
                    src={buildImageUrl(images[selectedImageIndex].name)}
                    alt={`Selected ${selectedImageIndex + 1}`}
                    style={{ width: '100%' }}
                />
            </Box>
        </Box>
    );
};
