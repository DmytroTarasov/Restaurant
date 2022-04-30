import { PortionOrder } from "./portionOrder";
import { User } from "./user";

export interface Order {
    id?: string; 
    date?: Date;
    user?: User;
    portions: PortionOrder[];
}

export class Order implements Order {
    id?: string = undefined; 
    date?: Date;
    user?: User;
    portions: PortionOrder[];

    constructor(portions: PortionOrder[]) {
        this.portions = portions;
    }
}