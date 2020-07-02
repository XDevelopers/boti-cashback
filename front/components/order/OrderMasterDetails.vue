<template>
  <!-- Detalhes do Item Selecionado -->
  <v-expand-transition>
    <v-card v-show="showMasterDetails" color="blue-grey darken-4 " raised class="card">
      <v-system-bar color="blue-grey darken-3 " dark>
        <v-spacer></v-spacer>
        <v-icon @click="onCancel">mdi-close</v-icon>
      </v-system-bar>
      <div class="pa-2 v-card v-card--outlined v-sheet elevation-0">
        <!-- Detalhes do Solicitante -->
        <customer-details :editedItem="editedItem" :enumCustomerStatus="enumCustomerStatus" />
        <!-- end / Detalhes do Solicitante -->
        
        <!-- Detalhes do Pedido -->
        <order-details
          :editedItem="editedItem"
          :enumOrderStatus="enumOrderStatus"
          :isEditedSiteTax="isEditedSiteTax"
          :datetime-is-valid="datetimeIsValid"
          :onSave="onSave"
          :onCancel="onCancel"
          :onHistory="onHistory"
          :onDeliveryChanged="onDeliveryChanged"
          :onBankTaxNowChanged="onBankTaxNowChanged"
          :onSiteTaxChanged="onSiteTaxChanged"
        />
        <!-- end / Detalhes do Pedido  -->
      </div>
    </v-card>
  </v-expand-transition>
  <!-- end / Detalhes do Item Selecionado -->
</template>

<script>
import CustomerDetails from "~/components/customer/CustomerDetails";
import OrderDetails from "~/components/order/OrderDetails";

export default {
  components: {
    CustomerDetails,
    OrderDetails
  },
  props: {
    showOrderDetails: {
      type: [Boolean],
      default: false
    },
    editedItem: {
      type: [Object],
      required: true,
      default: null
    },
    enumOrderStatus: {
      required: true,
      type: [Function, Array],
      default: null
    },
    enumCustomerStatus: {
      type: [Function, Array],
      default: null
    },
    isEditedSiteTax: {
      type: [Boolean],
      default: false
    },
    datetimeIsValid: {
      type: [Boolean],
      default: false
    },
    onSave: {
      type: [Function]
    },
    onCancel: {
      type: [Function]
    },
    onDeliveryChanged: {
      type: [Function]
    },
    onBankTaxNowChanged: {
      type: [Function]
    },
    onSiteTaxChanged: {
      type: [Function]
    },
    onHistory: {
      type: [Function]
    }
  },
  computed: {
    showMasterDetails() {
      return this.showOrderDetails;
    }
  },
  data() {
    return {
      showDetails: false
    };
  }
};
</script>