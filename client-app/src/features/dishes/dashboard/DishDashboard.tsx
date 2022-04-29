import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Button, Grid, Header } from 'semantic-ui-react';
import LoadingComponent from '../../../app/layout/LoadingComponent';
// import LoadingComponent from '../../../app/layout/LoadingComponent';
import { useStore } from '../../../app/stores/store';
import DishList from './DishList';
import DishListItemPlaceholder from './DishListItemPlaceholder';

export default observer(function ActivityDashboard() {
    const {dishStore, categoryStore} = useStore();
    const {loadingInitial, loadDishes, predicate} = dishStore;
    const {loadingCategories} = categoryStore;

    useEffect(() => {
        loadDishes();
    }, [loadDishes])
  
    if (loadingCategories) return <LoadingComponent content='Loading app...' />

    return (
        <Grid>
            <Grid.Column width='14'>
            <Header content={predicate.get("categoryName")} style={{fontSize: '24px', color: '#cb410b'}} />
                {loadingInitial ? (
                        <>
                            <DishListItemPlaceholder />
                            <DishListItemPlaceholder />
                            <DishListItemPlaceholder />
                        </>
                    ) : <DishList />}
            </Grid.Column>
            <Grid.Column width='2'>
            <Button 
                as={Link} 
                to={`/createDish`} 
                color='brown' 
                floated='right' 
                content='New dish'
                width='100%' />  
            </Grid.Column>
        </Grid>
    )
})