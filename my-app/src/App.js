import './App.css';
import HomePage from "./components/home";
import {Route, Routes} from "react-router-dom";
import CategoryCreatePage from "./components/category/create";
import Layout from "./components/containers";
import NotFoundPage from "./components/pages/404";
import ProductListPage from "./components/product/list";
import ProductCreatePage from "./components/product/create";

const apiUrl = process.env.REACT_APP_API_URL;

function App() {
    console.log('API URL:', apiUrl);

    return (
        <>
            <Routes>
                <Route path="/" element={<Layout/>}>
                    <Route index element={<HomePage/>} />
                    <Route path={"create"} element={<CategoryCreatePage/>} />

                    <Route path={"product"}>
                        <Route path={"list"} element={<ProductListPage/>} />
                        <Route path={"create"} element={<ProductCreatePage/>} />
                    </Route>

                    <Route path={"*"} element={<NotFoundPage/>} />
                </Route>
            </Routes>
        </>
    );
}

export default App;
