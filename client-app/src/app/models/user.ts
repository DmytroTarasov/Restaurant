export interface User {
    email: string;
    userName: string;
    displayName: string;
    isAdmin: boolean;
    token: string;
}

export interface UserFormValues {
    email: string;
    password: string;
    displayName?: string;
    username?: string;
}