import { DishOrder } from "../stores/dishOrder";

export interface PortionOrder {
    id: string;
    size: string;
    price: number;
    dish: DishOrder;
}

export class PortionOrder implements PortionOrder {
    id: string;
    size: string = '';
    price: number = 0;
    dish: DishOrder;

    constructor(id: string, size: string, price: number, dish: DishOrder) {
        this.id = id;
        this.size = size;
        this.price = price;
        this.dish = dish;
    }

}