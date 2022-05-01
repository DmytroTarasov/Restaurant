import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { Route, Switch } from 'react-router-dom';
import { Container } from 'semantic-ui-react';
import DishDashboard from '../../features/dishes/dashboard/DishDashboard';
import DishForm from '../../features/dishes/form/DishForm';
import NotFound from '../../features/errors/NotFound';
import HomePage from '../../features/home/HomePage';
import OrderList from '../../features/orders/OrderList';
import LoginForm from '../../features/users/LoginForm';
import ModalContainer from '../common/modals/ModalContainer';
import { useStore } from '../stores/store';
import LoadingComponent from './LoadingComponent';
import NavBar from './NavBar';
import PrivateRoute from './PrivateRoute';
import PrivateRouteAdmin from './PrivateRouteAdmin';

function App() {
	const {commonStore, userStore} = useStore();

	useEffect(() => {
		if (commonStore.token) {
			userStore.getUser().finally(() => commonStore.setAppLoaded());
		} else {
			commonStore.setAppLoaded();
		}
	}, [commonStore, userStore])

	if (!commonStore.appLoaded) return <LoadingComponent content='Loading app...' />

	return (
		<>
			{/* <ToastContainer position='bottom-right' hideProgressBar /> */}
			<ModalContainer />
			<Route exact path="/" component={HomePage} />
			<Route 
				path={'/(.+)'}
				render={() => (
				<>
					<NavBar />
					<Container style={{ marginTop: "7em" }}>
						<Switch>
							<PrivateRoute path='/dishes' component={DishDashboard} />
							<PrivateRouteAdmin path='/createDish' component={DishForm} />
							{/* <Route path='/login' component={LoginForm} /> */}
							<PrivateRouteAdmin path='/orders' component={OrderList} />
							<Route component={NotFound} />
						</Switch>
					</Container>
				</>
				)}
			/>
	</>
	);
}

export default observer(App);
