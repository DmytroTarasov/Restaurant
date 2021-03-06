import { createContext, useContext } from "react";
import CategoryStore from "./categoryStore";
import CommonStore from "./commonStore";
import DishStore from "./dishStore";
import IngredientStore from "./ingredientStore";
import ModalStore from "./modalStore";
import OrderStore from "./orderStore";
import UserStore from "./userStore";

interface Store {
    dishStore: DishStore,
    categoryStore: CategoryStore,
    ingredientStore: IngredientStore,
    userStore: UserStore,
    commonStore: CommonStore,
    modalStore: ModalStore,
    orderStore: OrderStore
}

export const store: Store = {
    dishStore: new DishStore(),
    categoryStore: new CategoryStore(),
    ingredientStore: new IngredientStore(),
    userStore: new UserStore(),
    commonStore: new CommonStore(),
    modalStore: new ModalStore(),
    orderStore: new OrderStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}