import { ComponentType } from 'react';
import { Guid } from 'guid-typescript';

export interface RouteConfig {
    path: string;
    Component: ComponentType;
}

export interface CatalogResponse {
    items: CatalogItem[];
    currentPage: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
}

export interface CatalogItem {
    id: Guid;
    name: string;
    description: string;
    price: number;
    updatedAt: string;
    ownerId: string;
    category: { name: string };
    type: { name: string };
    images: Image[];
}

export interface Image {
    name: string;
}

export interface CategoryType {
    id: Guid;
    name: string;
}

export interface Category {
    id: Guid;
    name: string;
    types: CategoryType[];
}

export interface RentRespone {
    id: Guid;
    startDate: Date;
    endDate: Date;
    thingId: Guid;
    tenantId: Guid;
    status: number;
}

export interface RentData {
    thingName: string;
    startRentDate: Date;
    endRentDate: Date;
    thingId: Guid;
    tenantId: Guid;
    price: number;
}