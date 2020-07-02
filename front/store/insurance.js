export const state = () => ({
    insurances: [],
    insurance: {
        originCountryCode: "",
        destinationCountryCode: "",
        totalPassenger: 0,
        profile: "",
        email: "",
        cpf: "",
        _id: "",
        travelStart: "",
        travelEnd: "",
    },
    enumInsuranceStatus: [{
            code: "WaitingForQuote",
            name: "Aguardando Cotação",
            color: "yellow",
            icon: "mdi-progress-clock"
        },
        {
            code: "QuoteSent",
            name: "Cotação Enviada",
            color: "blue",
            icon: "mdi-email-send-outline"
        },
        {
            code: "Contracted",
            name: "Contratado",
            color: "green",
            icon: "mdi-checkbox-marked-circle-outline"
        },
        {
            code: "CanceledByCustomer",
            name: "Cancelado Pelo Cliente",
            color: "red",
            icon: "mdi-cart-remove"
        },
    ],
    enumProfile: [{
            text: "Individual",
            value: "individual"
        },
        {
            text: "Família",
            value: "group"
        }
    ]
});

export const mutations = {

    setInsurance(state, insurance) {
        state.insurance = insurance;
    },

    setInsurances(state, insurances) {
        //console.log(insurances);
        state.insurances = insurances;
    }
};

export const actions = {
    /**
     * Vai na API buscar os Dados
     * @param {*} param0 
     */
    async getAll({ commit }) {
        try {
            const response = await this.$insuranceServices.get("getAll");
            //console.log(response.data);
            commit("setInsurances", response.data);
            return response.data;
        } catch (error) {
            //console.log(error);
            commit("setError", error, { root: true });
            return [];
        }
        // return new Promise((resolve, reject) => {
        //   this.$axios.get(apiGetBankAccounts)
        //     .then(response => {
        //       commit("setBankAccounts", asArray(response.data));
        //       resolve(response);
        //     })
        //     .catch(error => {
        //       commit("setError", error, { root: true });
        //       reject(error);
        //     });
        // });
    },

    async getById({ commit }, id) {
        try {
            const response = await this.$insuranceServices.getById("getById?insuranceId=", id);
            console.log(response);
            commit("setInsurance", response);
            return asArray(response);
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    },

    async update({ commit }, insurance) {
        try {
            const response = await this.$insuranceServices.update("update", insurance);
            return response;
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    }
};

export const getters = {
    enumInsuranceStatus: (state) => {
        return state.enumInsuranceStatus;
    },

    getInsuranceStatus: (state) => (status) => {
        return state.enumInsuranceStatus
            .find(os => os.code === status);
    },

    getAll: (state) => {
        return state.insurances;
    },

    enumProfile: (state) => {
        return state.enumProfile;
    }
};