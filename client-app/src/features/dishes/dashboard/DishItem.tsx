import React from "react";
import { Grid, Item, Segment, Image } from "semantic-ui-react";
import { Dish } from "../../../app/models/dish";

interface Props {
    dish: Dish
}

export default function DishItem({dish}: Props) {
    return (
        <Segment style={{border: '2px solid orange'}}>
            <Grid columns={2} stackable textAlign='justified'>
                <Grid.Row verticalAlign='middle'>
                    <Grid.Column width='7'>
                        <Image size='large' src='/assets/placeholder.png' />
                    </Grid.Column>

                    <Grid.Column width='9'>
                    <Item>
                        <Item.Content>
                            <Item.Header as='h2' style={{color: 'orange'}}>{dish.name}</Item.Header>
                            <Item.Description>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus facilisis auctor ligula quis euismod. Aliquam erat volutpat. Donec id nibh finibus, vulputate est sit amet, ultrices elit. Phasellus vitae sapien volutpat, elementum justo rutrum, pharetra arcu. Suspendisse potenti. Vivamus mollis urna nec aliquam cursus. Aenean facilisis nunc non purus accumsan.</Item.Description>
                        </Item.Content>
                    </Item>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    )
}