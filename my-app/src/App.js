import './App.css';
import HomePage from "./components/home";
import {Route, Routes} from "react-router-dom";
import CategoryCreatePage from "./components/category/create";

const apiUrl = process.env.REACT_APP_API_URL;

function App() {
    console.log('API URL:', apiUrl);

    return (
        <>
            <Routes>
                <Route path="/">
                    <Route index element={<HomePage/>} />
                    <Route path={"create"} element={<CategoryCreatePage/>} />
                </Route>
            </Routes>
        </>
    );
}

export default App;
