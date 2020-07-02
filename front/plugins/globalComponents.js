/*
 ** Global Components
 ** See: https://github.com/nuxt/nuxt.js/issues/421
 */
import Vue from "vue";
import { mask } from "vue-the-mask";
import Loading from "~/components/loading";
import Alert from "~/components/alerts";
import SnackBar from "~/components/SnackBar";

Vue.component("loading", Loading);
Vue.component("alert", Alert);
Vue.component("snackbar", SnackBar);
Vue.component("v-mask", mask);