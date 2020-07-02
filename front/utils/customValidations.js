let required = (propertyType) => {
    return v => v && v.length > 0 || `O campo ${propertyType} é obrigatório.`;
}

let minLength = (propertyType, minLength) => {
    return v => v && v.length >= minLength || `O campo ${propertyType} deve ter pelo menos ${minLength} caracteres.`;
}

let maxLength = (propertyType, maxLength) => {
    return v => v && v.length <= maxLength || `O Campo ${propertyType} deve ter no máximo ${maxLength} caracteres.`;
}

let greaterThan = (propertyType, minValue, minimum) => {
    return v => v && parseFloat(v) > parseFloat(minValue) || parseFloat(v) > parseFloat(minimum) || `O Campo ${propertyType} deve ser maior que ${minValue}.`;
}

let lessThan = (propertyType, maxValue, maximum) => {
    return v => v && parseFloat(v) <= parseFloat(maxValue) || parseFloat(v) < parseFloat(maximum) || `O Campo ${propertyType} deve ser menor que ${maxValue}.`;
}

let emailFormat = () => {
    let regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,24})+$/
    return v => v && regex.test(v) || "Must be a valid email"
}

export default {
    required,
    minLength,
    maxLength,
    emailFormat,
    greaterThan,
    lessThan
}