import { Card, CardContent, CardMedia, Typography } from "@mui/material";
import { CatalogItem, Offer } from "../types/types";
import { useEffect, useState } from "react";
import { buildImageUrl, getThing } from "../http/catalogAPI";
import NotFound from "../shared/NotFound";
import { format } from "date-fns";

interface RentStatusMap {
    [key: number]: { name: string; color: string };
}

interface OffersItemProps {
    offer: Offer;
}

export default function OffersItem({ offer }: OffersItemProps) {
    const [thing, setThing] = useState<CatalogItem | null>(null);

    const rentStatusMap: RentStatusMap = {
        0: { name: "Pending", color: "warning.main" },
        1: { name: "Active", color: "success.main" },
        2: { name: "Expired", color: "error.main" },
        4: { name: "Canceled", color: "text.secondary" }
    };

    useEffect(() => {
        if (offer.thingId) {
            getThing(offer.thingId.toString()).then(data => setThing(data));
        }
    }, [offer.thingId]);

    if (!thing) {
        return <NotFound />;
    }

    const startRentDate = new Date(offer.startRentDate);
    const endRentDate = new Date(offer.endRentDate);

    const rentStatus = rentStatusMap[offer.status];

    return (
        <Card
            sx={{
                minWidth: 345,
                backgroundColor: "#1c1c1c",
                color: "primary.contrastText",
                borderRadius: 0
            }}
        >
            <CardMedia
                sx={{ height: 140 }}
                image={buildImageUrl(thing.images[0].name)}
                title={thing.name}
            />
            <CardContent>
                <Typography color="primary">{thing.name}</Typography>
                <Typography color="primary.light">
                    {`${format(startRentDate, "MM.dd.yyyy")} - ${format(endRentDate, "MM.dd.yyyy")}`}
                </Typography>
                <Typography sx={{ mt: 1 }} color={rentStatus.color}>
                    Status: {rentStatus.name}
                </Typography>
            </CardContent>
        </Card>
    );
}
