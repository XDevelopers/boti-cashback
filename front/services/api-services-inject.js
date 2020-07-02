// Provide nuxt-axios instance to use same configuration across the whole project
// I've used typical CRUD method names and actions here
// https://stackoverflow.com/questions/46404051/send-object-with-axios-get-request
export default $axios => resource => ({
    get(url) {
        if (url) {
            return $axios.$get(`${resource}/${url}`);
        }
        return $axios.$get(`${resource}`);
    },

    getById(url, id) {
        if (url) {
            return $axios.$get(`${resource}/${url}${id}`);
        }
        return $axios.$get(`${resource}/${id}`);
    },

    insert(url, payload) {
        if (url) {
            return $axios.$post(`${resource}/${url}`, payload);
        }
        return $axios.$post(`${resource}`, payload);
    },

    update(url, payload) {
        if (url) {
            return $axios.$post(`${resource}/${url}`, payload);
        }
        return $axios.$post(`${resource}`, payload);
    },

    delete(url, payload) {
        if (url) {
            return $axios.$delete(`${resource}/${url}`, payload);
        }
        return $axios.$delete(`${resource}`, payload);
    },

    post(url, payload) {
        if (url) {
            return $axios.$post(`${resource}/${url}`, payload);
        }
        return $axios.$post(`${resource}`, payload);
    },

    put(url, payload) {
        if (url) {
            return $axios.$put(`${resource}/${url}`, payload);
        }
        return $axios.$put(`${resource}`, payload);
    }
});