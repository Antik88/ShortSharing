import { Guid } from "guid-typescript";
import { $authHost, $host } from ".";
import { CatalogItem, CatalogResponse, Category } from "../types/types";

export interface CatalogParams {
    categoryId?: Guid | null;
    typeId?: Guid | null;
    pageNumber?: number | 1;
    pageSize?: number | 8;
}

export interface PostThing {
    id: string,
    name: string,
    description: string,
    price: string | number,
    categoryId: string | undefined,
    typeId: string | undefined,
    ownerId: string | undefined,
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

export const buildImageUrl = (name: string): string | undefined => {
    if(name)
        return `${$host.defaults.baseURL}image?name=${encodeURIComponent(name)}`;
};

export const postThing = async (thing: PostThing): Promise<PostThing> => {
    const { data } = await $authHost.post<PostThing>(`catalog`, thing);
    return data;
};

export const putImage = async (thingId: string, file: File) => {
    const formData = new FormData();
    formData.append('formFile', file);

    const { data } = await $authHost.put(`image?thingId=${thingId}`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
    return data;
};
