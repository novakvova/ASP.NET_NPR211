import {useEffect, useState} from "react";
import axios from "axios";

const HomePage = () => {
    const [list, setList] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5290/api/categories')
            .then(res => {
                //console.log("data server", res);
                setList(res.data);
            });
        // setList([{
        //     id: 1,
        //     name: "Ковбаса",
        //     image: "https://sardelka.com.ua/wp-content/uploads/2023/04/Pryprava-kovbasa-Matsykova-ta-matsik-Poliskyj.jpg"
        // }]);
    },[]);
    console.log("List items", list);
    const handleAddButton = () => {
        setList([
            ...list, {
                id: 2,
                name: "Масло",
                image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXPA1yX3G4SdQRpEDjr56wVaYCPqJwsxsdVg&s"
            }]);
    }
    return (
        <>
            <div className="container">
                <h1 className="text-center">Список категорій</h1>
                <button className={"btn btn-success"} onClick={handleAddButton}>Додати новий</button>
                <table className="table">
                    <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Фото</th>
                        <th scope="col">Назва</th>
                        <th scope="col">Опис</th>
                    </tr>
                    </thead>
                    <tbody>
                    {list.map((item) => (
                        <tr key={item.id}>
                            <th scope="row">{item.id}</th>
                            <td>
                                <img src={"http://localhost:5290/images/" + item.image} alt={item.name} width="75px"/>
                            </td>
                            <td>{item.name}</td>
                            <td>{item.description}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </>
    )
}

export default HomePage;