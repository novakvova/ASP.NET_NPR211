import {useEffect, useState} from "react";
import {Link} from "react-router-dom";
import APP_ENV from "../../env";
import api from "../../axiosConfig";
import ConfirmButtonDeleteModal from "../common/ConfirmButtonDeleteModal";

const HomePage = () => {
    const [list, setList] = useState([]);

    useEffect(() => {
        api.get(`api/categories`)
            .then(res => {
                //console.log("data server", res);
                setList(res.data);
            });
    },[]);

    const onDeleteHandler = (id) => {
        //console.log("Delete item", id);
        api.delete(`api/categories/${id}`)
            .then(() =>
                setList(list.filter((item) => item.id !== id)));
    }

    return (
        <>
            <div className="container">
                <h1 className="text-center">Список категорій</h1>
                <Link to={"/create"} className={"btn btn-success"}>Додати</Link>
                {/*<button className={"btn btn-success"} onClick={handleAddButton}>Додати новий</button>*/}
                <table className="table">
                    <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Фото</th>
                        <th scope="col">Назва</th>
                        <th scope="col">Опис</th>
                        <th scope="col"></th>
                    </tr>
                    </thead>
                    <tbody>
                    {list.map((item) => (
                        <tr key={item.id}>
                            <th scope="row">{item.id}</th>
                            <td>
                                <img src={`${APP_ENV.URL}images/150_${item.image}`} alt={item.name} width="75px"/>
                            </td>
                            <td>{item.name}</td>
                            <td>{item.description}</td>
                            <td>
                                <ConfirmButtonDeleteModal id={item.id}
                                                          title={"Ви впевненні у видалені категорії"}
                                                          body={`Видалити категорію "${item.name}"?`}
                                                          onDelete = {onDeleteHandler}
                                />
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </>
    )
}

export default HomePage;
