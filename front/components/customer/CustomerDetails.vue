<template>
  <!-- Detalhes do Solicitante -->
  <div class="v-data-table v-data-table--dense">
    <div class="v-data-table__wrapper">
      <table class="tb-customer-details">
        <thead>
          <tr>
            <th colspan="6" class="text-center text-no-wrap">
              <strong class="body-1 text-center">Detalhes do Solicitante</strong>
            </th>
          </tr>
          <tr>
            <th class="text-left text-no-wrap">
              <div class="d-flex align-left justify-left">
                <strong class="body-2">Nome</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex">
                <strong class="body-2">E-mail</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex">
                <strong class="body-2">Celular</strong>
              </div>
            </th>
            <th colspan="3" class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Status do Cliente</strong>
              </div>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td class="text-left">
              <span class="body-1 font-weight-light text-no-wrap">
                <link-name
                  :name="editedItem.name"
                  :email="editedItem.email"
                  :text="editedItem.name | titleCase"
                />
              </span>
            </td>
            <td class="text-left">
              <span class="body-1 font-weight-light text-no-wrap">
                <link-name
                  :name="editedItem.name"
                  :email="editedItem.email"
                  :text="editedItem.email"
                />
              </span>
            </td>
            <td class="text-left">
              <span class="body-1 font-weight-light text-no-wrap">
                <link-whats-app
                  :cellPhoneNumber="editedItem.cellPhoneNumber"
                  :text="editedItem.cellPhoneNumber"
                  :name="editedItem.name"
                />
              </span>
            </td>
            <td colspan="3" class="text-center">
              <customer-status-tooltip :status="editedItem.status" :enum="enumCustomerStatus" />
            </td>
          </tr>
        </tbody>
        <tfoot class="text-center">
          <tr>
            <td
              colspan="6"
              class="caption font-italic font-weight-light grey--text"
              style="height: auto;"
            >
              <div style="padding: 0px 12px 5px;">
                *Para mais Detalhes do Solicitante.
                <nuxt-link :to="{ name: 'cliente', query: { id: editedItem.userId } }">Clique Aqui</nuxt-link>
              </div>
            </td>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
  <!-- end / Detalhes do Solicitante -->
</template>

<script>
import LinkWhatsApp from "~/components/controls/LinkWhatsApp";
import LinkName from "~/components/controls/LinkName";
import CustomerStatusTooltip from "~/components/customer/CustomerStatusTooltip";

export default {
  components: {
    LinkWhatsApp,
    LinkName,
    CustomerStatusTooltip
  },
  props: {
    editedItem: {
      type: [Object],
      default: null
    },
    enumCustomerStatus: {
      type: [Function, Array],
      default: null
    }
  },

  data() {
    return {};
  },
  methods: {}
};
</script>

<style>
.tb-customer-details {
  width: 96% !important;
  margin: auto auto;
}
.tb-customer-details th,
.tb-customer-details td {
  padding: 10px 12px !important;
}

</style>