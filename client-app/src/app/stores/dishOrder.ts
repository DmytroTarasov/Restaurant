export interface DishOrder {
    id: string;
    name: string;
}

export class DishOrder implements DishOrder {
    id: string;
    name: string;

    constructor(id: string, name: string) {
        this.id = id;
        this.name = name;
    } 
}