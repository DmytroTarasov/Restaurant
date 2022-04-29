import React, { Fragment, useEffect, useState } from "react";
import { Button, Divider, Grid, Header, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from 'mobx-react-lite';
import { useParams, useHistory } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { v4 as uuid } from 'uuid';
import { Link } from "react-router-dom";
import { Formik, Form } from "formik";
import * as Yup from 'yup';
import MyTextInput from "../../../app/common/form/MyTextInput";
import MyTextArea from "../../../app/common/form/MyTextArea";
import MySelectInput from "../../../app/common/form/MySelectInput";
import { Dish, DishFormValues } from "../../../app/models/dish";
import { Portion } from "../../../app/models/portion";
import PhotoWidgetDropzone from "../../../app/common/imageUpload/PhotoWidgetDropzone";
import PhotoWidgetCropper from "../../../app/common/imageUpload/PhotoWidgetCropper";
import { Ingredient } from "../../../app/models/ingredient";

export default observer(function DishForm() {
    const history = useHistory();
    const {dishStore, categoryStore, ingredientStore} = useStore();
    const {createDish, uploadPhoto} = dishStore;
    const {formCategoryOptions, categoryOptions, 
        categories, loadCategories, loadingCategories} = categoryStore;
    const {loadIngredients, ingredients, ingredientsOptions, formIngredientOptions} = ingredientStore

    const [dish, setDish] = useState<DishFormValues>(new DishFormValues());
    const [portions, setPortions] = useState<Portion[]>([]);
    const [loading, setLoading] = useState(false);
    const [ingredientsLocal, setIngredientsLocal] = useState<Ingredient[]>([]);

    const [files, setFiles] = useState<any>([]);
    const [cropper, setCropper] = useState<Cropper>();

    function onCrop(dish: Dish) {
        if (cropper) {
            cropper.getCroppedCanvas().toBlob(blob => uploadPhoto(dish, blob!));
        }
    }

    const validationSchema = Yup.object({
        name: Yup.string().required('The dish is required'),
        description: Yup.string().required('The dish description is required'),
    })

    useEffect(() => {
        loadCategories().then(() => formCategoryOptions());
        loadIngredients().then(() => formIngredientOptions());
        // return () => {
        //     // actually, invoke dispose() method for each file to clean up resources
        //     files.forEach((file: any) => URL.revokeObjectURL(file.preview))
        // }
    }, [formCategoryOptions, loadCategories, formIngredientOptions, loadIngredients]);

    function handleFormSubmit(dish: DishFormValues) {
        if (!dish.id) { 
            let category = categories.find(c => c.name === dish.category)!!;
            let newIngredients: Ingredient[] = [];
            dish.ingredients.filter(i => !!i.name).forEach(ingredient => {
                newIngredients.push(ingredients.find(i => i.name === ingredient.name)!!);
            })
            let newDish = {
                ...dish,
                category,
                portions: dish.portions.filter(p => !!p.price && !!p.size), // portions that are not empty
                ingredients: newIngredients, // ingredients that are not empty
                id: uuid()
            };

            // console.log(newDish);

            setLoading(true);
            createDish(newDish).then(() => onCrop(newDish)).then(() => setTimeout(() => {
                history.push('/dishes');
                setLoading(false);
            }, 5000));
        } 
    }

    function handlePortionCreate() {
        setPortions([...portions, {size: '', price: ''}]);
    }

    function handleIngredientCreate() {
        setIngredientsLocal([...ingredientsLocal, {name: ''}]);
    }

    if(loadingCategories) return <LoadingComponent content="Loading..."/>

    return (
        <Segment clearing>
            <Header content='Dish Info' sub style={{fontSize: '18px', marginBottom: '10px', color: '#cb410b'}} />
            {/* widget start */}
            <Grid>
                <Grid.Column width={4}>
                    <Header sub content='Add photo' style={{textAlign: 'center', color: '#cb410b'}} />
                    <PhotoWidgetDropzone setFiles={setFiles} />
                </Grid.Column>
                <Grid.Column width={1} />
                <Grid.Column width={4}>
                    <Header sub content='Resize photo' style={{textAlign: 'center', color: '#cb410b'}} />
                    {files &&
                        files.length > 0 && (
                            <PhotoWidgetCropper setCropper={setCropper} imagePreview={files[0].preview}/>
                        )}
                </Grid.Column>
                <Grid.Column width={1} />
            </Grid>
            {/* widget end */}
            <Formik 
                validationSchema={validationSchema}
                enableReinitialize 
                initialValues={dish} 
                onSubmit={values => handleFormSubmit(values)}>
                {({handleSubmit, isValid, dirty}) => (
                    <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                        <MyTextInput name='name' placeholder='Name' />
                        <MyTextArea name='description' placeholder='Description' rows={3} />
                        <MySelectInput options={categoryOptions} name='category' placeholder='Category' />
                        <Header content='Ingredients' sub style={{fontSize: '16px', marginBottom: '10px', color: '#cb410b'}} />  
                        {ingredientsLocal && ingredientsLocal.map((ingredient, index) => (
                            <Fragment key={index}>
                                <MySelectInput 
                                    options={ingredientsOptions} 
                                    name={`ingredients[${index}].name`} 
                                    placeholder='Ingredient' />
                            </Fragment>
                        ))}
                        <Button  
                            type='button' 
                            content='Add ingredient'
                            onClick={() => handleIngredientCreate()}
                            style={{marginBottom: '20px', backgroundColor: '#cb410b', color: '#fff'}} />       
                        <Divider />
                        <Header content='Portions' sub style={{fontSize: '16px', marginBottom: '10px', color: '#cb410b'}} />          
                        {portions && portions.map((portion, index) => (
                            <Fragment key={index}>
                                <Header content={index + 1} style={{fontSize: '14px', color: '#cb410b'}} />
                                <MyTextInput name={`portions[${index}].size`} placeholder='Size' />
                                <MyTextInput name={`portions[${index}].price`} placeholder='Price' />
                            </Fragment>
                        ))} 
                        <Button  
                            type='button' 
                            content='Add portion'
                            onClick={() => handlePortionCreate()}
                            style={{marginBottom: '20px', backgroundColor: '#cb410b', color: '#fff'}} /> 
                        <Button 
                            disabled={loading || !dirty || !isValid}
                            loading={loading} 
                            floated='right' 
                            positive 
                            type='submit' 
                            content='Submit' />
                        <Button 
                            as={Link} 
                            to='/dishes' 
                            floated='right' 
                            type='submit' 
                            content='Cancel' />
                    </Form>
                )}
            </Formik>
        </Segment>
    )
})

