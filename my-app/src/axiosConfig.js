// src/axiosConfig.js
import axios from 'axios';
import APP_ENV from "./env";

const api = axios.create({
    baseURL: APP_ENV.URL, // Replace with your API base URL
    headers: {
        'Content-Type': 'application/json',
    },
});

export default api;
