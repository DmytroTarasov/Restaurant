import axios, { AxiosResponse } from "axios";
import { Category } from "../models/category";
import { Dish, DishFormValues } from "../models/dish";
import { Ingredient } from "../models/ingredient";
import { Photo } from "../models/photo";
import { User, UserFormValues } from "../models/user";
import { store } from "../stores/store";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = 'http://localhost:5500/api';

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token) config.headers.Authorization = `Bearer ${token}`
    return config;
})

axios.interceptors.response.use(async response => {
    try {
        await sleep(1000);
        return response;
    } catch (error) {
        console.log(error);
        return await Promise.reject(error);
    }
})

const responseBody = <T> (response: AxiosResponse<T>) => {
    return response.data;
}

const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
}

const Dishes = {
    list: (params: URLSearchParams) => axios.get<Dish[]>(`/dishes`, {params}).then(responseBody),
    create: (dish: DishFormValues) => requests.post<void>('/dishes', dish),
    uploadPhoto: (dishId: string, file: Blob) => {
        console.log(file);
        let formData = new FormData();
        formData.append('File', file);
        return axios.post<Photo>(`dishes/${dishId}/addPhoto`, formData, {
            headers: {'Content-type': 'multipart/form-data'}
        })
    }
}

const Categories = {
    list: () => requests.get<Category[]>('/categories'),
}

const Ingredients = {
    list: () => requests.get<Ingredient[]>('/ingredients'),
}

const Account = {
    current: () => requests.get<User>('/account'),
    login: (user: UserFormValues) => requests.post<User>('/account/login', user),
    register: (user: UserFormValues) => requests.post<User>('/account/register', user)
}

const agent = {
    Dishes,
    Categories,
    Ingredients,
    Account
}

export default agent;