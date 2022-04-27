import React from 'react';
import { Route, Switch } from 'react-router-dom';
import { Container } from 'semantic-ui-react';
import DishDashboard from '../../features/dishes/dashboard/DishDashboard';
import DishForm from '../../features/dishes/form/DishForm';
import HomePage from '../../features/home/HomePage';
import NavBar from './NavBar';

function App() {
	return (
		<>
			{/* <ToastContainer position='bottom-right' hideProgressBar /> */}
			<Route exact path="/" component={HomePage} />
				<Route 
					path={'/(.+)'}
					render={() => (
					<>
						<NavBar />
						<Container style={{ marginTop: "7em" }}>
							<Switch>
								<Route path='/dishes' component={DishDashboard} />
								<Route path='/createDish' component={DishForm} />
							</Switch>
						</Container>
					</>
					)}
				/>
		</>
	);
}

export default App;
