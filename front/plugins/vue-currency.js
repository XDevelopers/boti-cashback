import Vue from "vue";
import VueCurrencyFilter from "vue-currency-filter";

Vue.use(VueCurrencyFilter, {
  globalOptions: {
    symbol: 'R$',
    currency: 'BRL',
    precision: 4,
    allowNegative: false,
    thousandsSeparator: '.',
    fractionCount: 4,
    fractionSeparator: ',',
    symbolPosition: 'front',
    symbolSpacing: true
  },
  componentName:'Currency'
});
