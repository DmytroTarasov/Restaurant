import { ConsoleLogger } from "@microsoft/signalr/dist/esm/Utils";
import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import agent from "../api/agent";
import { Dish } from "../models/dish";
import { Portion } from "../models/portion";
import { PortionOrder } from "../models/portionOrder";
import { User, UserFormValues } from "../models/user";
import { store } from "./store";

export default class UserStore {
    user: User | null = null;
    // shoppingCartItems: Map<string, PortionOrder[]> = new Map<string, PortionOrder[]>();
    shoppingCartItems: PortionOrder[] = [];

    constructor() {
        makeAutoObservable(this)
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: UserFormValues) => {
        try {
            const user = await agent.Account.login(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            history.push('/dishes');
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem('jwt');
        this.user = null;
        history.push('/');
    }

    getUser = async () => {
        try {
            const user = await agent.Account.current();
            runInAction(() => this.user = user);
        } catch (error) {
            console.log(error);
        }
    }

    register = async (creds: UserFormValues) => {
        try {
            const user = await agent.Account.register(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            history.push('/dishes');
            store.modalStore.closeModal();
        } catch (error) {
            console.log(error);
            throw error;
        }
    }

    // addShoppingCartItem = (portion: PortionOrder, dish: Dish) => {
    //     // portion.dish = dish;
    //     // this.shoppingCartItems.push(portion);
    //     if (this.shoppingCartItems.has(dish.name)) {
    //         this.shoppingCartItems.get(dish.name)?.push(portion);
    //     } else {
    //         this.shoppingCartItems.set(dish.name, [portion]);
    //     }
    // }

    // removeShoppingCartItem = (portion: PortionOrder, dishName: string) => {
    //     this.shoppingCartItems.set(dishName, this.shoppingCartItems.get(dishName)!!.filter(i => i.id !== portion.id));
    // }

    // getShoppingCartItems = (): PortionOrder[] => {
    //     var items: PortionOrder[] = [];
    //     this.shoppingCartItems.forEach((value, key) => {
    //         items.push(...value);
    //     });
    //     // console.log(items);
    //     return items;
    // }
    addShoppingCartItem = (portion: PortionOrder) => {
        this.shoppingCartItems.push(portion);
    }

    removeShoppingCartItem = (index: number) => {
        this.shoppingCartItems.splice(index, 1);
    }
}