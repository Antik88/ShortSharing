import { addDays } from "date-fns";
import { $host } from ".";
import { Offer, RentData, RentRespone } from "../types/types";

export const getRentsByThingId = async (id: string): Promise<RentRespone[]> => {
    const { data } = await $host.get<RentRespone[]>(`rent/thing/${id}`);
    return data;
};

export const rentThing = async (rentData: RentData): Promise<RentData> => {
    rentData.startRentDate = addDays(rentData.startRentDate, 1)
    rentData.endRentDate = addDays(rentData.endRentDate, 1)

    const { data } = await $host.post<RentData>("rent", rentData);
    return data;
};

export const getRentsByTenantId = async (id: string | undefined): Promise<Offer[]> => {
    const { data } = await $host.get<Offer[]>(`rent/user/${id}`);
    return data;
};
