import { Guid } from "guid-typescript";
import { $host } from ".";
import { CatalogItem, CatalogResponse, Category } from "../types/types";

interface CatalogParams {
    name?: string;
    categoryId?: Guid;
    typeId?: Guid;
    currentPage?: number;
}

export const getCatalog = async (params: CatalogParams): Promise<CatalogResponse> => {
    const { data } = await $host.get<CatalogResponse>("catalog", {
        params
    });
    return data;
};

export const getCategories = async (): Promise<Category[]> => {
    const { data } = await $host.get<Category[]>("categories");
    return data;
};

export const getThing = async (id: string): Promise<CatalogItem> => {
    const { data } = await $host.get<CatalogItem>(`catalog/${id}`);
    return data;
};

export const getImageUrl = async (name: string): Promise<string> => {
    return `${$host.defaults.baseURL}image?name=${encodeURIComponent(name)}`;
};
