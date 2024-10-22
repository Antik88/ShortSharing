import { Container, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getThing } from "../http/catalogAPI";
import { CatalogItem, RentRespone } from "../types/types";
import Grid from '@mui/material/Grid2';
import ImageGallery from "../components/ImageGallery";
import RentModal from "../modals/RentModal";
import '../styles/dateRange.css';
import 'react-date-range/dist/styles.css';
import 'react-date-range/dist/theme/default.css';
import Loading from "../shared/Loading";
import useModal from "../hooks/useModal";
import NotFound from "../shared/NotFound";
import ThingInfo from "../components/ThingInfo";
import RentalPeriod from "../components/RentalPeriod";
import { getRentsByThingId } from "../http/rentAPI";
import useUserStore from "../store/useUserStore";

export default function ThingPage() {
    const { id } = useParams();
    const [thing, setThing] = useState<CatalogItem | null>(null);
    const [rents, setRents] = useState<RentRespone[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const userStore = useUserStore();
    const rentModal = useModal();

    const [selectedDates, setSelectedDates] = useState<any>({
        startDate: new Date(),
        endDate: new Date(),
        key: 'selection',
    });

    useEffect(() => {
        const fetchThingData = async () => {
            if (id) {
                const data = await getThing(id);
                setThing(data);

                const rents = await getRentsByThingId(id);
                setRents(rents);
                setLoading(false);
            }
        };

        fetchThingData();
    }, [id]);

    if (loading) {
        return (
            <Loading />
        );
    }

    if (!thing) {
        return (
            <NotFound />
        );
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
                        disabledDatesValues={rents}
                        selectedDates={selectedDates}
                        setSelectedDates={setSelectedDates}
                        ownerId={thing.ownerId.toString()}
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
                    tenantId: userStore.user.id,
                    price: thing.price,
                    startRentDate: selectedDates.startDate,
                    endRentDate: selectedDates.endDate
                }}
            />
        </Container>
    );
}
