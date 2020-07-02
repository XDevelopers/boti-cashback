const qs = require("querystring");
const apiUrl = process.env.BASE_API_URL;
const adminUserUrl = `${apiUrl}/admin/userAdmin/`;

export const state = () => ({
    adminUsers: [],
    adminUser: {
        name: "",
        cpf: "",
        email: "",
        active: false,
        roles: [
            "BackOffice",
            "Diamante"
        ],
        _id: ""
    },
    roles: [],
    role: {
        status: "",
        actions: [],
        _id: "",
        name: "",
        createdAt: "",
        updatedAt: "",
    }
});

export const mutations = {

    setAdminUser(state, adminUser) {
        state.adminUser = adminUser;
    },

    setAdminUsers(state, adminUsers) {
        state.adminUsers = adminUsers;
    },

    setRoles(state, roles) {
        state.roles = roles;
    },
};

export const actions = {
    /**
     * Vai na API buscar os Dados
     * @param {*} param0 
     */
    async getAll({ commit }) {
        try {
            const response = await this.$adminUserServices.get("getAll");
            commit("setAdminUsers", response);
            return response;
        } catch (error) {
            //console.log(error);
            commit("setError", error, { root: true });
            return [];
        }
    },

    async create({ commit }, adminUser) {
        try {
            let toString = Object.prototype.toString;
            // const response = await this.$adminUserServices.insert("create", adminUser);
            // //console.log(response);
            // //commit("setInsurance", response);
            // return response;
            return new Promise((resolve, reject) => {
                const requestBody = {
                    name: adminUser.name,
                    cpf: adminUser.cpf,
                    password: adminUser.password,
                    avatar: "",
                    roles: toString.call(adminUser.roles)
                };

                const config = {
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded"
                    }
                };
                this.$axios.post(`${adminUserUrl}create`, qs.stringify(requestBody), config)
                    .then(response => {
                        resolve(response);
                    })
                    .catch(error => {
                        commit("setError", error, { root: true });
                        reject(error);
                    });
            });
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },

    async update({ commit }, adminUser) {
        try {
            // const response = await this.$adminUserServices.update("update", adminUser);
            // //console.log(response);
            // //commit("setInsurance", response);
            // return response;
            return new Promise((resolve, reject) => {
                const requestBody = {
                    name: adminUser.name,
                    cpf: adminUser.cpf,
                    password: adminUser.password,
                    avatar: "",
                    roles: adminUser.roles.toString()
                };

                const config = {
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded"
                    }
                };
                this.$axios.post(`${adminUserUrl}create`, qs.stringify(requestBody), config)
                    .then(response => {
                        resolve(response);
                    })
                    .catch(error => {
                        commit("setError", error, { root: true });
                        reject(error);
                    });
            });
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },

    async delete({ commit }, adminUser) {
        try {
            let payload = { id: adminUser._id };
            return await this.$adminUserServices.delete("delete", payload);
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },

    /**
     * Vai na API todas as Roles
     * @param {*} param0 
     */
    async getAllRoles({ commit }) {
        try {
            const response = await this.$roleServices.get("getAll");
            commit("setRoles", response);
            return response;
        } catch (error) {
            commit("setError", error, { root: true });
            return [];
        }
    },

    async changePassword({ commit }, adminUser) {
        try {
            return await this.$adminUserServices.update("changePassword", adminUser);
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },
};

export const getters = {

    getAll: (state) => {
        return state.adminUsers;
    },

    getAllRoles: (state) => {
        return state.roles;
    },
};