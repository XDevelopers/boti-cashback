import asArray from "~/utils/as-array";
const qs = require("querystring");
const apiUrl = process.env.BASE_API_URL;
const apiUploadDocument = `${apiUrl}/admin/documents/uploadDocument`;

export const state = () => ({
    documents: [],
    document: {
        id: '',
        documentType: "", //idFront, idVerse
        thumbnail: ""
    }
});

export const mutations = {

    setDocument(state, document) {
        state.document = document;
    },

    setDocuments(state, documents) {
        state.documents = documents;
    }
};


export const actions = {
    /**
     * Get All Documents by UserId
     * @param {*} param0 
     */
    async getDocuments({ commit }, userId) {
        try {
            const response = await this.$documentsServices.get(`getThumbDocsByUserId?userId=${userId}`);
            //commit("setDocuments", asArray(response));
            return asArray(response);
        } catch (error) {
            commit("setError", error, { root: true });
            return [];
        }
    },

    async uploadDocument({ commit }, document) {
        try {
            let toString = Object.prototype.toString;
            // const response = await this.$adminUserServices.insert("create", adminUser);
            // //console.log(response);
            // //commit("setInsurance", response);
            // return response;
            return new Promise((resolve, reject) => {
                const config = {
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded"
                    }
                };
                return this.$axios.post(`${apiUploadDocument}`, qs.stringify(document), config)
                    .then(response => {
                        resolve(response);
                        return response;
                    })
                    .catch(error => {
                        commit("setError", error, { root: true });
                        reject(error);
                        console.error(error);
                        return null;
                    });
            });
        } catch (error) {
            commit("setError", error, { root: true });
            return null;
        }
    }
};

export const getters = {};