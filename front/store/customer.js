import asArray from "~/utils/as-array";

export const state = () => ({
    customers: [],
    customer: {
        id: '',
        createdAt: 0,
        updatedAt: 0
    },
    enumCustomerStatus: [{
            code: "PendingApproval",
            name: "Aguardando Aprovação",
            color: "yellow",
            icon: "mdi-account-clock"
        },
        {
            code: "PendindDocument",
            name: "Documentação Pendente",
            color: "amber",
            icon: "mdi-account-badge-alert-outline"
        },
        {
            code: "ApprovedLevel1",
            name: "Aprovado Nível 1",
            color: "light-green",
            icon: "mdi-account-arrow-right-outline"
        },
        {
            code: "ApprovedLevel2",
            name: "Aprovado Nível 2",
            color: "green",
            icon: "mdi-account-arrow-right-outline"
        },
        {
            code: "ApprovedLevel3",
            name: "Aprovado Nível 3",
            color: "green darken-3",
            icon: "mdi-account-arrow-right-outline"
        },
        {
            code: "SuspendByAdmin",
            name: "Suspenso pelo Admin",
            color: "red",
            icon: "mdi-account-cancel"
        }
    ]
});

export const mutations = {

    selectCustomer(state, cpf) {
        var index = state.customers.findIndex(t => t['cpf'] === customer.cpf);
        state.customer[index] = customer;
        state.customer = customer;
    },

    cleanCustomer(state) {
        state.customer = {};
    },

    cleanStore(state) {
        state.customers = [];
        state.customer = {};
    },

    setCustomer(state, customer) {
        state.customer = customer;
    },

    setCustomers(state, customers) {
        state.customers = customers;
    },

    deleteDeliveryAddress(state, userId, delivery) {
        let customer = state.customers.find(c => c.userId === userId);
        if (customer && customer.deliveryAddress && customer.deliveryAddress.length > 0) {
            let idx = customer.deliveryAddress
                .findIndex(d => d.cep == delivery.cep && d.logradouro == delivery.logradouro);
            customer.deliveryAddress.splice(idx, 1);

            state.customers.forEach((element, index) => {
                if (element.id === item.id) {
                    state.customers[index] = customer;
                }
            });
        }
    }
};


export const actions = {
    /**
     * Get All Customers
     * @param {*} param0 
     */
    async getCustomers({ commit }) {
        try {
            const response = await this.$customerServices.get("getAllClients");
            commit("setCustomers", asArray(response));
            return asArray(response);
        } catch (error) {
            commit("setError", error, { root: true });
            return [];
        }
    },

    /**
     * Get Customer by UserId
     * @param {*} param0 
     */
    async getCustomerById({ commit }, userId) {
        try {
            const response = await this.$customerServices.getById("getClientDetails?userId=", userId);
            commit("setCustomer", response);
            return response;
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },

    /**
     * Save Customer
     * @param {*} param0 
     */
    async save({ commit }, customer) {
        try {
            const response = await this.$customerServices.update("setClientData", customer);
            return response;
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },


};

export const getters = {
    enumCustomerStatus(state) {
        return state.enumCustomerStatus;
    },
};