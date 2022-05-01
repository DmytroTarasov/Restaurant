import React from "react";
import { Button, Header, Icon, Segment } from "semantic-ui-react";
import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <Segment placeholder>
            <Header icon>
                <Icon name='search' />
                We could not find any info. Check the correctness of the url specified
            </Header>
            <Segment.Inline>
                <Button as={Link} to='/dishes' style={{backgroundColor: 'orange', color: 'white'}}>
                    Return to the Dishes
                </Button>
            </Segment.Inline>
        </Segment>
    )
}