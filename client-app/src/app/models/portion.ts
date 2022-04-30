import { Dish } from "./dish";

export interface Portion {
    id?: string | undefined;
    size: string;
    price: number;
    dish?: Dish;
}

export class Portion implements Portion {
    size: string = '';
    price: number = 0;

    constructor(size: string, price: number) {
        this.size = size;
        this.price = price;
    }
}