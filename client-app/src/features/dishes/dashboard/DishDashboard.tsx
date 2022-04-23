import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { Grid } from 'semantic-ui-react';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import { useStore } from '../../../app/stores/store';
import DishList from './DishList';

export default observer(function ActivityDashboard() {
    const {dishStore} = useStore();
    const {loadingInitial, loadDishes} = dishStore;

    useEffect(() => {
       loadDishes();
    }, [loadDishes])
  
    if (loadingInitial) return <LoadingComponent content='Loading app...' />

    return (
        // <Grid>
        //     <Grid.Column width='10'>
            <DishList />
        //     </Grid.Column>
        // </Grid>
    )
})