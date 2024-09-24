import { $host } from ".";
import { RentRespone } from "../types/types";

export const getRentsById = async (id: string): Promise<RentRespone[]> => {
    const { data } = await $host.get<RentRespone[]>("rent", {
        params: { id }
    });
    return data;
};
