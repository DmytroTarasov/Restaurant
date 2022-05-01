import { observer } from "mobx-react-lite";
import React, { Fragment } from "react";
import { Button, Divider, Grid, Header, Item, Segment } from "semantic-ui-react";
import { Order } from "../../app/models/order";
import { useStore } from "../../app/stores/store";

export default observer(function ShoppingCart() {
    const {userStore: {shoppingCartItems, removeShoppingCartItem},
        orderStore: {addOrder, loading}} = useStore();

    return (
        <Segment style={{width: '500px'}}>
            <Header as='h3' content='Shopping Cart' style={{color: '#cb410b'}} textAlign='center' />
            <Divider />
            {shoppingCartItems.length !== 0 ?  
                (shoppingCartItems.map((item, index) => (
                    <Fragment key={index}>
                        <Item>
                            <Item.Content>
                                <Grid verticalAlign='middle'>
                                    <Grid.Column width={3}>
                                        <Item.Header as='h4' style={{color: '#cb410b'}}>{item.dish.name}</Item.Header>
                                    </Grid.Column>
                                    <Grid.Column width={8}>
                                        <span style={{color: '#cb410b'}}>{item.size}</span>
                                    </Grid.Column>
                                    <Grid.Column width={5}>
                                        <Button 
                                            content='Remove' 
                                            basic 
                                            color='orange' 
                                            floated='right'
                                            onClick={() => removeShoppingCartItem(index)} />
                                    </Grid.Column>
                                </Grid>
                            </Item.Content>
                        </Item>
                        <Divider />
                    </Fragment>  
                ))) : (
                    <Header as='h4' content='Shopping cart is empty' textAlign='center' />
                )}
                <Button 
                    color='green'
                    content='Confirm' 
                    disabled={shoppingCartItems.length === 0}
                    loading={loading}
                    onClick={() => addOrder(new Order(shoppingCartItems))}/>
        </Segment>
    )
})