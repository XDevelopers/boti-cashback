/* 
 * To See more details try:
 * https://github.com/jecovier/vue-json-excel
 * 
 * https://medium.com/@codebeast_/why-your-third-party-plugin-dont-work-in-nuxt-and-how-to-fix-it-d1a8caadf422
 *
 */
import Vue from "vue";
import JsonExcel from "vue-json-excel";

const VueJsonToExcel = {
    install(Vue, options) {
        Vue.component('downloadExcel', JsonExcel)
    }
};

Vue.use(VueJsonToExcel);

export default VueJsonToExcel;