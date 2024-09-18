import { RouteConfig } from './types/types.ts';
import AboutPage from './pages/AboutPage.tsx';
import CatalogPage from './pages/CatalogPage.tsx';
import MainPage from './pages/MainPage.tsx';

import {
    MAINPAGE_ROUTE,
    CATALOG_ROUTE,
    ABOUT_ROUTE,
} from './utils/consts.ts';


export const authRoutes: RouteConfig[] = [
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
]