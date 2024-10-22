import { Container, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { useEffect, useState } from "react";
import { Offer } from "../types/types";
import { getRentsByTenantId } from "../http/rentAPI";
import useUserStore from "../store/useUserStore";
import Loading from "../shared/Loading";
import OffersItem from "../components/OffersItem";

export default function OffersPage() {
    const [offers, setOffers] = useState<Offer[]>([])
    const [loading, setLoading] = useState<boolean>(true)
    const user = useUserStore()

    useEffect(() => {
        getRentsByTenantId(user.user.id?.toString())
            .then(data => setOffers(data))
            .then(() => setLoading(false))
    }, [])

    if (loading) {
        return (
            <Loading />
        )
    }

    return (
        <Container>
            <Typography variant="h4" color="primary">Offers</Typography>
            <Grid container spacing={2}>
                {offers.map(item => (
                    <Grid key={item.id} spacing={2}>
                        <OffersItem offer={item} />
                    </Grid>
                ))}
            </Grid>
        </Container>
    )
}
