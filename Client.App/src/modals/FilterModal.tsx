import {
    Modal, Box,
    Typography, IconButton, Divider,
    List, ListItemButton, ListItemText, Collapse, Button
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import ExpandLess from '@mui/icons-material/ExpandLess';
import ExpandMore from '@mui/icons-material/ExpandMore';
import { Guid } from 'guid-typescript';
import { Category } from '../types/types';
import useFilters from '../store/useFilters';
import { useEffect, useState } from 'react';

interface FilterModalProps {
    open: boolean;
    handleClose: () => void;
    categories: Category[];
}

export default function FilterModal({ open, handleClose, categories }: FilterModalProps) {
    const [openCategory, setOpenCategory] = useState<{ [key: string]: boolean }>({});

    const [localSelectedCategory, setLocalSelectedCategory] = useState<Guid | null>(null);
    const [localSelectedType, setLocalSelectedType] = useState<Guid | null>(null);

    const { selectedCategory, selectedType, selectCategoryType, applyFilters } = useFilters();

    useEffect(() => {
        if (open) {
            setLocalSelectedCategory(selectedCategory);
            setLocalSelectedType(selectedType);
        }
    }, [open, selectedCategory, selectedType]);

    const handleOpenCategory = (categoryId: Guid) => {
        setOpenCategory((prevState) => ({
            ...prevState,
            [categoryId.toString()]: !prevState[categoryId.toString()],
        }));
    };

    const handleTypeSelect = (categoryId: Guid, typeId: Guid) => {
        setLocalSelectedCategory(categoryId);
        setLocalSelectedType(typeId);
    };

    const handleApplyFilters = () => {
        if (localSelectedCategory && localSelectedType) {
            selectCategoryType(localSelectedCategory, localSelectedType);
        }
        applyFilters();
        handleClose();
    };

    return (
        <Modal open={open} onClose={handleClose}>
            <Box
                sx={{
                    position: 'absolute',
                    top: '50%',
                    left: '50%',
                    transform: 'translate(-50%, -50%)',
                    width: 400,
                    bgcolor: '#2d2d2d',
                    border: '0',
                    boxShadow: 24,
                    p: 2, display: 'flex',
                    flexDirection: 'column',
                    maxHeight: '80vh',
                    overflowY: 'auto',
                }}
            >
                <Box display="flex" justifyContent="space-between" alignItems="center">
                    <Typography
                        variant="h2"
                        fontFamily="Fahkwang, sans-serif"
                        textTransform="uppercase"
                        sx={{ color: 'primary.main', fontWeight: '600', fontSize: '20px' }}
                    >
                        Filter & Sort
                    </Typography>
                    <IconButton onClick={handleClose} sx={{ color: 'primary.main' }}>
                        <CloseIcon />
                    </IconButton>
                </Box>
                <Typography variant="h6" textTransform="uppercase" fontSize="14px" color="primary.light" sx={{ mt: 2 }}>
                    Applied filters
                </Typography>
                <Divider sx={{ my: 2, borderColor: 'primary.light' }} />
                <Typography
                    variant="h2"
                    fontFamily="Fahkwang, sans-serif"
                    textTransform="uppercase"
                    sx={{ mt: 3, color: 'primary.main', fontWeight: '600', fontSize: '20px' }}
                >
                    Filter by
                </Typography>
                <List component="nav">
                    {categories.map((item) => (
                        <Box key={item.id.toString()}>
                            <ListItemButton
                                onClick={() => handleOpenCategory(item.id)}
                                sx={{
                                    padding: 0,
                                    margin: 0,
                                    color: 'primary.main',
                                    borderTop: '1px solid #B5B5B5',
                                    borderBottom: '1px solid #B5B5B5',
                                }}
                            >
                                <ListItemText
                                    primary={item.name}
                                    primaryTypographyProps={{
                                        fontFamily: 'Fahkwang, sans-serif',
                                        textTransform: 'uppercase',
                                        fontWeight: '600',
                                        fontSize: '20px',
                                    }}
                                />
                                {openCategory[item.id.toString()] ? <ExpandLess /> : <ExpandMore />}
                            </ListItemButton>
                            <Collapse in={openCategory[item.id.toString()]} timeout="auto" unmountOnExit>
                                <List component="div" disablePadding>
                                    {item.types.map((type) => (
                                        <ListItemButton
                                            key={type.id.toString()}
                                            onClick={() => handleTypeSelect(item.id, type.id)}
                                            sx={{
                                                color: localSelectedCategory === item.id && localSelectedType === type.id
                                                    ? 'primary.main'
                                                    : 'primary.light',
                                            }}
                                        >
                                            <ListItemText primary={type.name} />
                                        </ListItemButton>
                                    ))}
                                </List>
                            </Collapse>
                        </Box>
                    ))}
                </List>
                <Box mt={2} display="flex" justifyContent="flex-end">
                    <Button
                        variant="outlined"
                        color="primary"
                        onClick={handleApplyFilters}
                        sx={{
                            border: '2px solid',
                            textTransform: 'none',
                            borderRadius: 0
                        }}
                    >
                        Apply
                    </Button>
                </Box>
            </Box>
        </Modal>
    );
}
