import { create } from 'zustand';

export interface ModalState {
  isOpen: boolean;
  open: () => void;
  close: () => void;
}

const useModal = create<ModalState>((set) => ({
  isOpen: false,
  open: () => set({ isOpen: true }),
  close: () => set({ isOpen: false }),
}));

export default useModal;