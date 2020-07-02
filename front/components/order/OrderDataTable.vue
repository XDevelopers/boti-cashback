<template>
  <!-- Generic DataTable -->
  <v-app>
    <v-card flat>
      <v-data-table
        id="tb-purchases"
        class="transparent tb-purchases"
        :sort-by="table.sortBy"
        :sort-desc="table.sortDesc"
        :items-per-page="table.itemsPerPage"
        :headers="table.headers"
        :items="table.items"
        :search="table.search"
        :footer-props="table.footerProps"
      >
        <!-- Colunas customizadas -->
        <template v-slot:item.createdAt="{ item }">{{ item.createdAt }}</template>
        <template v-slot:item.name="{ item }">
          <link-name :name="item.name" :email="item.email" :text="item.name | titleCase" />
        </template>
        <template v-slot:item.purchasePrice="{ item }">$ {{ item.purchasePrice }}</template>
        <template v-slot:item.siteTax="{ item }">R$ {{ item.siteTax }}</template>
        <template v-slot:item.total="{ item }">R$ {{ item.total }}</template>
        <template v-slot:item.calculatedSpread="{ item }">{{ item.calculatedSpread }}%</template>
        <template v-slot:item.cellPhoneNumber="{ item }">
          <link-whats-app
            :cellPhoneNumber="item.cellPhoneNumber"
            :text="item.cellPhoneNumber"
            :name="item.name"
          />
        </template>
        <template v-slot:item.status="{ item }">
          <customer-status-tooltip
            v-if="enumCustomerStatus"
            :status="item.status"
            :enum="enumCustomerStatus"
          />
          <span style="display: none;">{{item.status}}</span>
        </template>
        <template v-slot:item.orderStatus="{ item }">
          <order-status-tooltip :status="item.orderStatus" :enum="enumOrderStatus" />
          <span style="display: none;">{{item.orderStatus}}</span>
        </template>
        <template v-slot:item.region="{ item }">{{ nameByRegion(item.region) }}</template>
        <!-- Colunas customizadas | Pedidos de Cotação -->
        <template
          v-slot:item.originCountryCode="{ item }"
        >{{ item.originCountryCode | displayCountry }}</template>
        <template
          v-slot:item.destinationCountryCode="{ item }"
        >{{ item.destinationCountryCode | displayCountry }}</template>
        <template v-slot:item.travelStart="{ item }">{{ item.travelStart | formatDatetime }}</template>
        <template v-slot:item.travelEnd="{ item }">{{ item.travelEnd | formatDatetime }}</template>
        <template v-slot:item.email="{ item }">
          <link-name :name="item.name" :email="item.email" :text="item.email" />
        </template>

        <template v-if="detailPurchase" v-slot:item.action="{ item }">
          <v-tooltip top color="info">
            <template v-slot:activator="{ on }">
              <v-icon
                v-on="on"
                @click="viewItem(item.purchaseId); scrollToTop();"
                color="primary"
                class="mdi-folder-account-outline"
              >mdi-folder-account-outline</v-icon>&nbsp;
            </template>
            <span>Visualizar Mais Detalhes</span>
          </v-tooltip>
        </template>
        <!-- Colunas customizadas -->
        <template v-slot:top>
          <v-toolbar flat color="#2b3442" class="transparent">
            <v-toolbar-title>{{table.title}}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-divider class="mx-4" inset vertical></v-divider>

            <v-text-field
              class="mx-4"
              v-on="on"
              v-model="table.search"
              append-icon="fas fa-search"
              label="Pesquisar"
              single-line
              hide-details
            ></v-text-field>
            <!-- DatePicker -->
            <v-date-pickers :search-by-date="searchByDate" />
            <!-- end / DatePicker -->
            <btn-to-export
              :file-name="table.title"
              :sheet-name="table.title"
              html-selector="#tb-purchases"
            />
          </v-toolbar>
        </template>
        <template v-slot:no-data>
          <!-- <v-btn color="primary" @click="initialize">Carregar Dados</v-btn> -->
          Nenhum Registro encontrado!
        </template>
      </v-data-table>
    </v-card>
  </v-app>
  <!-- end / Generic DataTable -->
</template>

<script>
import { mapGetters } from "vuex";
import LinkName from "~/components/controls/LinkName";
import LinkWhatsApp from "~/components/controls/LinkWhatsApp";
import CustomerStatusTooltip from "~/components/customer/CustomerStatusTooltip";
import OrderStatusTooltip from "~/components/order/OrderStatusTooltip";
import VDatePickers from "~/components/inputs/VDatePickers";

export default {
  components: {
    LinkName,
    LinkWhatsApp,
    CustomerStatusTooltip,
    OrderStatusTooltip,
    VDatePickers
  },

  props: {
    table: {
      type: [Object],
      required: true,
      default: null
    },
    viewItem: {
      type: [Function]
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
    enumInsuranceStatus: {
      type: [Function, Array],
      default: null
    },
    searchByDate: {
      type: [Function],
      default: () => {}
    }
  },

  computed: {
    ...mapGetters({
      regions: "settings/getRegions",
      checkActions: "checkActions"
    })
  },

  data() {
    return {
      on: false,
      // Permissões por Action
      detailPurchase: false
    };
  },

  mounted() {
    this.initialize();
  },

  methods: {
    initialize() {
      // Verificações de Permissões
      this.detailPurchase = this.checkActions("detailPurchase");
    },

    nameByRegion(region) {
      if (region && this.regions) {
        let result = asArray(this.regions).find(r => r.key === region);
        return result ? result.value : region;
      }
      return region;
    },

    scrollToTop() {
      window.scrollTo(0, 54);
    }
  }
};
</script>

<style lang="css">
.app-purchases #app {
  background-color: transparent !important;
}
.tb-purchases table thead tr th {
  white-space: nowrap !important;
}

/* createAt */
.tb-purchases table tbody tr td:nth-child(1) {
  white-space: nowrap !important;
}
/* Status, StatusPedidos, Ações */
.tb-purchases table tbody tr td:nth-child(9),
.tb-purchases table tbody tr td:nth-child(10),
.tb-purchases table tbody tr td:nth-child(11) {
  text-align: center;
}
</style>