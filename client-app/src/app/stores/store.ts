import { createContext, useContext } from "react";
import CategoryStore from "./categoryStore";
import DishStore from "./dishStore";
import IngredientStore from "./ingredientStore";

interface Store {
    dishStore: DishStore,
    categoryStore: CategoryStore,
    ingredientStore: IngredientStore
}

export const store: Store = {
    dishStore: new DishStore(),
    categoryStore: new CategoryStore(),
    ingredientStore: new IngredientStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}