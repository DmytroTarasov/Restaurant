import React from "react";
import { Grid, Item, Segment, Image, Icon, Divider, List, Button } from "semantic-ui-react";
import { Dish } from "../../../app/models/dish";

interface Props {
    dish: Dish
}

export default function DishItem({dish}: Props) {
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
                            <Item.Description>
                                <Icon name='food' size='big' color='orange' style={{marginRight: '20px'}} />
                                <List horizontal>
                                    { dish.portions.map(portion => (
                                        <List.Item 
                                            style={{color: 'orange', fontWeight: '600', fontSize: '18px'}} 
                                            key={portion.id}>
                                            <List.Content>
                                                <span style={{marginRight: '10px'}}>{portion.size} &ndash; {portion.price}$ </span>
                                                <Icon name='shopping cart' size='large' color='orange' style={{marginRight: '10px'}}/>
                                            </List.Content>
                                        </List.Item>
                                    )) }     
                                </List>
                                {/* <Button 
                                    color='brown' 
                                    floated='right' 
                                    content='Edit' />         */}
                            </Item.Description>
                        </Item.Content>
                    </Item>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    )
}