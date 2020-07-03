export default function({ $axios, app, store, redirect, route }) {
    //console.log("app.Properties", Object.getOwnPropertyNames(app));
    //console.log("app.context", Object.getOwnPropertyNames(app.context));
    //console.log("localStorage", app.$auth.options.localStorage);


    // $axios.onError((res) => {
    //     //console.info("onError", res);
    //     if (res && res.response) {
    //         console.log(res.response);
    //         if (res.response.status === 401) {
    //             //redirect("/login");
    //             throw new Error();
    //         }
    //         return res;
    //     }
    //     // Http StatusCode (408) === Timeout
    //     if (res && res.response && res.response.status && res.response.status === 408) {
    //         console.info(`Tentar novamente`, res.config)
    //         return axios.request(res.config)
    //     }
    // });

    ///
    /// https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status
    ///
    // $axios.onError((res) => {
    //     if (res && res.response) {
    //         console.log("axios -> onError:\n", res.response);
    //         if (res.response.status === 408) {
    //             // Http StatusCode (408) === Timeout
    //             console.info("Try Again", res.config);
    //             return $axios.request(res.config);
    //         }
    //         if (res.response.status === 405) {
    //             // Http StatusCode (405) === Method Not Allowed
    //             console.info("Method Not Allowed", res.config);
    //             return $axios.request(res.config);
    //         }
    //         if (res.response.status === 403) {
    //             // Http StatusCode (403) === Access Forbidden
    //             console.info("Access Forbidden ", res.config);
    //             redirect("/solution-monitor");
    //         }
    //         if (res.response.status === 401) {
    //             // Http StatusCode (401) === Unauthorized
    //             console.log("Unauthorized", res.config);
    //             redirect("/login");
    //             //return router.push("/login");
    //             return res;
    //         }
    //         if (res.response.status === 400) {
    //             console.info("Bad Request", res.config);
    //             if (res.response.data) {
    //                 return res.data;
    //             }
    //         }
    //         return res.response;
    //     }
    // });
}