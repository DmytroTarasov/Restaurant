import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { Divider, Grid, Header, Item, List, Segment } from "semantic-ui-react";

export default observer(function OrderList() {
    const {orderStore: {orders}} = useStore();
    orders.forEach(o => o.portions.forEach(p => p));

    return (
        <Grid centered>   
            <Grid.Column width={10}>
                <Header style={{color: '#cb410b'}} content='Orders' /> 
                {orders.map((order, index) => (
                    <Segment key={order.id} style={{border: '2px solid orange', color: '#cb410b'}}>
                            <Item>
                                <Item.Content>
                                    <Item.Header style={{textAlign: 'center'}}>
                                        <span 
                                            style={{border: '2px solid #cb410b', 
                                                padding: '5px 10px', 
                                                borderRadius: '100%',
                                                fontSize: '18px',
                                                fontWeight: '600'}}>{index + 1}</span>
                                    </Item.Header>
                                    <Divider />
                                    <Item.Description>
                                        <List>
                                            <List.Header
                                                style={{ 
                                                fontSize: '18px',
                                                fontWeight: '600',
                                                marginBottom: '5px'}}>Portions</List.Header>
                                            {order.portions.map((p, idx) => (
                                                <List.Item key={p.id} style={{fontSize: '16px'}}>
                                                    <Grid columns={3}>
                                                        <Grid.Column>
                                                            {idx + 1}) {p.dishName}
                                                        </Grid.Column>
                                                        <Grid.Column textAlign='center'>
                                                            {p.size}
                                                        </Grid.Column>
                                                        <Grid.Column textAlign='center'>
                                                            {p.price}$
                                                        </Grid.Column>
                                                    </Grid>
                                                </List.Item>
                                            ))}
                                        </List>
                                        <Divider />
                                        <Grid columns={3}>
                                            <Grid.Column>
                                            </Grid.Column>
                                            <Grid.Column>
                                            </Grid.Column>
                                            <Grid.Column textAlign='center'>
                                                {order.portions.map(p => p.price).reduce((prev, next) => prev + next)}$
                                            </Grid.Column>
                                        </Grid>
                                        <h4>Ordered by: {order.user?.displayName}, email: {order.user?.email}</h4>
                                    </Item.Description>
                                </Item.Content>
                            </Item>
                        </Segment>
                ))}
                </Grid.Column>                
        </Grid>  
    )
})