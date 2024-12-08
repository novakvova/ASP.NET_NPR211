import {Link, useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import axios from "axios";
import APP_ENV from "../../../env";
import api from "../../../axiosConfig";

const CategoryEditPage = () => {
    const navigate = useNavigate();
    const {id} = useParams();
    //console.log("id = ", id);

    const [data, setData] = useState({
        id: 0,
        name: "", //назва категорії
        description: "", //опис категорії
        image: "",
        imageFile: null //файлу немає
    });

    useEffect(() => {

        api.get(`api/categories/${id}`)
            .then((response) => {
                setData(response.data); //збегіраю дані у моделі
                console.log('Info category', response);
        });

    }, [id]);

    const handlerOnChange = (e) => {
        setData({...data, [e.target.name]: e.target.value});
    }

    const handlerOnFileChange = (e) => {
        //console.log("Files", e.target.files[0]);
        setData({...data, imageFile: e.target.files[0]});
    }
    //console.log(data);
    const handlerOnSubmit = (e) => {
        e.preventDefault();
        axios.post(`${APP_ENV.URL}api/categories`, data,
            {
                headers: {"Content-Type": "multipart/form-data"},
            })
            .then(res => {
                navigate("/");
            });
        //console.log("State send", data);
    }
    return (
        <>
            <div className={"container"}>
                <h1 className={"text-center"}> Зміна категорії</h1>
                <form onSubmit={handlerOnSubmit} className={"col-md-6 offset-md-3"}>
                    <div className="mb-3">
                        <label htmlFor="name" className="form-label">Назва</label>
                        <input type="text" className="form-control"
                               id="name" name={"name"}
                               value={data.name}
                               onChange={handlerOnChange}
                        />
                    </div>

                    <div className="mb-3">
                        <label htmlFor="imageFile" className="form-label">Фото</label>
                        <input className="form-control" type="file" id="imageFile" name={"imageFile"}
                            onChange={handlerOnFileChange}/>
                    </div>

                    <div className="form-floating mb-3">
                        <textarea className="form-control" placeholder="Вкажіть опис"
                                  name={"description"}
                                  id="description"
                                  value={data.description}
                                  onChange={handlerOnChange}
                                  style={{height: "100px"}}></textarea>
                        <label htmlFor="description">Опис</label>
                    </div>

                    <div className="mb-3 d-flex justify-content-center">
                        <Link to={"/"} className="btn btn-info mx-2">Скасувати</Link>
                        <button type="submit" className="btn btn-primary">Додати</button>
                    </div>

                </form>
            </div>
        </>
    )
}

export default CategoryEditPage;
