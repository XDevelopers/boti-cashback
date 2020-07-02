<template>
  <div class="content n-content">
    <!-- Área de Mensagens -->
    <alert v-if="msg" message="msg" />
    <!-- end/ Área de Mensagens -->

    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-body app-customers">
            <!-- Listagem de Clientes -->
            <v-app>
              <v-card class="card">
                <v-data-table
                  id="tb-customers"
                  class="transparent tb-customers"
                  :sort-by="table.sortBy"
                  :sort-desc="table.sortDesc"
                  :items-per-page="table.itemsPerPage"
                  :headers="table.headers"
                  :items="table.items"
                  :search="table.search"
                  :footer-props="table.footerProps"
                >
                  <!-- Colunas customizadas -->
                  <template v-slot:item.createdAt="{ item }">{{ item.createdAt | formatDate }}</template>
                  <template v-slot:item.name="{ item }">
                    <link-name :name="item.name" :email="item.email" :text="item.name | titleCase" />
                  </template>
                  <!-- <template v-slot:item.cpf="{ item }">{{ item.cpf, 'F' | formatDocumentBr }}</template> -->
                  <template v-slot:item.cellPhoneNumber="{ item }">
                    <link-whats-app
                      :cellPhoneNumber="item.cellPhoneNumber"
                      :text="item.cellPhoneNumber"
                      :name="item.name"
                    />
                  </template>
                  <template v-slot:item.email="{ item }">
                    <link-name :name="item.name" :email="item.email" :text="item.email" />
                  </template>
                  <template v-slot:item.status="{ item }">
                    <customer-status-tooltip :status="item.status" :enum="enumCustomerStatus" />
                    <span style="display: none;">{{item.status}}</span>
                  </template>

                  <!--
                  <template v-if="detailClient" v-slot:item.action="{ item }">
                    <v-tooltip top color="info">
                      <template v-slot:activator="{ on }">
                        <v-icon v-on="on" color="primary" @click="editItem(item)">edit</v-icon>
                      </template>
                      <span>Editar Cliente</span>
                    </v-tooltip>
                  </template>
                  -->

                  <template v-if="detailClient" v-slot:item.action="{ item }">
                    <v-tooltip top color="info">
                      <template v-slot:activator="{ on }">
                        <nuxt-link :to="`/cliente?id=${item.userId}`">
                          <v-icon v-on="on" color="primary">edit</v-icon>
                        </nuxt-link>
                      </template>
                      <span>Editar Cliente</span>
                    </v-tooltip>
                  </template>

                  <!-- Colunas customizadas -->
                  <template v-slot:top>
                    <v-toolbar flat color="#2b3442" class="transparent">
                      <v-toolbar-title>{{ table.title }}</v-toolbar-title>
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
                      <btn-to-export
                        :file-name="title"
                        :sheet-name="title"
                        html-selector="#tb-customers"
                      />
                      <v-spacer></v-spacer>
                      <v-btn
                        color="primary"
                        dark
                        class="mb-2 d-none"
                        @click="newCustomer"
                      >Novo Cliente</v-btn>
                    </v-toolbar>
                  </template>
                  <template v-slot:no-data>
                    <v-btn color="primary" @click="initialize">Carregar Dados</v-btn>
                  </template>
                </v-data-table>
              </v-card>
            </v-app>
            <!-- end / Listagem de Clientes -->
          </div>
        </div>
      </div>
    </div>

    <loading :showLoading="showLoading" />
  </div>
</template>

<script>
import LinkName from "~/components/controls/LinkName";
import LinkWhatsApp from "~/components/controls/LinkWhatsApp";
import CustomerStatusTooltip from "~/components/customer/CustomerStatusTooltip";

import { mapGetters } from "vuex";

export default {
  meta: {
    title: "Clientes"
  },

  middleware: ["auth", "page-agent"],

  components: {
    LinkName,
    LinkWhatsApp,
    CustomerStatusTooltip
  },

  data() {
    return {
      title: this.$store.state.title,
      msg: "",
      showLoading: false,
      // Dados necessários para a listagem
      dialog: false,
      togglePassword: false,
      toggleRePassword: false,
      searchActive: true,
      on: false,
      table: {
        itemsPerPage: 20,
        sortBy: "createdAt",
        sortDesc: true,
        footerProps: { "items-per-page-options": [10, 20, 30, 40, 50] },
        title: this.$store.state.title,
        search: "",
        headers: [
          { text: "Data do Cadastro", value: "createdAt" },
          { text: "Nome", value: "name" },
          //{ text: "C.P.F.", value: "cpf" },
          { text: "Telefone", value: "cellPhoneNumber" },
          { text: "E-mail", value: "email" },
          { text: "Status Cliente", value: "status", class: ["text-center"] },
          //{ text: "Endereço", value: "address" },
          {
            text: "Ações",
            value: "action",
            sortable: false,
            class: ["text-center"]
          }
        ],
        items: []
      },
      editedIndex: -1,
      // Insert and Update Item
      editedItem: {
        userId: "",
        _id: ""
      },
      defaultItem: {
        userId: "",
        _id: ""
      },
      // Permissões por Action
      detailClient: false
    };
  },

  computed: {
    ...mapGetters({
      enumCustomerStatus: "enumCustomerStatus",
      checkActions: "checkActions"
    })
  },

  mounted() {
    this.initialize();
  },

  methods: {
    initialize() {
      this.showLoading = true;
      this.getAllCustomers();
      this.editedItem = Object.assign({}, this.defaultItem);
      // Verificações de Permissões
      this.checkPermissions();
    },

    newCustomer() {
      this.showLoading = true;
      this.$router.push("/novo-cliente");
    },

    editItem(item) {
      this.showLoading = true;
      this.editedIndex = this.table.items.indexOf(item);
      this.editedItem = Object.assign({}, this.table.items[this.editedIndex]);
      //this.$router.push(`/cliente/${this.editedItem.userId}`);
      this.$router.push({
        name: "cliente-id",
        params: { id: this.editedItem.userId },
        query: { id: this.editedItem.userId }
      });
    },

    async getAllCustomers() {
      this.showLoading = true;
      try {
        const response = await this.$customerServices.get("getAllClients");
        if (response) {
          this.table.items = response;
        }
      } catch (error) {
        console.log("getAllCustomers", error);
      } finally {
        this.showLoading = false;
      }
    },

    checkPermissions() {
      this.detailClient = this.checkActions("detailClient");
    }
  }
};
</script>

<style lang="css">
.app-customers,
.app-customers .v-application--wrap,
.app-customers .theme--dark.v-application {
  background-color: transparent !important;
}
/* Status Cliente -> Centralizada */
.tb-customers table tbody tr td:nth-child(5),
.tb-customers table tbody tr td:nth-child(6) {
  text-align: center;
}
</style>