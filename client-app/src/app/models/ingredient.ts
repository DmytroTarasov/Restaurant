export interface Ingredient {
    id?: string;
    name: string;
} 

export class Ingredient implements Ingredient {
    name: string = '';

    constructor(name: string) {
        this.name = name;
    }
}