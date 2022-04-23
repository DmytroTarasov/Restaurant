import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { Dish } from '../models/dish';

export default class DishStore {
    dishes: Dish[] = [];
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadDishes = async () => {
        this.loadingInitial = true;
        try {
            const result = await agent.Dishes.list();
            runInAction(() => {
                this.dishes = result;
                this.loadingInitial = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => this.loadingInitial = false);
        }
    }
}