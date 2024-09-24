import { create } from 'zustand';
import { Guid } from 'guid-typescript';

interface FilterState {
  selectedCategory: Guid | null;
  selectedType: Guid | null;
  applied: boolean;
  selectCategoryType: (categoryId: Guid, typeId: Guid) => void;
  applyFilters: () => void;
  clearFilters: () => void;
}

const useFilters = create<FilterState>((set) => ({
  selectedCategory: null,
  selectedType: null,
  applied: false,

  selectCategoryType: (categoryId: Guid, typeId: Guid) =>
    set({
      selectedCategory: categoryId,
      selectedType: typeId,
    }),

  applyFilters: () =>
    set((state) => ({
      applied: !!state.selectedCategory && !!state.selectedType,
    })),

  clearFilters: () =>
    set({
      selectedCategory: null,
      selectedType: null,
      applied: false,
    }),
}));

export default useFilters;
