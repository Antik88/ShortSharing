import { Container, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getThing } from "../http/catalogAPI";
import { CatalogItem } from "../types/types";
import Grid from '@mui/material/Grid2';
import ImageGallery from "../components/ImageGallery";
import RentModal from "../modals/RentModal";
import { Guid } from "guid-typescript";
import '../styles/dateRange.css';
import 'react-date-range/dist/styles.css';
import 'react-date-range/dist/theme/default.css';
import Loading from "../shared/Loading";
import { addDays } from "date-fns";
import useModal from "../hooks/useModal";
import NotFound from "../shared/NotFound";
import ThingInfo from "../components/ThingInfo";
import RentalPeriod from "../components/RentalPeriod";

export default function ThingPage() {
    const { id } = useParams();
    const [thing, setThing] = useState<CatalogItem | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const rentModal = useModal();

    const [selectedDates, setSelectedDates] = useState<any>({
        startDate: addDays(new Date(), 1),
        endDate: addDays(new Date(), 1),
        key: 'selection',
    });

    useEffect(() => {
        const fetchThingData = async () => {
            if (id) {
                const data = await getThing(id);
                setThing(data)

                setLoading(false)
            }
        }

        fetchThingData();
    }, [id])

    if (loading) {
        return (
            <Loading />
        )
    }

    if (!thing) {
        return (
            <Container>
                <NotFound />
            </Container>
        )
    }

    return (
        <Container>
            <Typography
                variant='h5'
                color='primary'
                sx={{ mb: 4 }}
            >
                Catalog / {thing.category.name} / {thing.type.name}
            </Typography>
            <Grid container spacing={2}>
                <Grid size={7}>
                    <ImageGallery images={thing.images} />
                </Grid>
                <Grid size={5}>
                    <ThingInfo
                        name={thing.name} 
                        description={thing.description} 
                        price={thing.price} 
                    />
                    <RentalPeriod
                        selectedDates={selectedDates}
                        setSelectedDates={setSelectedDates}
                        openRentModal={rentModal.open}
                    />
                </Grid>
            </Grid>
            <RentModal
                open={rentModal.isOpen}
                handleClose={rentModal.close}
                rentData={{
                    thingId: thing.id,
                    thingName: thing.name,
                    tenantId: Guid.create(),
                    price: thing.price,
                    startRentDate: selectedDates.startDate,
                    endRentDate: selectedDates.endDate
                }}
            />
        </Container>
    );
}
