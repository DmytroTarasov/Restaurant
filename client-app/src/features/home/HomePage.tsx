import React from "react";
import { Container, Header, Segment, Button, Icon } from "semantic-ui-react";
import { Link } from 'react-router-dom';
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoginForm from "../users/LoginForm";
import RegisterForm from "../users/RegisterForm";

export default observer(function HomePage() {
    const {userStore, modalStore} = useStore();
    
    return (
        <Segment inverted textAlign='center' vertical className='masthead'>
            <Container text>
                <Header as='h1' inverted> 
                    {/* <Image 
                        size='massive' 
                        src='/assets/logo.png'
                        alt='logo' 
                        style={{marginBottom: '12px'}} /> */}
                    <Icon name='food' />
                        Паляниця
                </Header>
                {/* <>
                        <Header as='h2' inverted content='Welcome to the Restaurant' />
                        <Button as={Link} to='/dishes' size='huge' inverted>
                            Go to the Restaurant!
                        </Button>
                </> */}
                {userStore.isLoggedIn ? (
                    <>
                        <Header as='h2' inverted content='Welcome!' />
                        <Button as={Link} to='/dishes' size='huge' inverted>
                            Go to the restaurant!
                        </Button>
                    </>
                ) : (
                    <>
                        <Button onClick={() => modalStore.openModal(<LoginForm />)} size='huge' inverted>
                            Log in!
                        </Button>
                        <Button onClick={() => modalStore.openModal(<RegisterForm />)} size='huge' inverted>
                            Sign up!
                        </Button>
                    </>
                )}
            </Container>
        </Segment>
    )
})