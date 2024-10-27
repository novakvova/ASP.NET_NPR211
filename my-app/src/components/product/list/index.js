import {useEffect, useState} from "react";
import api from "../../../axiosConfig";
import {Link} from "react-router-dom";
import APP_ENV from "../../../env";

const ProductListPage = () => {

    const [list, setList] = useState([]);
    useEffect(() => {
        console.log("Render ProductListPage");
        api.get("api/products").then(resp => {
            // console.log("Get products", data);
            setList(resp.data);
        });
    }, []);

    return (
        <>
            <div className="container">
                <h1 className="text-center">Продукти</h1>
                <Link to={"/product/create"} className={"btn btn-success"}>Додати</Link>
                {/*<button className={"btn btn-success"} onClick={handleAddButton}>Додати новий</button>*/}
                <table className="table">
                    <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Фото</th>
                        <th scope="col">Назва</th>
                        <th scope="col">Ціна</th>
                        <th scope="col">Категорія</th>
                    </tr>
                    </thead>
                    <tbody>
                    {list.map((item) => (
                        <tr key={item.id}>
                            <th scope="row">{item.id}</th>
                            <td>
                                <img src={`${APP_ENV.URL}images/150_${item.images[0]}`} alt={item.name} width="75px"/>
                            </td>
                            <td>{item.name}</td>
                            <td>{item.price}</td>
                            <td>{item.categoryName}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>


        </>
    );
}

export default ProductListPage;
