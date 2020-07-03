/*
 **
 ** Nuxt Combined Inject ->
 ** See: https://nuxtjs.org/guide/plugins#combined-inject
 **
 */
import createServices from "~/services/api-services-inject";

export default (ctx, inject) => {
    // inject the service in the context (ctx.app.$service)
    // And in the Vue instances (this.$service in your components)
    const serviceWithAxios = createServices(ctx.$axios);

    inject("orderServices", serviceWithAxios("/api/v1/order"));
    inject("resellerServices", serviceWithAxios("/api/v1/reseller"));
    inject("cashbackServices", serviceWithAxios("/api/v1/cashback"));
}