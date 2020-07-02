export const state = () => ({
    intentions: [],
    intention: {
        region: "",
        cellphone: "",
        currency: "",
        orderValue: "",
        price: "",
        cpf: "",
        _id: "",
        validity: "",
    },
    enumIntentionStatus: [{
            code: "Requested",
            name: "Solicitado",
            color: "yellow",
            icon: "mdi-clock-alert-outline"
        },
        {
            code: "Declined",
            name: "Recusado",
            color: "red",
            icon: "mdi-account-remove-outline"
        },
        {
            code: "Expired",
            name: "Expirado",
            color: "red",
            icon: "mdi-account-clock-outline"
        },
        {
            code: "Accepted",
            name: "Aceito",
            color: "green",
            icon: "mdi-account-check-outline"
        },
        {
            code: "Effectiveted",
            name: "Efetivado",
            color: "green",
            icon: "mdi-calendar-multiple-check"
        },
    ]
});

export const mutations = {

    selectIntention(state, intention) {
        var index = state.intentions.findIndex(x => x['_id'] === intention._id);
        state.intention[index] = intention;
        state.intention = intention;
    },

    setIntention(state, intention) {
        state.intention = intention;
    },

    setIntentions(state, intentions) {
        //console.log(intentions);
        state.intentions = intentions;
        /*
        intentions.forEach(item => state.intentions.push({
            region: item.region,
            cellphone: item.cellphone,
            currency: item.currency,
            orderValue: item.orderValue,
            price: item.price,
            cpf: item.cpf,
            _id: item._id,
            validity: item.validity,
        }));
        */
    }
};

export const actions = {
    /**
     * Vai na API buscar os Dados
     * @param {*} param0 
     */
    async getAll({ commit }) {
        try {
            const response = await this.$intentionServices.get("getAll");
            commit("setIntentions", response.data);
            return response.data;
        } catch (error) {
            console.log(error);
            commit("setError", error, { root: true });
            return [];
        }
    },

    async getById({ commit }, id) {
        try {
            const response = await this.$intentionServices.getById("getById?intentionId=", id);
            console.log(response);
            commit("setIntention", response);
            return asArray(response);
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },

    async update({ commit }, intention) {
        try {
            const response = await this.$intentionServices.update("update", intention);
            return response;
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },

    async createDiscountByIntention({ commit }, intention) {
        try {
            const response = await this.$intentionServices.update("createDiscountByIntention", intention);
            return response;
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    }
};

export const getters = {
    enumIntentionStatus: (state) => {
        return state.enumIntentionStatus;
    },

    getIntentionStatus: (state) => (status) => {
        return state.enumIntentionStatus
            .find(os => os.code === status);
    },

    getAll: (state) => {
        return state.intentions;
    }
};