import Vue from "vue";
//import axios from "axios";

const ConstTitleApp = "Boticario";
let axios = {};
export default function({ $axios, app, store, redirect, route }) {
    //console.log($axios.defaults.baseURL);
    axios = $axios;
};

// # Obrigatório declarar como Plugin no Nuxt.Config.js
Vue.prototype.$ApiService = {
    query: (resource, params) => {
        return axios.get(resource, params).catch(error => {
            throw new Error(`[${ConstTitleApp}] ApiService ${error}`);
        });
    },

    get: (resource, config) => {
        //console.log(axios.defaults);
        return axios.get(resource, config);
        //.catch(error => {
        // if (error.response) {
        //   // The request was made and the server responded with a status code
        //   // that falls out of the range of 2xx
        //   console.log(error.response.data);
        //   console.log(error.response.status);
        //   console.log(error.response.headers);
        // } else if (error.request) {
        //   // The request was made but no response was received
        //   // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
        //   // http.ClientRequest in node.js
        //   console.log(error.request);
        // } else {
        //   // Something happened in setting up the request that triggered an Error
        //   console.log('Error', error.message);
        // }
        // console.log(error.config);

        //   throw new Error(`[${ConstTitleApp}] ApiService ${error}`);
        // });
    },

    post: (resource, data, config) => {
        return axios.post(`${resource}`, data, config).catch(error => {
            //console.log(error);
            throw new Error(`[${ConstTitleApp}] ApiService ${error}`);
        });
    },

    update: (resource, data) => {
        return axios.put(`${resource}`, data).catch(error => {
            //console.log(error);
            throw new Error(`[${ConstTitleApp}] ApiService ${error}`);
        });
    },

    put: (resource, data) => {
        return axios.put(`${resource}`, data).catch(error => {
            //console.log(error);
            throw new Error(`[${ConstTitleApp}] ApiService ${error}`);
        });
    },

    delete: (resource, data, config) => {
        console.log("delete", resource);
        console.log("delete", data);
        console.log("delete", config);
        return axios.delete(`${resource}`, data, config).catch(error => {
            throw new Error(`[${ConstTitleApp}] ApiService ${error}`);
        });
    }
};