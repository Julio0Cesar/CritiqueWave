import axios from 'axios';//biblioteca para fazer GET,POST,PUT,DELETE

const API_URL = "http://localhost:5218/api/"

const axiosInstance = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json',
    }
    
});

export default axiosInstance