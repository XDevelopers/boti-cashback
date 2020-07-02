import asArray from "~/utils/as-array";

export const state = () => ({
    states: [
        /*
        {
            id: "12",
            initials: "AC",
            name: "Acre"
        },
        {
            id: "27",
            initials: "AL",
            name: "Alagoas"
        },
        {
            id: "16",
            initials: "AP",
            name: "Amapá"
        },
        {
            id: "13",
            initials: "AM",
            name: "Amazonas"
        },
        {
            id: "29",
            initials: "BA",
            name: "Bahia"
        },
        {
            id: "23",
            initials: "CE",
            name: "Ceará"
        },
        {
            id: "53",
            initials: "DF",
            name: "Distrito Federal"
        },
        {
            id: "32",
            initials: "ES",
            name: "Espírito Santo"
        },
        {
            id: "52",
            initials: "GO",
            name: "Goiás"
        },
        {
            id: "21",
            initials: "MA",
            name: "Maranhão"
        },
        {
            id: "51",
            initials: "MT",
            name: "Mato Grosso"
        },
        {
            id: "50",
            initials: "MS",
            name: "Mato Grosso do Sul"
        },
        {
            id: "31",
            initials: "MG",
            name: "Minas Gerais"
        },
        {
            id: "41",
            initials: "PR",
            name: "Paraná"
        },
        {
            id: "25",
            initials: "PB",
            name: "Paraíba"
        },
        {
            id: "15",
            initials: "PA",
            name: "Pará"
        },
        {
            id: "26",
            initials: "PE",
            name: "Pernambuco"
        },
        {
            id: "22",
            initials: "PI",
            name: "Piauí"
        },
        {
            id: "24",
            initials: "RN",
            name: "Rio Grande do Norte"
        },
        {
            id: "43",
            initials: "RS",
            name: "Rio Grande do Sul"
        },
        {
            id: "33",
            initials: "RJ",
            name: "Rio de Janeiro"
        },
        {
            id: "11",
            initials: "RO",
            name: "Rondônia"
        },
        {
            id: "14",
            initials: "RR",
            name: "Roraima"
        },
        {
            id: "42",
            initials: "SC",
            name: "Santa Catarina"
        },
        {
            id: "28",
            initials: "SE",
            name: "Sergipe"
        },
        {
            id: "35",
            initials: "SP",
            name: "São Paulo"
        },
        {
            id: "17",
            initials: "TO",
            name: "Tocantins"
        }
        */
    ],
    cities: [],
    enumMaritalStatus: [{
        code: "casado",
        name: "Casado(a)",
        color: "",
        icon: ""
    }, {
        code: "solteiro",
        name: "Solteiro(a)",
        color: "",
        icon: ""
    }, {
        code: "viuvo",
        name: "Viúvo(a)",
        color: "",
        icon: ""
    }, {
        code: "divorciado",
        name: "Divorciado(a)",
        color: "",
        icon: ""
    }, ]
});

export const mutations = {
    setStates(state, states) {
        state.states = states;
    }

};

export const actions = {
    /**
     * Get de todos os estados da API
     * @param {*} param0 
     */
    async getStates({ commit }) {
        try {
            const response = await this.$regionServices.get("getState");
            //console.log(response);
            commit("setStates", asArray(response));
        } catch (error) {
            commit("setError", error, { root: true });
        }
    }
};

export const getters = {
    states(state) {
        return state.states;
    },
    cities(state, stateCode) {
        return state.cities.filter(c => c.state === stateCode);
    },
    enumMaritalStatus(state) {
        return state.enumMaritalStatus;
    }
};