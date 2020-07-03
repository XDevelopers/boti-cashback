// VeeValidate
import * as VeeValidate from "vee-validate";
import { extend, configure, localize } from "vee-validate";
import pt_BR from "vee-validate/dist/locale/pt_BR.json";
import * as rules from 'vee-validate/dist/rules';
import { messages } from "vee-validate/dist/locale/pt_BR.json";

// ------------------------------------------------
// VeeValidate -> Custom "unique:"
// ------------------------------------------------
const isUnique = (value, list) => {
    return !list.includes(value);
};
const paramNames = ["list"];
VeeValidate.extend(
    "unique", {
        message: (field) => `Este ${field} já existe!`,
        validate: isUnique
    }, {
        paramNames,
    }
);
// ------------------------------------------------


// ------------------------------------------------
// Loop over all rules
// ------------------------------------------------
/*
Object.keys(rules).forEach((rule) => {
    VeeValidate.extend(rule, {
        ...rules[rule], // copies rule configuration
        message: messages[rule], // assign message
    });
});
*/
for (let rule in rules) {
    // add the rule
    extend(rule, rules[rule]);
}
// ------------------------------------------------


// ------------------------------------------------
// Install Portuguese BR and English.
// localize({
//   pt_BR,
//   en
// });

// Install and Activate the Brasil locale.
localize('pt_BR', pt_BR);

export default function VeeValidatePlugin({ app }) {
    configure({
        inject: true,
        fieldsBagName: 'veeFields',
        errorBagName: 'veeErrors'
    });
}