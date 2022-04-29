import { Dish } from "./dish";

export interface Portion {
    id?: string | undefined;
    size: string;
    price: number | string;
    dish?: Dish;
}

export class Portion implements Portion {
    size: string = '';
    price: number | string = '';

    constructor(size: string, price: number | string) {
        this.size = size;
        this.price = price;
    }
}