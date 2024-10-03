import { create } from 'zustand';
import User from './Interface/UserStore';

interface UserStoreState {
    user: User;
    setUser: (payload: User) => void;
    removeUser: () => void;
}

const initialState: User = {
    id: '',
    authId: '',
    name: '',
    email: '',
    userPictureUrl: '',
};

const useUserStore = create<UserStoreState>((set) => ({
    user: initialState,
    setUser: (payload: User) => set((state) => ({ ...state, user: payload })),
    removeUser: () => set({ user: initialState }),
}));

export default useUserStore;