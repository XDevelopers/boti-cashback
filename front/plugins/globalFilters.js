import Vue from "vue";
import asArray from "~/utils/as-array";

let moment = {};
let store = {};
let getters = {};
export default function({ $moment, store }) {
    moment = $moment;
    store = store;
    getters = store.getters;
};



Vue.filter("formatDatetime", (val) => {
    // 2020-01-31T03:00:00.000Z
    return moment(val, "YYYY-MM-DDTHH:mm:ss.SSSSZ")
        .format("DD/MM/YYYY HH:mm:ss");
});



Vue.filter("formatDate", (val) => {
    return moment(val, "YYYY-MM-DDThh:mm:ssZ")
        .format("DD/MM/YYYY");
});



/*
 ** Formatará tudo para UPPERCASE
 */
Vue.filter("upperCase", (val) => {
    if (val) {
        let result = (typeof val != 'string' ? val.toString() : val)
        return result.toUpperCase();
    }
    return val;
});



/*
 ** Formatação para Nomes Próprios
 */
Vue.filter("titleCase", val => {
    if (val) {
        let result = (typeof val != 'string' ? val.toString() : val)
        return result.toLowerCase().split(" ")
            .map(word => word.charAt(0).toUpperCase() + word.slice(1))
            .join(" ");
    }
    return val;
});



/*
 ** Retornará o Icon relativo ao CustomerStatus informado
 */
Vue.filter("iconByCustomerStatus", (status) => {
    if (status) {
        let result = getters.enumCustomerStatus.find(os => os.code === status);
        return result ? result.icon : "mdi-bacteria-outline";
    }
    return "mdi-bacteria-outline";
});


/*
 ** Retornará o Icon relativo ao IntentionStatus informado
 */
Vue.filter("iconByIntentionStatus", (status) => {
    if (status) {
        let result = getters.enumIntentionStatus.find(os => os.code.toLowerCase() === status.toLowerCase());
        return result ? result.icon : "mdi-bacteria-outline";
    }
    return "mdi-bacteria-outline";
});



/*
 ** Retornará o Icon relativo ao CustomerStatus informado
 */
Vue.filter("iconByInsuranceStatus", (status) => {
    if (status) {
        let result = getters.enumInsuranceStatus.find(os => os.code.toLowerCase() === status.toLowerCase());
        return result ? result.icon : "mdi-bacteria-outline";
    }
    return "mdi-bacteria-outline";
});



/*
 ** Retornará o "perfil" conforme o "profile" informado
 */
Vue.filter("nameProfile", (profile) => {
    if (profile) {
        let result = getters.enumProfile.find(p => p.value.toLowerCase() === profile.toLowerCase());
        return result ? result.text : profile;
    }
    return profile;
});




/*
 ** Retornará o Nome relativo ao CustomerStatus informado
 */
Vue.filter("nameCustomerStatus", (status) => {
    if (status) {
        let result = getters.enumCustomerStatus.find(os => os.code.toLowerCase() === status.toLowerCase());
        return result ? result.name : "Sem Descrição";
    }
    return "Sem Descrição";
});




/*
 ** Retornará o Nome relativo ao IntentionStatus informado
 */
Vue.filter("nameIntentionStatus", (status) => {
    if (status) {
        let result = getters.enumIntentionStatus.find(os => os.code.toLowerCase() === status.toLowerCase());
        return result ? result.name : "Sem Descrição";
    }
    return "Sem Descrição";
});



/*
 ** Retornará o Nome relativo ao InsuranceStatus informado
 */
Vue.filter("nameInsuranceStatus", (status) => {
    if (status) {
        let result = getters.enumInsuranceStatus.find(os => os.code.toLowerCase() === status.toLowerCase());
        return result ? result.name : "Sem Descrição";
    }
    return "Sem Descrição";
});



/*
 ** Retornará o Icon relativo ao OrderStatus informado
 */
Vue.filter("iconByOrderStatus", (status) => {
    if (status) {
        let result = getters.enumOrderStatus.find(os => os.code.toLowerCase() === status.toLowerCase());
        return result ? result.icon : "mdi-bacteria-outline";
    }
    return "mdi-bacteria-outline";
});



/*
 ** Retornará a descrição relativo ao OrderStatus informado
 */
Vue.filter("nameOrderStatus", (status) => {
    if (status) {
        let result = getters.enumOrderStatus.find(os => os.code.toLowerCase() === status.toLowerCase());
        return result ? result.name : "Sem descrição";
    }
    return "Sem descrição";
});


/*
 ** Retornará a descrição relativo ao InsuranceStatus informado
 */
Vue.filter("nameInsuranceStatus", (status) => {
    if (status) {
        let result = getters.enumInsuranceStatus.find(s => s.code.toLowerCase() === status.toLowerCase());
        return result ? result.name : "Sem descrição";
    }
    return "Sem descrição";
});




/*
 ** Retornará a descrição relativo ao IntentionStatus informado
 */
Vue.filter("nameIntentionStatus", (status) => {
    if (status) {
        let result = getters.enumIntentionStatus.find(s => s.code.toLowerCase() === status.toLowerCase());
        return result ? result.name : "Sem descrição";
    }
    return "Sem descrição";
});




/*
 **
 ** Formata o documento informado nos Padrões CPF (Física) ou CNPJ (Jurídica)
 **
 */
Vue.filter('formatDocumentBr', (documentId, documentType) => {
    documentId = (typeof documentId != 'string' ? documentId.toString() : documentId);
    if (documentType == 'J') {
        documentId = documentId.padStart(14, '0');
        documentId = documentId.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5');
    } else {
        documentId = documentId.padStart(11, '0');
        documentId = documentId.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
    }
    return documentId;
});


/*
 ** Retornará o endereço completo se o mesmo existir
 */
Vue.filter("formatAddress", (address) => {
    let fullAddress = "Sem endereço de entrega!";
    if (address) {
        fullAddress = "";
        if (address.street) {
            fullAddress = `${address.street}`;
        }
        if (address.number) {
            fullAddress += `, ${address.number}`;
        }
        if (address.complement) {
            fullAddress += `, (${address.complement})`;
        }
        if (address.city) {
            fullAddress += `, ${address.city}`;
        }
        if (address.state) {
            fullAddress += ` - ${address.state}`;
        }
        if (address.cep) {
            let cep = (typeof address.cep != 'string' ? address.cep.toString() : address.cep);
            // Padrão -> 81910-440
            //cep = cep.padStart(9, '0');
            cep = cep.replace(/^(\d{5})(\d{3})/, '$1-$2');
            fullAddress += ` - ${cep}`;
        }
    }
    return fullAddress;
});



/*
 ** Retornará o calculo definido pelo cliente onde o 'Valor Total da Compra (TED ou PMT)' segue a formula abaixo:
 ** (total + delivery + calculatedIof)
 */
Vue.filter("totalPrice", (total, delivery, calculatedIof) => {
    if (total && delivery && calculatedIof) {
        total = parseFloat(total);
        console.log(total);
        delivery = parseFloat(delivery);
        console.log(delivery);
        calculatedIof = parseFloat(calculatedIof);
        console.log(calculatedIof);
        let result = (total + delivery + calculatedIof);
        console.log(result);
        return result ? result : "0.0";
    }
    return "0.0";
});


/*
 ** Retornará a descrição relativo ao Region informado
 */
Vue.filter("regionByName", (region) => {
    if (region && getters.settings && getters.settings.getRegions) {
        let result = asArray(getters.settings.getRegions).find(r => r.key === region);
        return result ? result.value : region;
    }
    return region;
});



/*
 ** Retornará a descrição relativo ao País conforme o Código informado
 */
Vue.filter("displayCountry", (countryCode) => {
    if (countryCode) {
        if (countryCode.length == 2) {
            countryCode = `0${countryCode}`;
        }
        let result = getters.countries.find(c => c.codigo === countryCode);
        return result ? result.nome : countryCode;
    }
    return countryCode;
});



/*
 ** Retornará a descrição relativo ao CurrencyId informado
 */
Vue.filter("currencyById", (currencyId) => {
    let currencies = getters["settings/getCurrencies"];

    if (currencyId && currencies) {
        let result = asArray(currencies).find(r => r._id === currencyId);
        return result ? result.key : currencyId;
    }
    return currencyId;
});