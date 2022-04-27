import React, { Fragment } from 'react';
import { Segment, Placeholder, Grid } from 'semantic-ui-react';

export default function DishListItemPlaceholder() {
    return (
        <>
            <Segment style={{border: '2px solid orange'}}>
                <Grid columns={2} stackable textAlign='justified'>
                    <Grid.Row verticalAlign='middle'>
                        <Grid.Column width='7'>
                            <Placeholder>
                            <Placeholder.Image rectangular>
                                <Placeholder.Line />
                                <Placeholder.Line />
                            </Placeholder.Image>
                             </Placeholder>
                        </Grid.Column>

                        <Grid.Column width='9'>
                            <Placeholder>
                                <Placeholder.Paragraph>
                                    <Placeholder.Line />
                                    <Placeholder.Line />
                                    <Placeholder.Line />
                                    <Placeholder.Line />
                                </Placeholder.Paragraph>
                            </Placeholder>
                        </Grid.Column>
                    </Grid.Row>
                </Grid>
            </Segment>
        </>
    );
};
