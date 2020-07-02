import asArray from "~/utils/as-array";

const apiUrl = process.env.BASE_API_URL;
// ShopRules
const apiGetShopRules = `${apiUrl}/admin/shopRules/getShopRules`;
const apiUpdateShopRules = `${apiUrl}/admin/shopRules/update`;
// Bank Account
const apiCreateBankAccount = `${apiUrl}/admin/bank/createAccount`;
const apiUpdateBankAccount = `${apiUrl}/admin/bank/update`;
const apiDeleteBankAccount = `${apiUrl}/admin/bank/delete`;
// Currency
const apiGetAllCurrencies = `${apiUrl}/admin/currency/getAll`;
const apiCreateCurrency = `${apiUrl}/admin/currency/create`;
const apiUpdateCurrency = `${apiUrl}/admin/currency/update`;
const apiDeleteCurrency = `${apiUrl}/admin/currency/delete`;
// Configuration
const apiGetConfigurations = `${apiUrl}/admin/configuration/getConfigurations`;
const apiUpdateConfiguration = `${apiUrl}/admin/configuration/update`;
// Region
const apiGetAllRegions = `${apiUrl}/admin/region/getRegions`;
const apiCreateRegion = `${apiUrl}/admin/region/create`;
const apiUpdateRegion = `${apiUrl}/admin/region/update`;
const apiDeleteRegion = `${apiUrl}/admin/region/delete`;
// Definition
const apiGetAllDefinitions = `${apiUrl}/admin/definitions/getDefinitions`;
const apiCreateDefinition = `${apiUrl}/admin/definitions/create`;
const apiUpdateDefinition = `${apiUrl}/admin/definitions/update`;
const apiDeleteDefinition = `${apiUrl}/admin/definitions/delete`;

export const state = () => ({
    shopRules: [],
    bankAccount: null,
    bankAccounts: [],
    currency: null,
    currencies: [],
    configuration: {
        iofCash: "",
        iofCard: "",
        minimalAge: "",
        siteTimeout: "",
        adminTimeout: "",
        _id: "",
        regionId: "",
    },
    region: null,
    regions: [],
    definition: null,
    definitions: [],
    requiredDocuments: [{
            text: "RG, CNH, Passporte",
            value: "ID"
        },
        {
            text: "Compr. Residência",
            value: "proofAddress"
        },
        {
            text: "Imposto de Renda",
            value: "incomeTax"
        }
    ]
});

export const mutations = {

    /**
     * Preenche a lista de Dados Bancários
     * @param {*} state 
     * @param {*} bankAccounts 
     */
    setBankAccounts(state, bankAccounts) {
        state.bankAccounts = [];
        bankAccounts.map(b => { state.bankAccounts.push(b) });
    },

    /**
     * Preenche as regras de compra
     * @param {*} state 
     * @param {*} shopRules 
     */
    setShopRules(state, shopRules) {
        state.shopRules = shopRules;
    },

    /**
     * Atualiza as Regras de Compra
     * @param {*} state 
     * @param {*} updatedItem 
     */
    updateShopRules(state, updatedItem) {
        state.shopRules = [
            ...state.shopRules.map(item => item.key !== updatedItem.key ? item : {...item, ...updatedItem })
        ];
    },



    /**
     * Preenche a lista de Currencies
     * @param {*} state 
     * @param {*} currencies 
     */
    setCurrencies(state, currencies) {
        state.currencies = currencies;
    },

    /**
     * Insere a nova Currency
     * @param {*} state 
     * @param {*} newItem 
     */
    setCurrency(state, newItem) {
        state.currencies.push(newItem);
    },

    /**
     * Atualiza as Currencies
     * @param {*} state 
     * @param {*} updatedItem 
     */
    updateCurrency(state, updatedItem) {
        state.currencies = [
            ...state.currencies.map(item => item._id !== updatedItem._id ? item : {...item, ...updatedItem })
        ];
    },

    /**
     * Delete Currency
     * @param {*} state 
     * @param {*} removeItem 
     */
    deleteCurrency(state, removeItem) {
        let index = state.currencies.findIndex(i => i._id === removeItem._id);
        state.currencies.splice(index, 1);
    },


    /**
     * Set the State related to Configuration object!
     * @param {*} state 
     * @param {*} configuration 
     */
    setConfiguration(state, configuration) {
        state.configuration = configuration;
    },

    setRegions(state, regions) {
        state.regions = regions;
    },

    setDefinitions(state, definitions) {
        state.definitions = definitions;
    },
};

export const actions = {
    /**
     * Vai na API buscar as Regras de Compra
     * @param {*} param0 
     */
    getShopRules({ commit }) {
        return new Promise((resolve, reject) => {
            // Make network request and fetch data
            // and commit the data
            //this.$axios.get("/users")
            this.$axios.get(apiGetShopRules)
                .then(response => {
                    commit("setShopRules", asArray(response.data));
                    resolve(response);
                })
                .catch(error => {
                    commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Irá atualizar as Regras de Compra
     * @param {*} param0 
     */
    updateShopRules(context, payload) {
        return new Promise((resolve, reject) => {
            this.$axios.post(apiUpdateShopRules, payload)
                .then(response => {
                    context.dispatch("getShopRules");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Vai na API buscar os Dados Bancários
     * @param {*} param0 
     */
    async getBankAccounts({ commit }) {
        try {
            const response = await this.$bankServices.get("getAccounts");
            //console.log(response);
            commit("setBankAccounts", asArray(response));
            return asArray(response);
        } catch (error) {
            commit("setError", error, { root: true });
            return [];
        }
    },

    /**
     * Criará novo registro dos Dados Bancários
     * @param {*} param0 
     */
    createBankAccount(context, payload) {
        return new Promise((resolve, reject) => {
            this.$axios.post(apiCreateBankAccount, payload)
                .then(response => {
                    //context.dispatch("getBankAccounts");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Irá atualizar um registro dos Dados Bancários pelo ID
     * @param {*} param0 
     */
    updateBankAccount(context, payload) {
        return new Promise((resolve, reject) => {
            this.$axios.post(apiUpdateBankAccount, payload)
                .then(response => {
                    context.dispatch("getBankAccounts");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Irá deletar um registro dos Dados Bancários pelo ID
     * @param {*} param0 
     */
    deleteBankAccount(context, payload) {
        //console.log("deleteBankAccount", payload);
        let data = { _id: payload._id };
        return new Promise((resolve, reject) => {
            this.$axios.post(apiDeleteBankAccount, data)
                .then(response => {
                    context.dispatch("getBankAccounts");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },



    getCurrencies({ commit }) {
        return new Promise((resolve, reject) => {
            this.$axios.get(apiGetAllCurrencies)
                .then(response => {
                    let results = asArray(response.data);
                    commit("setCurrencies", results);
                    resolve(response);
                    return results;
                })
                .catch(error => {
                    commit("setError", error, { root: true });
                    reject(error);
                    return [];
                });
        });
    },

    /**
     * Criará novo registro
     * @param {*} param0 
     */
    createCurrency(context, payload) {
        //console.log("createCurrency", payload);
        return new Promise((resolve, reject) => {
            this.$axios.post(apiCreateCurrency, payload)
                .then(response => {
                    context.dispatch("getCurrencies");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Irá atualizar um registro
     * @param {*} param0 
     */
    updateCurrency(context, payload) {
        //console.log("updateCurrency", payload);
        return new Promise((resolve, reject) => {
            this.$axios.post(apiUpdateCurrency, payload)
                .then(response => {
                    context.dispatch("getCurrencies");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Irá deletar um registro pelo ID
     * @param {*} param0 
     */
    deleteCurrency(context, payload) {
        //console.log("deleteCurrency", payload);
        let data = { _id: payload._id };
        return new Promise((resolve, reject) => {
            this.$axios.post(apiDeleteCurrency, data)
                .then(response => {
                    context.dispatch("getCurrencies");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },


    /**
     * Vai na API buscar as Configuration
     * @param {*} param0 
     */
    getConfigurations({ commit }) {
        return new Promise((resolve, reject) => {
            this.$axios.get(apiGetConfigurations)
                .then(response => {
                    commit("setConfiguration", response.data);
                    resolve(response);
                })
                .catch(error => {
                    commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Atualizará o objeto de configuration
     * @param {*} param0 
     */
    updateConfiguration(context, payload) {
        return new Promise((resolve, reject) => {
            this.$axios.post(apiUpdateConfiguration, payload)
                .then(response => {
                    context.dispatch("getConfigurations");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },



    // getRegions({ commit }) {
    //     return new Promise((resolve, reject) => {
    //         this.$axios.get(apiGetAllRegions)
    //             .then(response => {
    //                 commit("setRegions", asArray(response.data));
    //                 resolve(response);
    //             })
    //             .catch(error => {
    //                 commit("setError", error, { root: true });
    //                 reject(error);
    //             });
    //     });
    // },
    async getRegions({ commit }) {
        try {
            const response = await this.$regionServices.get("getRegions");
            commit("setRegions", asArray(response));
            return asArray(response);
        } catch (error) {
            commit("setError", error, { root: true });
            return [];
        }
    },

    /**
     * Criará novo registro
     * @param {*} param0 
     */
    createRegion(context, payload) {
        //console.log("createRegion", payload);
        return new Promise((resolve, reject) => {
            this.$axios.post(apiCreateRegion, payload)
                .then(response => {
                    context.dispatch("getRegions");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Irá atualizar um registro
     * @param {*} param0 
     */
    updateRegion(context, payload) {
        //console.log("updateRegion", payload);
        return new Promise((resolve, reject) => {
            this.$axios.post(apiUpdateRegion, payload)
                .then(response => {
                    context.dispatch("getRegions");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Irá deletar um registro pelo ID
     * @param {*} param0 
     */
    deleteRegion(context, payload) {
        //console.log("deleteRegion", payload);
        let data = { _id: payload._id };
        return new Promise((resolve, reject) => {
            this.$axios.post(apiDeleteRegion, data)
                .then(response => {
                    context.dispatch("getRegions");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },



    getDefinitions({ commit }) {
        return new Promise((resolve, reject) => {
            this.$axios.get(apiGetAllDefinitions)
                .then(response => {
                    commit("setDefinitions", asArray(response.data));
                    resolve(response);
                })
                .catch(error => {
                    commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Criará novo registro
     * @param {*} param0 
     */
    createDefinition(context, payload) {
        //console.log("createDefinition", payload);
        return new Promise((resolve, reject) => {
            this.$axios.post(apiCreateDefinition, payload)
                .then(response => {
                    context.dispatch("getDefinitions");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Irá atualizar um registro
     * @param {*} param0 
     */
    updateDefinition(context, payload) {
        //console.log("updateDefinition", payload);
        return new Promise((resolve, reject) => {
            this.$axios.post(apiUpdateDefinition, payload)
                .then(response => {
                    context.dispatch("getDefinitions");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },

    /**
     * Irá deletar um registro pelo ID
     * @param {*} param0 
     */
    deleteDefinition(context, payload) {
        //console.log("deleteDefinition", payload);
        let data = { _id: payload._id };
        return new Promise((resolve, reject) => {
            this.$axios.post(apiDeleteDefinition, data)
                .then(response => {
                    context.dispatch("getDefinitions");
                    resolve(response);
                })
                .catch(error => {
                    context.commit("setError", error, { root: true });
                    reject(error);
                });
        });
    },


};

export const getters = {
    getShopRules(state) {
        return state.shopRules;
    },

    getBankAccounts(state) {
        return state.bankAccounts;
    },

    getCurrencies(state) {
        return state.currencies;
    },

    getConfigurations(state) {
        return state.configuration;
    },

    getRegions(state) {
        return state.regions;
    },

    getDefinitions(state) {
        return state.definitions;
    },

    getDefinitionsByRegion: state => regionId => {
        return asArray(state.definitions).filter(d => {
            return d.regionId === regionId;
        });
    },

    getRequiredDocuments(state) {
        return state.requiredDocuments;
    },

    getMinimalAge(state) {
        return state.configuration.minimalAge;
    }
};