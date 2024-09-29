import './App.css';
import {useState} from "react";

function App() {
    const [list, setList] = useState([
        {
            id: 1,
            name: "Ковбаса",
            image: "https://sardelka.com.ua/wp-content/uploads/2023/04/Pryprava-kovbasa-Matsykova-ta-matsik-Poliskyj.jpg"
        }
    ]);
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
                    </tr>
                    </thead>
                    <tbody>
                    {list.map((item) => (
                        <tr>
                            <th scope="row">{item.id}</th>
                            <td>
                                <img src={item.image} alt={item.name} width="75px" />
                            </td>
                            <td>{item.name}</td>
                        </tr>
                    ))}

                    </tbody>
                </table>
            </div>


        </>
    );
}

export default App;
