export const state = () => ({
    orders: [],
    order: {},
    enumOrderStatus: [{
            code: "OnChecking",
            name: "Em Validação",
            color: "yellow",
            icon: "mdi-clock-alert-outline"
        },
        {
            code: "Unapproved",
            name: "Reprovado",
            color: "red",
            icon: "mdi-account-remove-outline"
        },
        {
            code: "Approved",
            name: "Aprovado",
            color: "green",
            icon: "mdi-account-check-outline"
        }
    ]
});

export const mutations = {

    cleanOrder(state) {
        state.order = {};
    },
    cleanStore(state) {
        state.orders = [];
        state.order = {};
    }
};

export const actions = {
    /**
     * Delete
     * @param {*} param0 
     */
    async delete({ commit }, order) {
        try {
            await this.$orderServices.delete(order.Id);
        } catch (error) {
            commit("setError", error, { root: true });
            return [];
        }
    }
};


export const getters = {
    enumOrderStatus: (state) => {
        return state.enumOrderStatus;
    },

    getOrderStatus: (state) => (orderStatus) => {
        return state.enumOrderStatus
            .find(os => os.code === orderStatus);
    }
};