import React from "react";
import { Container, Header, Segment, Image, Button } from "semantic-ui-react";
import { Link } from 'react-router-dom';
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";

export default observer(function HomePage() {
    // const {userStore, modalStore} = useStore();
    
    return (
        <Segment inverted textAlign='center' vertical className='masthead'>
            <Container text>
                <Header as='h1' inverted> 
                    <Image 
                        size='massive' 
                        src='/assets/logo.png'
                        alt='logo' 
                        style={{marginBottom: '12px'}} />
                        Reactivities
                </Header>
                <>
                        <Header as='h2' inverted content='Welcome to the Restaurant' />
                        <Button as={Link} to='/dishes' size='huge' inverted>
                            Go to the Restaurant!
                        </Button>
                </>
                {/* {userStore.isLoggedIn ? (
                    <>
                        <Header as='h2' inverted content='Welcome to Reactivities' />
                        <Button as={Link} to='/activities' size='huge' inverted>
                            Go to Activities!
                        </Button>
                    </>
                ) : (
                    <>
                        <Button onClick={() => modalStore.openModal(<LoginForm />)} size='huge' inverted>
                            Login!
                        </Button>
                        <Button onClick={() => modalStore.openModal(<RegisterForm />)} size='huge' inverted>
                            Register!
                        </Button>
                    </>
                )} */}
            </Container>
        </Segment>
    )
})