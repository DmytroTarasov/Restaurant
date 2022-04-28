import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { Ingredient } from '../models/ingredient';

export default class IngredientStore {
    loadingIngredients = false;
    ingredients: Ingredient[] = [];
    ingredientsOptions: any[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    loadIngredients = async () => {
        this.loadingIngredients = true;
        try {
            this.ingredients = await agent.Ingredients.list();
            runInAction(() => {
                this.loadingIngredients = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => this.loadingIngredients = false);
        }
    }

    formIngredientOptions = () => {
        this.ingredientsOptions.splice(0, this.ingredientsOptions.length);
        this.ingredients.forEach(i => {
            this.ingredientsOptions.push({
                text: i.name,
                value: i.name
            })
        })
    }
}