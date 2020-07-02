import Vue from "vue";
import Vuetify from "vuetify";
import colors from "vuetify/es5/util/colors";
import "vuetify/dist/vuetify.min.css";
import "@mdi/font/css/materialdesignicons.css"; // Ensure you are using css-loader version "^2.1.1" ,
// Translation provided by Vuetify (javascript)
import pt from 'vuetify/es5/locale/pt'
Vue.use(Vuetify)

export default ctx => {
    const vuetify = new Vuetify({
        theme: {
            dark: true, // From 2.0 You have to select the theme dark or light here
            themes: {
                dark: {
                    primary: colors.green.darken3,
                    accent: colors.green.darken3,
                    secondary: colors.amber.darken3,
                    info: colors.teal.lighten1,
                    warning: colors.amber.base,
                    error: colors.deepOrange.accent4,
                    success: colors.green.darken1
                },
                light: {
                    primary: colors.blue.darken1,
                    accent: "#E7E6DC" || colors.indigo.base,
                    secondary: colors.red.lighten4,
                    info: colors.teal.lighten1,
                    warning: colors.amber.base,
                    error: colors.deepOrange.accent4,
                    success: colors.green.accent3
                }
            }
        },
        lang: {
            locales: { pt },
            current: 'pt',
        },
    })

    ctx.app.vuetify = vuetify
    ctx.$vuetify = vuetify.framework
}