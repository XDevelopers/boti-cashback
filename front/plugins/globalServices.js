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

    inject("actionServices", serviceWithAxios("/admin/action"));
    inject("addressServices", serviceWithAxios("/admin/address"));
    inject("adminUserServices", serviceWithAxios("/admin/userAdmin"));
    inject("bankServices", serviceWithAxios("/admin/bank"));
    inject("customerServices", serviceWithAxios("/admin/clients"));
    inject("currencyServices", serviceWithAxios("/admin/currency"));
    inject("dashboardServices", serviceWithAxios("/admin/dashboard"));
    inject("documentsServices", serviceWithAxios("/admin/documents"));
    inject("insuranceServices", serviceWithAxios("/admin/insurance"));
    inject("intentionServices", serviceWithAxios("/admin/intention"));
    inject("purchaseServices", serviceWithAxios("/admin/purchase"));
    inject("regionServices", serviceWithAxios("/admin/region"));
    inject("roleServices", serviceWithAxios("/admin/Role"));
}