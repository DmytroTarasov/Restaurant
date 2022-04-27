import axios, { AxiosResponse } from "axios";
import { Category } from "../models/category";
import { Dish, DishFormValues } from "../models/dish";
import { Photo } from "../models/photo";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = 'http://localhost:5500/api';

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
    // put: <T> (url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    // del: <T> (url: string) => axios.delete<T>(url).then(responseBody),
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
    },
}

const Categories = {
    list: () => requests.get<Category[]>('/categories'),
}

const agent = {
    Dishes,
    Categories
}

export default agent;