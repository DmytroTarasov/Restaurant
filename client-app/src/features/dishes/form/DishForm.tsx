import React, { Fragment, useEffect, useState } from "react";
import { Button, Grid, Header, Segment } from "semantic-ui-react";
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

export default observer(function DishForm() {
    const history = useHistory();
    const {dishStore, categoryStore} = useStore();
    const {createDish, loadingCreate, uploadPhoto, dishes} = dishStore;
    const {formCategoryOptions, categoryOptions, 
        categories, loadCategories, loadingCategories} = categoryStore;
    // const {id} = useParams<{id: string}>();

    const [dish, setDish] = useState<DishFormValues>(new DishFormValues());
    const [portions, setPortions] = useState<Portion[]>([]);
    // const [blob, setBlob] = useState<Blob>();

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
        // return () => {
        //     // actually, invoke dispose() method for each file to clean up resources
        //     files.forEach((file: any) => URL.revokeObjectURL(file.preview))
        // }
    }, [formCategoryOptions, loadCategories]);

    function handleFormSubmit(dish: DishFormValues) {
        if (!dish.id) { 
            let category = categories.find(c => c.name === dish.category)!!;
            let newDish = {
                ...dish,
                category,
                portions: dish.portions.filter(p => !!p.price && !!p.size), // portions that are not empty
                id: uuid()
            };
            createDish(newDish).then(() => onCrop(newDish)).then(() => history.push('/dishes'));
        } 
        // else {
        //     updateActivity(activity).then(() => history.push(`/activities/${activity.id}`))
        // }
    }

    function handlePortionCreate() {
        setPortions([...portions, {size: '', price: ''}]);
    }

    if(loadingCategories) return <LoadingComponent content="Loading..."/>

    return (
        <Segment clearing>
            <Header content='Dish Info' sub color='orange' style={{fontSize: '18px'}} />
            {/* widget start */}
            <Grid>
                <Grid.Column width={4}>
                    <Header sub color='teal' content='Step 1 - Add photo' />
                    <PhotoWidgetDropzone setFiles={setFiles} />
                </Grid.Column>
                <Grid.Column width={1} />
                <Grid.Column width={4}>
                    <Header sub color='teal' content='Step 2 - Resize image' />
                    {files &&
                        files.length > 0 && (
                            <PhotoWidgetCropper setCropper={setCropper} imagePreview={files[0].preview}/>
                        )}
                </Grid.Column>
                <Grid.Column width={1} />
            </Grid>
            {/* widget end */}
            {/* <PhotoUploadWidget uploadPhoto={uploadPhoto} /> */}
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
                        <Header content='Portions' sub color='orange' style={{fontSize: '16px'}} />                  
                        {portions.length !== 0 ? portions.map((portion, index) => (
                            <Fragment key={index}>
                                <MyTextInput name={`portions[${index}].size`} placeholder='Size' />
                                <MyTextInput name={`portions[${index}].price`} placeholder='Price' />
                                <Button  
                                    floated='left' 
                                    type='button' 
                                    content='Add portion'
                                    color='orange'
                                    onClick={() => handlePortionCreate()} 
                                    style={{marginBottom: '20px'}}/>  
                            </Fragment>
                        )) : (
                            <Button  
                                floated='left' 
                                type='button' 
                                content='Add portion'
                                color='orange'
                                onClick={() => handlePortionCreate()} /> 
                        )}
                        <Button 
                            disabled={loadingCreate || !dirty || !isValid}
                            loading={loadingCreate} 
                            floated='right' 
                            positive 
                            type='submit' 
                            content='Submit' 
                            color='orange' />
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

