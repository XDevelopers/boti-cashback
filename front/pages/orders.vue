<template>
  <div class="content n-content">
    <!-- Área de Mensagens -->
    <alert v-if="msg" message="msg" />
    <!-- end/ Área de Mensagens -->

    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-body app-orders">
            <!-- Listagem de Compras -->
            <v-app>
              <v-card class="card">
                <v-data-table
                  id="tb-orders"
                  class="transparent tb-orders"
                  :sort-by="table.sortBy"
                  :sort-desc="table.sortDesc"
                  :items-per-page="table.itemsPerPage"
                  :headers="table.headers"
                  :items="table.items"
                  :search="table.search"
                  :footer-props="table.footerProps"
                >
                  <!-- Colunas customizadas -->
                  <template v-slot:item.cpf="{ item }">{{ item.cpf | formatCpf }}</template>
                  <template v-slot:item.date="{ item }">{{ item.date | formatDate }}</template>
                  <template v-slot:item.value="{ item }">{{ item.value | formatPrice }}</template>
                  <template
                    v-slot:item.valueCashback="{ item }"
                  >{{ item.valueCashback | formatPrice }}</template>
                  <template
                    v-slot:item.appliedCashback="{ item }"
                  >{{ item.appliedCashback | formatPercent }}</template>
                  <template v-slot:item.orderStatus="{ item }">
                    <order-status-tooltip :status="item.orderStatus" :enum="enumOrderStatus" />
                    <span style="display: none;">{{item.orderStatus}}</span>
                  </template>

                  <!-- Ações -->
                  <template v-slot:item.action="{ item }">
                    <v-tooltip top color="info">
                      <template v-slot:activator="{ on }">
                        <v-icon
                          v-on="on"
                          color="green darken-3"
                          @click="editItem(item)"
                          title="Editar"
                        >edit</v-icon>
                      </template>
                      <span>Editar Item</span>
                    </v-tooltip>
                    <v-tooltip top color="info">
                      <template v-slot:activator="{ on }">
                        <v-icon
                          v-on="on"
                          color="red darken-1"
                          @click="deleteItem(item)"
                          title="Deletar"
                        >delete</v-icon>
                      </template>
                      <span>Deletar Item</span>
                    </v-tooltip>
                  </template>
                  <!-- Ações -->

                  <!-- / Colunas customizadas -->
                  <template v-slot:top>
                    <v-toolbar flat color="#2b3442">
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
                      <v-spacer></v-spacer>
                      <v-btn color="primary" dark class="mx-4" @click="newItem">{{ formTitle }}</v-btn>

                      <!-- Modal Dialog -->
                      <v-dialog v-model="dialog" persistent max-width="600px">
                        <ValidationObserver ref="observer" v-slot="{ invalid }">
                          <v-card>
                            <v-card-title>
                              <span class="headline">{{ formTitle }}</span>
                            </v-card-title>
                            <v-card-text>
                              <v-container>
                                <v-row>
                                  <v-col cols="12" sm="6" md="6">
                                    <VInputWithValidation
                                      v-model="editedItem.code"
                                      :disabled="editedIndex !== -1"
                                      label="Code *"
                                      rules="required|max:10"
                                      counter="10"
                                    />
                                  </v-col>
                                  <v-col cols="12" sm="6" md="6">
                                    <VInputWithValidation
                                      v-model="editedItem.value"
                                      v-mask="['#.##','##.##','###.##','####.##','#####.##']"
                                      prefix="R$"
                                      label="Valor *"
                                      rules="required|max:10"
                                      counter="10"
                                    />
                                  </v-col>
                                  <v-col cols="12" sm="6" md="6">
                                    <VInputWithValidation
                                      v-model="editedItem.cpf"
                                      v-mask="'###.###.###-##'"
                                      :disabled="editedIndex !== -1"
                                      dense
                                      label="CPF *"
                                      rules="required|min:14|max:14|cpf"
                                      counter="14"
                                    />
                                  </v-col>
                                  <v-col cols="12" sm="6" md="6">
                                    <v-menu
                                      v-model="menuDate"
                                      :close-on-content-click="false"
                                      transition="scale-transition"
                                      offset-y
                                      max-width="290px"
                                      min-width="290px"
                                    >
                                      <template v-slot:activator="{ on }">
                                        <VInputWithValidation
                                          v-model="editedItem.date"
                                          v-mask="'##/##/####'"
                                          label="Data *"
                                          rules="required"
                                          prepend-icon="event"
                                          dense
                                          v-on="on"
                                          @blur="date = parseDate(editedItem.date)"
                                        />
                                      </template>
                                      <v-date-picker
                                        v-model="date"
                                        locale="pt-BR"
                                        scrollable
                                        no-title
                                        @input="menuDate = false"
                                      />
                                    </v-menu>
                                  </v-col>
                                </v-row>
                              </v-container>
                            </v-card-text>

                            <v-card-actions>
                              <div
                                class="form-action-buttons text-center justify-center align-center"
                              >
                                <v-btn v-if="!showLoading" color="primary" @click="close">Cancelar</v-btn>
                                <v-btn
                                  v-if="!showLoading"
                                  color="primary"
                                  @click="save"
                                  :disabled="invalid"
                                  class="ml-4"
                                >{{ formButton }}</v-btn>
                                <v-btn v-else :loading="showLoading" color="green darken-1">Salvando</v-btn>
                              </div>
                            </v-card-actions>
                          </v-card>
                        </ValidationObserver>
                      </v-dialog>
                      <!-- Modal Dialog -->
                    </v-toolbar>
                  </template>

                  <template v-slot:no-data>
                    <v-btn color="primary" @click="initialize">Reset</v-btn>
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
import { mapGetters } from "vuex";
import { ValidationObserver, extend } from "vee-validate";
extend("cpf", value => {
  let result = window.checkCPF(value.replace(/\./g, "").replace("-", ""));
  return result ? true : "O {_field_} informado não é válido!";
});

import OrderStatusTooltip from "~/components/order/OrderStatusTooltip";
import VInputWithValidation from "~/components/inputs/VInputWithValidation";

export default {
  meta: {
    title: "Compras"
  },

  middleware: ["auth", "page-agent"],

  components: {
    ValidationObserver,
    OrderStatusTooltip,
    VInputWithValidation
  },

  data() {
    return {
      title: this.$store.state.title,
      msg: "",
      showLoading: false,
      // Dados necessários para a listagem
      dialog: false,
      searchActive: true,
      on: false,
      table: {
        itemsPerPage: 25,
        sortBy: "date",
        sortDesc: true,
        footerProps: { "items-per-page-options": [10, 25, 50] },
        title: this.$store.state.title,
        search: "",
        headers: [
          { text: "Cód.", value: "code" },
          { text: "Valor R$", value: "value" },
          { text: "Data", value: "date" },
          { text: "CPF", value: "cpf" },
          { text: "% Cashback", value: "appliedCashback" },
          { text: "Valor Cashback", value: "valueCashback" },
          { text: "Status", value: "orderStatus", class: ["text-center"] },
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
        code: "",
        cpf: "",
        value: "",
        date: "",
        appliedCashback: "",
        valueCashback: "",
        orderStatus: "OnChecking",
        id: "",
        createdAt: "",
        updatedAt: ""
      },
      defaultItem: {
        code: "",
        cpf: "",
        value: "",
        date: "", //2020-06-21T00:00:00-03:00
        appliedCashback: "",
        valueCashback: "",
        orderStatus: "OnChecking",
        id: "",
        createdAt: "",
        updatedAt: ""
      },
      // Date Picker
      menuDate: false,
      date: ""
    };
  },

  watch: {
    date(val) {
      console.log("watch", this.formatDate(this.date));
      this.editedItem.date = this.formatDate(this.date);
    }
  },

  computed: {
    ...mapGetters({
      enumOrderStatus: "enumOrderStatus",
      currentUser: "getUser"
    }),
    formTitle() {
      return this.editedIndex === -1 ? "Nova Compra" : "Editar Compra";
    },
    formButton() {
      return this.editedIndex === -1 ? "Salvar" : "Atualizar";
    }
  },

  mounted() {
    this.initialize();
  },

  methods: {
    initialize() {
      // Add function to Global Scope
      window.checkCPF = this.$core.checkCPF;

      this.showLoading = true;
      this.getAllOrders();
      this.editedItem = Object.assign({}, this.defaultItem);
    },

    async getAllOrders() {
      this.showLoading = true;
      try {
        const response = await this.$orderServices.get();
        if (response) {
          //console.log("getAllOrders", response);
          this.table.items = response;
        }
      } catch (error) {
        console.log("getAllOrders", error);
      } finally {
        this.showLoading = false;
      }
    },

    newItem() {
      this.dialog = true;
      this.editedItem = Object.assign({}, this.defaultItem);
      setTimeout(() => {
        //console.log(this.currentUser);
        this.editedIndex = -1;
        // if (this.currentUser && this.currentUser.cpf) {
        //   this.editedItem.cpf = this.currentUser.cpf;
        // }
      }, 200);
    },

    editItem(item) {
      this.editedIndex = this.table.items.indexOf(item);
      this.editedItem = Object.assign({}, this.table.items[this.editedIndex]);
      this.editedItem.value = this.editedItem.value.toFixed(2);
      this.editedItem.date = this.formatDate(
        this.$core.parseISODate(this.editedItem.date)
      );
      this.dialog = true;
    },

    deleteItem(item) {
      this.$swal
        .fire({
          title: "Você tem certeza disso?",
          text: "Você não conseguirá reverter essa ação",
          type: "question",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          cancelButtonText: "Cancelar",
          confirmButtonText: "Sim, deletar!"
        })
        .then(result => {
          if (result.value) {
            this.editedIndex = this.table.items.indexOf(item);
            // call Delete Process on endpoint!
            this.$store.dispatch("order/delete", item);

            // Remove from grid
            this.table.items.splice(this.editedIndex, 1);
            this.$swal.fire(
              "Deletado!",
              "Registro removido com sucesso",
              "success"
            );
            this.initialize();
          }
        });
    },

    async close() {
      this.dialog = false;
      this.editedItem = Object.assign({}, this.defaultItem);
      this.editedIndex = -1;

      // clear errors from validations
      if (this.$refs.observer) {
        this.$refs.observer.reset();
      }
      await this.initialize();
    },

    async save() {
      this.showLoading = true;
      try {
        if (this.editedIndex > -1) {
          // Update
          //Object.assign(this.table.items[this.editedIndex], this.editedItem);
          if (this.checkRules()) {
            this.showLoading = true;
            this.editedItem.cpf = this.editedItem.cpf
              .replace(/\./g, "")
              .replace("-", "");
            const result = await this.$orderServices.put(
              this.editedItem.Id,
              this.editedItem
            );
            console.log("update", result);
            if (result) {
              this.$toast.success("Operação realizada com Sucesso!");
              this.close();
            } else {
              this.$toast.error(result.message);
            }
          }
        } else {
          // if (this.currentUser && this.currentUser.cpf) {
          //   this.editedItem.cpf = this.currentUser.cpf;
          // }

          // TODO: Foi comentado a linha acima, pois a documentação está meio confusa, verificar!

          // Insert
          if (this.checkRules()) {
            this.editedItem.cpf = this.editedItem.cpf
              .replace(/\./g, "")
              .replace("-", "");
            let result = await this.$orderServices.post(this.editedItem);
            console.log("insert", result);
            if (result) {
              this.$toast.success("Operação realizada com Sucesso!");
              this.close();
            } else {
              this.$toast.error(result.message);
            }
          }
        }
      } catch (error) {
        if (error && error.message) {
          this.$toast.error(error.message);
        }
        console.error("save Update", error);
      } finally {
        this.showLoading = false;
      }
    },

    checkRules() {
      // Validar CPF
      // Algumas regras:
      if (
        !this.$core.checkCPF(
          this.editedItem.cpf.replace(/\./g, "").replace("-", "")
        )
      ) {
        this.$toast.error("O CPF informado é inválido.");
        this.showLoading = false;
        return false;
      }

      return true;
    },

    formatDate(date) {
      // Deveria retornar DD/MM/YYYY
      //console.log("formatDate", date);
      try {
        return this.$core.formatDate(date);
      } catch (error) {
        console.log("formatDate Error:", error);
      }
    },

    parseDate(date) {
      // Deveria Retornar YYYY-MM-DD
      //console.log("parseDate", date);
      if (!date) return null;
      if (date.length < 10) return null;
      try {
        if (date.indexOf("/") === -1) {
          return this.$moment(date)
            .utc()
            .format("YYYY-MM-DD");
        } else {
          return this.$core.parseDate(date);
        }
      } catch (error) {
        console.log("parseDate Error:", error);
        return null;
      }
    }
  }
};
</script>

<style lang="css">
.app-orders,
.app-orders .v-application--wrap,
.app-orders .theme--dark.v-application {
  background-color: transparent !important;
}
/* Status Cliente -> Centralizada */
.tb-orders table tbody tr td:nth-child(7),
.tb-orders table tbody tr td:nth-child(8) {
  text-align: center;
}
</style>