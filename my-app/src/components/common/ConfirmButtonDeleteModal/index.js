import "./style.css";
import {useRef} from "react";

const ConfirmButtonDeleteModal = (model) => {
    const {id, title, body} = model;
    const modalCloseRef = useRef(null);
    const modalId = `deleteModal${id}`;

    const handleConfirmDelete = () => {
        console.log("Delete id", id);
        modalCloseRef.current.click();
    }
    return (
        <>
            <i className="bi bi-trash3-fill delete-clickable"
               data-bs-toggle="modal" data-bs-target={`#${modalId}`}></i>

            <div className="modal fade" id={`${modalId}`} tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h1 className="modal-title fs-5" id="exampleModalLabel">{title}</h1>
                            <button ref={modalCloseRef} type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            <p className={"fs-3"}>{body}</p>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Скасувати</button>
                            <button type="button" className="btn btn-danger"
                                onClick={handleConfirmDelete}>Видалити</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default ConfirmButtonDeleteModal;
