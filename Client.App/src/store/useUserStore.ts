import { create } from 'zustand';
import { persist } from 'zustand/middleware';
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
    token: '',
};

const useUserStore = create<UserStoreState>()(
    persist(
        (set) => ({
            user: initialState,
            setUser: (payload: User) => set((state) => ({ ...state, user: payload })),
            removeUser: () => set({ user: initialState }),
        }),
        {
            name: 'user-storage'
        }
    )
);

export default useUserStore;