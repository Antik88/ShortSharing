import axios, { AxiosRequestHeaders, InternalAxiosRequestConfig } from 'axios';

axios.defaults.baseURL = import.meta.env.VITE_API_URL;

const $authService = axios.create({
    baseURL: import.meta.env.VITE_AUTH0_API,
});

const $host = axios.create({
    baseURL: import.meta.env.VITE_API_URL, 
});

const $authHost = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
});

const authInterceptor = (config: InternalAxiosRequestConfig) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers = {
            ...config.headers,
            Authorization: `Bearer ${token}`,
        } as AxiosRequestHeaders;
    }
    return config;
};

$authHost.interceptors.request.use(authInterceptor);
$authService.interceptors.request.use(authInterceptor);

export {
    $host,
    $authHost,
    $authService
};
