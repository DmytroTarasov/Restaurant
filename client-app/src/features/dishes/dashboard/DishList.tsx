import { observer } from 'mobx-react-lite';
import React from 'react';
import { useStore } from "../../../app/stores/store";
import DishItem from './DishItem';

export default observer(function DishList() {
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
})