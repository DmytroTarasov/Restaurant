export interface Portion {
    id?: string | undefined;
    size: string;
    price: number | string;
}

export class Portion implements Portion {
    size: string = '';
    price: number | string = '';

    constructor(size: string, price: number | string) {
        this.size = size;
        this.price = price;
    }
}