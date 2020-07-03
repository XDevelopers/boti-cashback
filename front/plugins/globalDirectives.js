import Vue from "vue";

let moment = {};
let store = {};
let getters = {};
export default function({ $moment, store }) {
    moment = $moment;
    store = store;
    getters = store.getters;
};

/*
 ** Docs: https://vuejs.org/v2/guide/custom-directive.html
 */

Vue.directive("statusName", (el, binding, vnode) => {
    let status = binding.value;
    if (status) {
        let result = getters.enumCustomerStatus.find(os => os.code === status);
        let title = result ? result.name : "";
        if (title) {
            el.setAttribute("title", title);
        }
    }
});

Vue.directive("orderStatusName", (el, binding, vnode) => {
    let status = binding.value;
    console.log(vnode);
    //vnode.child.color = "red";
    if (status) {
        let result = getters.enumOrderStatus.find(os => os.code === status);
        let title = result ? result.name : "";
        if (title) {
            el.setAttribute("title", title);
        }
    }
    return "mdi-bacteria-outline";
});

/*
 ** Adicionar "data:image/png;base64," antes do conteudo Base64!
 ** how to use: v-img-base64="variavel"
 */
Vue.directive("img-base64", (el, binding, vnode) => {
    let base64 = binding.value;
    //console.log(vnode);  
    if (base64) {
        el.setAttribute("src", `data:image/png;base64,${base64}`);
    }
});


// TESTAR mais...
Vue.directive("Currency", (el, binding, vnode) => {
    let currentValue = binding.value;
    console.log(currentValue);
    if (currentValue) {
        currentValue = currentValue.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });
        el.setAttribute("value", `${currentValue}`);
    }
});