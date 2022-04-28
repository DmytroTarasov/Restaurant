import React, { Fragment } from 'react';
import { Segment, Placeholder, Grid, Divider } from 'semantic-ui-react';

export default function DishListItemPlaceholder() {
    return (
        <>
            <Segment style={{border: '2px solid orange'}}>
                <Grid columns={2} stackable textAlign='justified'>
                    <Grid.Row>
                        <Grid.Column width='5'>
                            <Placeholder>
                                <Placeholder.Image rectangular>
                                    <Placeholder.Line />
                                    <Placeholder.Line />
                                </Placeholder.Image>
                             </Placeholder>
                        </Grid.Column>

                        <Grid.Column width='11'>
                        <Placeholder fluid>
                            <Placeholder.Line />
                        </Placeholder>
                        <Divider />
                        <Placeholder>
                            <Placeholder.Paragraph>
                                <Placeholder.Line length='short' />
                                <Placeholder.Line length='very short' />
                                <Placeholder.Line length='very short' />
                                <Placeholder.Line length='very short' />
                                <Placeholder.Line length='very short' />
                                <Placeholder.Line length='very short' />
                            </Placeholder.Paragraph>
                        </Placeholder>
                        <Divider />
                        <Placeholder fluid>
                            <Placeholder.Paragraph>
                                <Placeholder.Line length='full' />
                                <Placeholder.Line length='full' />
                            </Placeholder.Paragraph>
                        </Placeholder>
                        </Grid.Column>
                    </Grid.Row>
                </Grid>
            </Segment>
        </>
    );
};
