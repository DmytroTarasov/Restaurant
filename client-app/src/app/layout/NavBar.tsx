import React, { useEffect } from "react";
import { Container, Menu } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";

export default observer(function NavBar() {
    const {categoryStore: {categories, loadCategories}, 
        dishStore: {predicate, setPredicate}} = useStore();

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

            </Container>
        </Menu>
    )
})