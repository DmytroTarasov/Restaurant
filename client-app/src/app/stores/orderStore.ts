import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { makeAutoObservable, runInAction } from "mobx";
import { Order } from "../models/order";
import { OrderGet } from "../models/orderGet";
import { store } from "./store";

export default class OrderStore {
    orders: OrderGet[] = [];
    hubConnection: HubConnection | null = null;
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    createHubConnection = () => {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl("http://localhost:5500/orders", {
                accessTokenFactory: () => store.userStore.user?.token!
            })
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();

        this.hubConnection.start()
            .catch(error => console.log('Error with establishing the connection: ', error));
        
        this.hubConnection.on("LoadOrders", (orders: OrderGet[]) => {
            runInAction(() => {
                orders.forEach(order => {
                    order.date = new Date(order.date!!);
                })
                this.orders = orders;
            });
        })

        this.hubConnection.on("ReceiveOrder", (order: OrderGet) => {
            runInAction(() => {
                order.date = new Date(order.date!!);
                this.orders.push(order);
            });
        })
    }

    stopHubConnection = () => {
        this.hubConnection?.stop().catch(error => 
            console.log('Error with stopping the connection: ', error));
        this.hubConnection = null;
    }

    addOrder = async (order: Order) => {
        this.loading = true;
        order.user = store.userStore.user!!;
        try {
            await this.hubConnection?.invoke('SendOrder', order);
            store.userStore.clearShoppingCartItems(); 
            runInAction(() => this.loading = false);
        } catch (error) {
            console.log(error);
            runInAction(() => this.loading = false);
        }
    }
}