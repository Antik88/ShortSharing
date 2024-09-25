import { useEffect, useState } from "react";
import { Badge, Box, Button, Container, Pagination, Typography } from "@mui/material";
import Grid from '@mui/material/Grid2';
import { getCatalog, getCategories } from "../http/catalogAPI";
import { CatalogResponse, Category } from "../types/types";
import { useNavigate } from "react-router-dom";
import ItemCard from "../components/ItemCard";
import SkeletonItemCard from "../components/SkeletonItemCard";
import useModal from "../hooks/useModal";
import useFilters from "../store/useFilters";
import ClearIcon from '@mui/icons-material/Clear';
import TuneIcon from '@mui/icons-material/Tune';
import AddIcon from '@mui/icons-material/Add';
import FilterModal from "../modals/FilterModal";
import NotFound from "../components/NotFound";
import { POSTTHING_ROUTE } from "../utils/consts";

export default function CatalogPage() {
    const [catalog, setCatalog] = useState<CatalogResponse | null>(null);
    const [categories, setCategories] = useState<Category[]>([]);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const filterModal = useModal();
    const navigate = useNavigate();

    const { applied, clearFilters, selectedCategory, selectedType } = useFilters();

    useEffect(() => {
        if (!catalog) {
            getCatalog({
                categoryId: selectedCategory,
                typeId: selectedType,
                pageNumber: currentPage
            }).then(data => setCatalog(data));
        }
        if (categories.length === 0) {
            getCategories().then(data => setCategories(data));
        }
    }, [selectedCategory, selectedType, currentPage]);

    const handlePageChange = (_event: React.ChangeEvent<unknown>, value: number) => {
        setCurrentPage(value);
    };

    return (
        <Box
            minHeight='90vh'
            display="flex"
            flexDirection="column"
        >
            <Container sx={{ flexGrow: 1 }}>
                <Box sx={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                }}>
                    <Box>
                        <Typography variant="h4" color="primary" marginBottom='20px'>
                            Catalog
                        </Typography>
                    </Box>
                    <Box>
                        {applied && (
                            <Button
                                color="primary"
                                onClick={clearFilters}
                                variant="outlined"
                                sx={{
                                    border: '3px solid',
                                    borderRadius: '0',
                                    mr: 2
                                }}
                            >
                                <Typography
                                    sx={{
                                        fontSize: '14px',
                                        textTransform: 'none',
                                    }}
                                >
                                    Clear
                                </Typography>
                                <ClearIcon />
                            </Button>
                        )}
                        <Badge
                            color="secondary"
                            variant="dot"
                            invisible={!applied}
                            overlap="circular"
                            anchorOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                        >
                            <Button
                                variant="outlined"
                                sx={{
                                    border: '3px solid',
                                    borderRadius: '0',
                                    position: 'relative',
                                }}
                                onClick={filterModal.open}
                            >
                                <Typography
                                    sx={{
                                        fontSize: '14px',
                                        textTransform: 'none',
                                    }}
                                >
                                    Filter & Sort
                                </Typography>
                                <TuneIcon sx={{ ml: 1 }} />
                            </Button>
                        </Badge>
                        <Button
                            variant="outlined"
                            sx={{
                                border: '3px solid',
                                borderRadius: '0',
                                ml: 2
                            }}
                            onClick={() => navigate(POSTTHING_ROUTE)}
                        >
                            <AddIcon />
                        </Button>
                    </Box>
                </Box>
                {catalog?.items.length === 0 ? (
                    <NotFound />
                ) : (
                    <Grid container spacing={4}>
                        {catalog === null ? (
                            Array.from({ length: 8 }).map((_, index) => (
                                <Grid key={index}>
                                    <SkeletonItemCard />
                                </Grid>
                            ))
                        ) : (
                            catalog.items.map((item) => (
                                <Grid key={item.id.toString()}>
                                    <ItemCard item={item} />
                                </Grid>
                            ))
                        )}
                    </Grid>
                )}
            </Container>
            {catalog?.totalPages === 1 ? (
                <></>
            ) : (
                <Box display='flex' justifyContent='center' mt={2}>
                    <Pagination
                        count={catalog?.totalPages}
                        sx={{
                            '& .MuiPaginationItem-root': {
                                color: 'primary.main',
                            },
                            '& .MuiPaginationItem-root.Mui-selected': {
                                backgroundColor: 'primary.main',
                                color: 'black',
                            }
                        }}
                        page={currentPage}
                        onChange={handlePageChange}
                    />
                </Box>
            )}
            <FilterModal
                open={filterModal.isOpen}
                handleClose={filterModal.close}
                categories={categories}
            />
        </Box >
    );
};
