import asArray from "~/utils/as-array";

export const state = () => ({
    roles: [],
    role: {
        _id: "",
        status: "",
        actions: [],
        name: "",
        createdAt: "",
        updatedAt: ""
    },
    actions: []
});

export const mutations = {

    setRole(state, payload) {
        state.role = payload;
    },

    setRoles(state, payload) {
        state.roles = payload;
    },

    setActions(state, payload) {
        state.actions = payload;
    }
};


export const actions = {
    /**
     * Get All Documents by UserId
     * @param {*} param0 
     */
    async getRoles({ commit }) {
        try {
            const response = await this.$roleServices.get("getAll");
            commit("setRoles", asArray(response));
            return asArray(response);
        } catch (error) {
            commit("setError", error, { root: true });
            return [];
        }
    },


    async createRoles({ commit }, payload) {
        try {
            const response = await this.$roleServices.insert("create", payload);
            //commit("setRoles", asArray(response));
            return response;
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },


    async updateRoles({ commit }, payload) {
        try {
            const response = await this.$roleServices.update("update", payload);
            //commit("setRoles", asArray(response));
            return response;
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },
};

export const getters = {
    getActions(state) {
        return state.actions;
    },
};