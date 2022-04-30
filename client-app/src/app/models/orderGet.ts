import { PortionOrderGet } from "./PortionOrderGet";
import { User } from "./user";

export interface OrderGet {
    id?: string; 
    date?: Date;
    user?: User;
    portions: PortionOrderGet[];
}