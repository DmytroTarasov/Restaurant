import { makeAutoObservable, reaction, runInAction } from 'mobx';
import agent from '../api/agent';
import { Dish, DishFormValues } from '../models/dish';

export default class DishStore {
    dishes: Dish[] = [];
    loadingInitial = false;
    predicate = new Map().set("categoryName", "All");
    loadingCreate = false;

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.predicate.keys(),
            () => {
                this.dishes.splice(0, this.dishes.length);
                this.loadDishes();
            }
        )
    }

    // computed property
    get axiosParams() {
        const params = new URLSearchParams();
        this.predicate.forEach((value, key) => params.append(key, value));
        return params;
    }

    setPredicate = (value: string) => {
        const resetPredicate = () => {
            this.predicate.forEach((value, key) => {
                this.predicate.delete(key)
            })
        }  
        resetPredicate();
        this.predicate.set("categoryName", value);
    }

    sortDishesPortions = (dishes: Dish[]) => {
        dishes.forEach(dish => dish.portions.sort((p1, p2) => +p1.size.replace(/[^0-9]/g, '') - +p2.size.replace(/[^0-9]/g, '')))
        return dishes;
    }

    loadDishes = async () => {
        this.loadingInitial = true;
        try {
            const result = await agent.Dishes.list(this.axiosParams);
            runInAction(() => {
                this.dishes = this.sortDishesPortions(result);
                this.loadingInitial = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loadingInitial = false
            });
        }
    }

    createDish = async (dish: DishFormValues) => {
        this.loadingCreate = true;
        try {
            await agent.Dishes.create(dish);
            runInAction(() => this.loadingCreate = false);
        } catch (error) {
            console.log(error);
            runInAction(() => this.loadingCreate = false);
        }
    }

    uploadPhoto = async (dish: Dish, file: Blob) => {
        try {
            await agent.Dishes.uploadPhoto(dish.id, file);
        } catch(error) {
            console.log(error);
        }
    }
}