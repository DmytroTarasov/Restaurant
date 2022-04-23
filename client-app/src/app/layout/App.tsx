import React from 'react';
import { Container } from 'semantic-ui-react';
import DishDashboard from '../../features/dishes/dashboard/DishDashboard';

function App() {
	return (
		<>
			<Container style={{ marginTop: "7em" }}>
				<DishDashboard />
			</Container>
		</>
	);
}

export default App;
