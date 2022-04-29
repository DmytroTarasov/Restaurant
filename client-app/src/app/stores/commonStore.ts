import { makeAutoObservable, reaction } from "mobx";

export default class CommonStore {
    token: string | null = window.localStorage.getItem('jwt');
    appLoaded = false;

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.token,
            token => {
                if (token) window.localStorage.setItem('jwt', token); // set a key-value pair into a Local Storage
                else window.localStorage.removeItem('jwt');
            }
        )
    }

    setToken = (token: string | null) => {
        this.token = token;
    }

    setAppLoaded = () => {
        this.appLoaded = true;
    }

}