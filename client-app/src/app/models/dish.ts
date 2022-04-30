import { Category } from "./category";
import { Ingredient } from "./ingredient";
import { Photo } from "./photo";
import { Portion } from "./portion";

export interface Dish {
    id: string;
    name: string;
    description: string;
    category: Category;
    portions: Portion[];
    photo?: Photo;
    ingredients: Ingredient[];
}

export class Dish implements Dish {
    constructor(init?: DishFormValues) {
        Object.assign(this, init); 
    }
}

export class DishFormValues {
    id?: string = undefined;
    name: string = '';
    description: string = '';
    category: Category | string = ''; 
    portions: Portion[] = Array(3).fill(new Portion('', 0));
    ingredients: Ingredient[] = Array(10).fill(new Ingredient(''));

    constructor(dish?: DishFormValues) {
        if (dish) {
            this.id = dish.id;
            this.name = dish.name;
            this.description = dish.description;
            this.category = dish.category;
            this.portions = dish.portions;
         }
    }
}