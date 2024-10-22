import { $host } from ".";

export interface User {
    nickname: string;
    sub: string;
    email: string;
    picture: string;
}

export interface UserService {
    id: string | undefined;
    authId: string | undefined;
    name: string | undefined;
    email: string | undefined;
    userPictureUrl: string | undefined;
}

export const postUser = async (userData: UserService): Promise<User | null> => {
    try {
        const { data } = await $host.post<User>(`users`, userData);
        return data;
    } catch (error) {
        return null;
    }
};

export const getUser = async (authId: string | undefined): Promise<UserService> => {
    const { data } = await $host.get<UserService>(`user/auth0/${authId}`);
    return data;
};