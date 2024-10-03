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

const saveToSessionStorage = (key: string, value: any) => {
  sessionStorage.setItem(key, JSON.stringify(value));
};

const loadFromSessionStorage = (key: string) => {
  const storedValue = sessionStorage.getItem(key);
  return storedValue ? JSON.parse(storedValue) : null;
};

const useFilters = create<FilterState>((set) => {
  const initialState = loadFromSessionStorage('filters') || {
    selectedCategory: null,
    selectedType: null,
    applied: false,
  };

  return {
    ...initialState,

    selectCategoryType: (categoryId: Guid, typeId: Guid) => {
      const newState = {
        selectedCategory: categoryId,
        selectedType: typeId,
      };
      saveToSessionStorage('filters', newState);
      set(newState);
    },

    applyFilters: () =>
      set((state) => {
        const newState = {
          applied: !!state.selectedCategory && !!state.selectedType,
        };
        saveToSessionStorage('filters', { ...state, ...newState });
        return newState;
      }),

    clearFilters: () => {
      const newState = {
        selectedCategory: null,
        selectedType: null,
        applied: false,
      };
      sessionStorage.removeItem('filters');
      set(newState);
    },
  };
});

export default useFilters;
