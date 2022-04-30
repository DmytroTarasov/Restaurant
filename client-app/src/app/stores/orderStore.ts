import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { makeAutoObservable, runInAction } from "mobx";
import { Order } from "../models/order";
import { OrderGet } from "../models/orderGet";
import { store } from "./store";

export default class OrderStore {
    orders: OrderGet[] = [];
    hubConnection: HubConnection | null = null;

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
            // console.log(orders);
            runInAction(() => this.orders = orders);
        })

        this.hubConnection.on("ReceiveOrder", (order: OrderGet) => {
            console.log("Received order: " + order.user?.userName);
            order.portions.forEach(p => console.log(p.dishName));
            runInAction(() => this.orders.push(order));
        })
    }

    stopHubConnection = () => {
        this.hubConnection?.stop().catch(error => 
            console.log('Error with stopping the connection: ', error));
    }

    addOrder = async (order: Order) => {
        // console.log(order);
        order.user = store.userStore.user!!;
        // console.log(order);
        try {
            await this.hubConnection?.invoke('SendOrder', order); 
        } catch (error) {
            console.log(error);
        }
    }
}