import React, { useEffect } from "react";
import { Container, Menu, Image, Dropdown, Icon, Popup } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { Link } from "react-router-dom";
import ShoppingCart from "../../features/cart/ShoppingCart";

export default observer(function NavBar() {
    const {categoryStore: {categories, loadCategories}, 
        dishStore: {predicate, setPredicate}, userStore: {user, logout}} = useStore();

    useEffect(() => {
        if (categories.length < 1) loadCategories();
    }, [loadCategories, categories]);

    return (
        <Menu inverted secondary fixed='top'>
            <Container>
                <Menu.Item header>
                    <img src="/assets/logo.png" alt="logo" style={{ marginRight: '10px' }} />
                    Restaurant
                </Menu.Item>
                <Menu.Item 
                    content="All dishes"
                    active={predicate.has("All")} 
                    onClick={() => setPredicate("All")}/>
                {
                    categories.map(category => (
                        <Menu.Item 
                            content={category.name} 
                            key={category.id} 
                            active={predicate.has(`${category.name}`)} 
                            onClick={() => setPredicate(`${category.name}`)}/>
                    ))
                }
                <Menu.Item position='right'>
                    <Popup hoverable
                        position='bottom right'
                        trigger={
                            <Icon 
                                name='shop' 
                                size='large'
                                style={{marginRight: '20px', color: '#fff'}} /> } >
                        <Popup.Content>
                            <ShoppingCart />
                        </Popup.Content>
                    </Popup>
                    <Image src='/assets/user.png' avatar spaced='right' />
                    <Dropdown pointing='top left' text={user?.displayName}>
                        <Dropdown.Menu>
                            <Dropdown.Item onClick={logout} text='Log out' icon='power' />
                        </Dropdown.Menu>
                    </Dropdown>
                </Menu.Item>
            </Container>
        </Menu>
    )
})