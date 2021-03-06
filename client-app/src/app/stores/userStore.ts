import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import agent from "../api/agent";
import { PortionOrder } from "../models/portionOrder";
import { User, UserFormValues } from "../models/user";
import { store } from "./store";

export default class UserStore {
    user: User | null = null;
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
        store.orderStore.stopHubConnection();
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

    addShoppingCartItem = (portion: PortionOrder) => {
        this.shoppingCartItems.push(portion);
    }

    removeShoppingCartItem = (index: number) => {
        this.shoppingCartItems.splice(index, 1);
    }

    clearShoppingCartItems = () => this.shoppingCartItems = [];
}