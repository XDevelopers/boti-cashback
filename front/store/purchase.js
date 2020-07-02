export const state = () => ({
    purchases: [],
    purchase: {
        "purchaseId": "",
        "currency": "",
        "value": "",
        "siteTax": "",
        "total": "",
        "calculatedSpread": "",
        "orderStatus": "",
        "createdAt": "",
        "updatedAt": "",
        "iof": "",
        "calculatedIof": "",
        "yearTotalPurchase": "",
        "totalPurchaseInCash": "",
        "name": "",
        "email": "",
        "phoneNumber": "",
        "status": "",
        "comercialTax": "",
        "spreadCash": "",
        "spreadBank": "",
        "payOption": "",
        "bankTaxNow": "",
        "cashResult": "",
        "delivery": "",
        "address": {
            "_id": "",
            "cep": "",
            "street": "",
            "number": "",
            "complement": "",
            "city": "",
            "state": ""
        },
        "vet": "",
        "calculatedSpreadRS": "",
        "observation": "",
        "totalOrderValue": "",
        "proofPayment": "",
        "thumbProofPayment": "",
        "region": ""
    },
    enumOrderStatus: [{
            code: "PendingPayment",
            name: "Aguardando Pagamento",
            color: "yellow",
            icon: "mdi-clock-alert-outline"
        },
        {
            code: "CanceledByCustomer",
            name: "Cancelado pelo Cliente",
            color: "red",
            icon: "mdi-account-remove-outline"
        },
        {
            code: "CanceledByAdmin",
            name: "Cancelado pelo Admin",
            color: "red",
            icon: "mdi-account-off-outline"
        },
        {
            code: "ExpiredTime",
            name: "Tempo Expirado",
            color: "red",
            icon: "mdi-account-clock-outline"
        },
        {
            code: "Done",
            name: "Concluído",
            color: "green",
            icon: "mdi-account-check-outline"
        },
        {
            code: "InReview",
            name: "Em Análise",
            color: "amber",
            icon: "mdi-account-badge-alert-outline"
        },
        {
            code: "InProcess",
            name: "Em Processamento",
            color: "blue",
            icon: "mdi-account-convert"
        }
    ]
});

export const mutations = {

    selectPurchase(state, cpf) {
        var index = state.purchases.findIndex(t => t['cpf'] === purchase.cpf);
        state.purchase[index] = purchase;
        state.purchase = purchase;
    },

    cleanPurchase(state) {
        state.purchase = {};
    },
    cleanStore(state) {
        state.purchases = [];
        state.purchase = {};
    },

    setPurchases(state, purchases) {
        purchases.forEach(item => state.purchases.push({

            name: item.name,
            address: item.address,
            id: item.id,
            createdAt: item.createdAt,
            updatedAt: item.updatedAt
        }));
    }
};

export const getters = {
    enumOrderStatus: (state) => {
        return state.enumOrderStatus;
    },

    getOrderStatus: (state) => (orderStatus) => {
        return state.enumOrderStatus
            .find(os => os.code === orderStatus);
    },
};