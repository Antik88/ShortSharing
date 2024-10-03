import { RouteConfig } from './types/types.ts';
import AboutPage from './pages/AboutPage.tsx';
import CatalogPage from './pages/CatalogPage.tsx';
import MainPage from './pages/MainPage.tsx';
import PostThing from './pages/PostThing.tsx';
import ThingPage from './pages/ThingPage.tsx';

import {
    MAINPAGE_ROUTE,
    CATALOG_ROUTE,
    ABOUT_ROUTE,
    POSTTHING_ROUTE,
    THING_ROUTE,
} from './utils/consts.ts';


export const authRoutes: RouteConfig[] = [
    {
        path: POSTTHING_ROUTE,
        Component: PostThing
    },
]

export const publicRoutes: RouteConfig[] = [
    {
        path: MAINPAGE_ROUTE,
        Component: MainPage
    },
    {
        path: CATALOG_ROUTE,
        Component: CatalogPage
    },
    {
        path: ABOUT_ROUTE,
        Component: AboutPage
    },
    {
        path: THING_ROUTE + "/:id",
        Component: ThingPage
    },
]