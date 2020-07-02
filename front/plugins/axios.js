export default function({ $axios, app, store, redirect, route }) {
    //console.log("app.Properties", Object.getOwnPropertyNames(app));
    //console.log("app.context", Object.getOwnPropertyNames(app.context));
    //console.log("localStorage", app.$auth.options.localStorage);

    // $axios.defaults.baseURL = 'https://myaddress/api';
    $axios.onRequest(config => {
        // TODO: M.L. Rever isso com o Pai da Criança (Wesley)
        if (config.url.includes('http')) { // don't use BaseURL
            config.headers.accept = 'application/json';
            // Do not run on server
            if (process.server) {
                return;
            }
            config.headers["Authorization"] = localStorage.getItem("auth._token.local");
            //console.log(localStorage.getItem("auth._token.local"));
        } else {
            config.headers.accept = 'application/json';
            // Do not run on server
            if (process.server) {
                return;
            }
            //config.headers["Authorization"] = window.localStorage.getItem("auth._token.local");
            // let isNotNull = typeof tenant.apiKey != "undefined"
            //                     && tenant.apiKey != null
            //                     && tenant.apiKey.length != null
            //                     && tenant.apiKey.length > 0;
            // if (isNotNull) {
            //     config.headers['X-API-KEY'] = tenant.apiKey;
            // }
        }
    });

    $axios.onError((res) => {
        //console.info("onError", res);
        if (res && res.response) {
            console.log(res.response);
            if (res.response.status === 401) {
                redirect("/login");
                return;
            }
            return res.response;
        }
        // Http StatusCode (408) === Timeout
        if (res && res.response && res.response.status && res.response.status === 408) {
            console.info(`Tentar novamente`, res.config)
            return axios.request(res.config)
        }
    });

    // $axios.onError(error => {
    //     const code = parseInt(error.response && error.response.status);
    //     if (code === 401) {
    //         console.log(401);
    //         store.commit('setUser', null);
    //         redirect('/login');
    //     }
    // });
}