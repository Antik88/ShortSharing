import { $authService } from ".";

export interface User {
    nickname: string;
    sub: string;
    email: string;
    picture: string;
}

export interface UserService {
    authId: string;
    name: string;
    email: string;
}

export const getUserData = async (): Promise<User> => {
    const { data } = await $authService.get<User>("userinfo");
    return data;
};
