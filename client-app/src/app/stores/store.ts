import { createContext, useContext } from "react";
import CategoryStore from "./categoryStore";
import DishStore from "./dishStore";

interface Store {
    dishStore: DishStore,
    categoryStore: CategoryStore
}

export const store: Store = {
    dishStore: new DishStore(),
    categoryStore: new CategoryStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}