import { createContext, useContext } from "react";
import DishStore from "./dishStore";

interface Store {
    dishStore: DishStore,
}

export const store: Store = {
    dishStore: new DishStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}