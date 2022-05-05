import { observer } from "mobx-react-lite";
import React from "react";
import { Grid, Item, Segment, Image, Icon, Divider, List, Button } from "semantic-ui-react";
import { Dish } from "../../../app/models/dish";
import { PortionOrder } from "../../../app/models/portionOrder";
import { DishOrder } from "../../../app/stores/dishOrder";
import { useStore } from "../../../app/stores/store";

interface Props {
    dish: Dish
}

export default observer(function DishItem({dish}: Props) {
    const {userStore: {addShoppingCartItem}} = useStore();

    return (
        <Segment style={{border: '2px solid orange'}}>
            <Grid columns={2} stackable textAlign='justified'>
                <Grid.Row verticalAlign='middle'>
                    <Grid.Column width='5'>
                        <Image size='large' src={dish.photo?.url || '/assets/placeholder.png'} />
                    </Grid.Column>

                    <Grid.Column width='11'>
                    <Item>
                        <Item.Content>
                            {/* #a40606 */}
                            <Item.Header as='h2' style={{marginTop: '10px', textAlign: 'center'}}>{dish.name}</Item.Header>
                            <Item.Description>{dish.description}</Item.Description>
                            <Divider />
                            <Item.Header as='h3' style={{marginTop: '10px'}}>Ingredients</Item.Header>
                            <List>
                                    { dish.ingredients.map(i => (
                                        <List.Item
                                            style={{fontSize: '16px'}}
                                            key={i.id} >
                                            <List.Content>
                                                {i.name}
                                            </List.Content>
                                        </List.Item>
                                    ))}
                                </List>
                            <Divider />
                            <Item.Description style={{display: 'flex', justifyContent: 'flex-end'}}>
                                <Icon name='food' size='big' style={{marginRight: '20px', color: '#cb410b'}} />
                                <List horizontal>
                                    { dish.portions.map(portion => (
                                        <List.Item 
                                            style={{color: '#cb410b', fontWeight: '600', fontSize: '18px'}} 
                                            key={portion.id}>
                                            <List.Content>
                                                <span>{portion.size} &ndash; {portion.price}$ </span>
                                                <Button animated='fade' 
                                                    style={{backgroundColor: 'transparent', padding: '0 10px'}}
                                                    onClick={() => 
                                                        addShoppingCartItem(new PortionOrder
                                                            (portion.id!!, portion.size, portion.price, new DishOrder(dish.id, dish.name)))}>
                                                    <Button.Content visible>
                                                        <Icon 
                                                            style={{display: 'block', marginBottom: '4px', color: '#cb410b'}}
                                                            name='shop' 
                                                            size='large' />
                                                    </Button.Content>
                                                    <Button.Content hidden>
                                                        <div style={{color: '#cb410b', lineHeight: '8px', fontSize: '16px'}}>Try it</div>
                                                    </Button.Content>
                                                </Button>
                                            </List.Content>
                                        </List.Item>
                                    )) }     
                                </List>
                            </Item.Description>
                        </Item.Content>
                    </Item>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    )
})