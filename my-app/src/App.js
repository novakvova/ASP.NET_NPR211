import './App.css';
import HomePage from "./components/home";
import {Route, Routes} from "react-router-dom";
import CategoryCreatePage from "./components/category/create";

function App() {
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
