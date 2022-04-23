import React from 'react';
import { useStore } from "../../../app/stores/store";
import DishItem from './DishItem';

export default function DishList() {
    const {dishStore: {dishes}} = useStore();

    return (
        <>
            {
                dishes.map(dish => (
                    <DishItem key={dish.id} dish={dish} />
                ))
            }
        </>
    );
}