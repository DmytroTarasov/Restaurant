import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { Category } from '../models/category';

export default class CategoryStore {
    loadingCategories = false;
    categories: Category[] = [];
    categoryOptions: any[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    loadCategories = async () => {
        this.loadingCategories = true;
        try {
            this.categories = await agent.Categories.list();
            runInAction(() => {
                this.loadingCategories = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => this.loadingCategories = false);
        }
    }

    formCategoryOptions = () => {
        this.categoryOptions.splice(0, this.categoryOptions.length);
        this.categories.forEach(c => {
            this.categoryOptions.push({
                text: c.name,
                value: c.name
            })
        })
    }
}