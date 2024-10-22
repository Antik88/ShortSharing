import { useEffect, useState } from "react";
import { Badge, Box, Container, Pagination, Typography } from "@mui/material";
import Grid from '@mui/material/Grid2';
import { getCatalog, getCategories } from "../http/catalogAPI";
import { CatalogResponse, Category } from "../types/types";
import { useNavigate } from "react-router-dom";
import ItemCard from "../components/ItemCard";
import SkeletonItemCard from "../shared/SkeletonItemCard";
import useModal from "../hooks/useModal";
import useFilters from "../store/useFilters";
import ClearIcon from '@mui/icons-material/Clear';
import TuneIcon from '@mui/icons-material/Tune';
import AddIcon from '@mui/icons-material/Add';
import FilterModal from "../modals/FilterModal";
import NotFound from "../shared/NotFound";
import { POSTTHING_ROUTE } from "../utils/consts";
import { StyledButton } from "../styled/StyledButton";
import { StyledButtonText } from "../styled/StyledButtonText";

export default function CatalogPage() {
    const [catalog, setCatalog] = useState<CatalogResponse | null>(null);
    const [categories, setCategories] = useState<Category[]>([]);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const filterModal = useModal();
    const navigate = useNavigate();

    const { applied, clearFilters, selectedCategory, selectedType } = useFilters();

    useEffect(() => {
        getCatalog({
            categoryId: selectedCategory,
            typeId: selectedType,
            pageNumber: currentPage
        }).then(data => setCatalog(data));

        getCategories().then(data => setCategories(data));
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
                            <StyledButton sx={{ mr: 2 }} onClick={clearFilters}>
                                <StyledButtonText>
                                    Clear
                                </StyledButtonText>
                                <ClearIcon />
                            </StyledButton>
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
                            <StyledButton onClick={filterModal.open} >
                                <StyledButtonText>
                                    Filter & Sort
                                </StyledButtonText>
                                <TuneIcon sx={{ ml: 1 }} />
                            </StyledButton>
                        </Badge>
                        <StyledButton sx={{ ml: 2 }} onClick={() => navigate(POSTTHING_ROUTE)} >
                            <AddIcon />
                        </StyledButton>
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
