<template>
  <div class="content n-content">
    <!-- Área de Mensagens -->
    <alert v-if="msg" message="msg" />
    <!-- end/ Área de Mensagens -->

    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-body app-resellers">
            <!-- Listagem de Revendedores -->
            <v-app>
              <v-card class="card">
                <v-data-table
                  id="tb-resellers"
                  class="transparent tb-resellers"
                  :sort-by="table.sortBy"
                  :sort-desc="table.sortDesc"
                  :items-per-page="table.itemsPerPage"
                  :headers="table.headers"
                  :items="table.items"
                  :search="table.search"
                  :footer-props="table.footerProps"
                >
                  <!-- Colunas customizadas -->
                  <template v-slot:item.email="{ item }">
                    <link-name :name="item.name" :email="item.email" :text="item.email" />
                  </template>
                  <template v-slot:item.createdAt="{ item }">{{ item.createdAt | formatDatetime }}</template>
                  <template v-slot:item.cpf="{ item }">{{ item.cpf | formatCpf }}</template>

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
                    <!-- <v-tooltip top color="info">
                      <template v-slot:activator="{ on }">
                        <v-icon
                          v-on="on"
                          color="red darken-1"
                          @click="deleteItem(item)"
                          title="Deletar"
                        >delete</v-icon>
                      </template>
                      <span>Deletar Item</span>
                    </v-tooltip>-->
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
                      <template>
                        <v-btn color="primary" dark class="mx-4" @click="newItem">{{ formTitle }}</v-btn>
                      </template>

                      <!-- Modal Dialog -->
                      <v-dialog v-model="dialog" persistent max-width="800px">
                        <ValidationObserver ref="observer" v-slot="{ invalid }">
                          <v-card>
                            <v-card-title class="pb-0">
                              <span class="headline">{{ formTitle }}</span>
                            </v-card-title>
                            <v-card-text class="pt-0">
                              <v-container class="pt-0">
                                <v-row>
                                  <v-col cols="12" sm="6" md="6">
                                    <VInputWithValidation
                                      v-model="editedItem.name"
                                      label="Nome Completo *"
                                      rules="required|max:255|min:3"
                                      counter="255"
                                    />
                                  </v-col>
                                  <v-col cols="12" sm="6" md="6">
                                    <VInputWithValidation
                                      v-model="editedItem.email"
                                      label="E-mail *"
                                      rules="required|email"
                                      counter="255"
                                    />
                                  </v-col>
                                </v-row>
                                <v-row>
                                  <v-col cols="12" sm="6" md="6">
                                    <VInputWithValidation
                                      v-model="editedItem.cpf"
                                      :disabled="editedIndex !== -1"
                                      v-mask="'###.###.###-##'"
                                      dense
                                      label="CPF *"
                                      rules="required|min:14|max:14|cpf"
                                      counter="14"
                                    />
                                  </v-col>
                                  <v-col v-if="currentUser" cols="12" sm="6" md="6" class="pt-1">
                                    <VSelectWithValidation
                                      v-if="currentUser.role == 'Administrador'"
                                      v-model="editedItem.role"
                                      :items="roles"
                                      label="Role"
                                    />
                                  </v-col>
                                </v-row>
                                <v-row>
                                  <v-col cols="12" sm="12" md="6">
                                    <ValidationProvider
                                      v-slot="{ errors }"
                                      :rules="{ min:3, max:12, required: editedIndex === -1 }"
                                      vid="password"
                                      name="Senha"
                                    >
                                      <v-text-field
                                        v-model="editedItem.password"
                                        :error-messages="errors"
                                        :append-icon="togglePassword ? 'visibility' : 'visibility_off'"
                                        :type="togglePassword ? 'text' : 'password'"
                                        @click:append="togglePassword = !togglePassword"
                                        counter="12"
                                        label="Senha"
                                      />
                                    </ValidationProvider>
                                  </v-col>
                                  <v-col cols="12" sm="12" md="6">
                                    <ValidationProvider
                                      :rules="{ confirmed: 'password', required: editedItem.password.toString().length > 0 || editedIndex === -1 }"
                                      v-slot="{ errors }"
                                      name="Confirmação de Senha"
                                    >
                                      <v-text-field
                                        v-model="editedItem.confirmPassword"
                                        :error-messages="errors"
                                        :append-icon="toggleRePassword ? 'visibility' : 'visibility_off'"
                                        :type="toggleRePassword ? 'text' : 'password'"
                                        @click:append="toggleRePassword = !toggleRePassword"
                                        counter="12"
                                        label="Confirmação de Senha"
                                      />
                                    </ValidationProvider>
                                  </v-col>
                                </v-row>
                              </v-container>
                            </v-card-text>

                            <v-card-actions>
                              <div
                                class="form-action-buttons text-center justify-center align-center"
                              >
                                {{ editedItem.role }}
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
import { ValidationObserver, ValidationProvider, extend } from "vee-validate";
extend("cpf", value => {
  let result = window.checkCPF(value.replace(/\./g, "").replace("-", ""));
  return result ? true : "O {_field_} informado não é válido!";
});

import VSelectWithValidation from "~/components/inputs/VSelectWithValidation";
import VInputWithValidation from "~/components/inputs/VInputWithValidation";
import LinkName from "~/components/controls/LinkName";

export default {
  meta: {
    title: "Revendedores"
  },

  middleware: ["auth", "page-agent"],

  components: {
    ValidationObserver,
    ValidationProvider,
    LinkName,
    VSelectWithValidation,
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
        sortBy: "createdAt",
        sortDesc: true,
        footerProps: { "items-per-page-options": [10, 25, 50] },
        title: this.$store.state.title,
        search: "",
        headers: [
          { text: "Data", value: "createdAt" },
          { text: "Nome", value: "name" },
          { text: "E-mail", value: "email" },
          { text: "CPF", value: "cpf" },
          { text: "Role", value: "role" }
          // {
          //   text: "Ações",
          //   value: "action",
          //   sortable: false,
          //   class: ["text-center"]
          // }
        ],
        items: []
      },
      editedIndex: -1,
      // Insert and Update Item
      editedItem: {
        name: "",
        cpf: "",
        email: "",
        role: "",
        id: "",
        createdAt: "",
        updatedAt: "",
        password: "",
        confirmPassword: ""
      },
      defaultItem: {
        name: "",
        cpf: "",
        email: "",
        role: "",
        id: "",
        createdAt: "",
        updatedAt: "",
        password: "",
        confirmPassword: ""
      },
      // Customizações para os campos especiais
      // Hide / show password
      togglePassword: false,
      toggleRePassword: false,
      error: "",
      roles: [
        {
          text: "Administrador",
          value: "Administrador"
        },
        {
          text: "Revendedor",
          value: "Revendedor"
        }
      ]
    };
  },

  watch: {
    date(val) {
      //console.log("watch", this.formatDate(this.date));
      this.editedItem.date = this.formatDate(this.date);
    }
  },

  computed: {
    ...mapGetters({
      currentUser: "getUser"
    }),
    formTitle() {
      return this.editedIndex === -1 ? "Novo Revendedor" : "Editar Revendedor";
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
      this.getAllResellers();
      this.editedItem = Object.assign({}, this.defaultItem);
    },

    async getAllResellers() {
      this.showLoading = true;
      try {
        const response = await this.$resellerServices.get();
        if (response) {
          //console.log("getAllResellers", response);
          this.table.items = response;
        }
      } catch (error) {
        console.log("getAllResellers", error);
      } finally {
        this.showLoading = false;
      }
    },

    newItem() {
      this.dialog = true;
      this.editedItem = Object.assign({}, this.defaultItem);
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
    },

    editItem(item) {
      this.editedIndex = this.table.items.indexOf(item);
      this.editedItem = Object.assign({}, this.table.items[this.editedIndex]);
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
            // TODO: Criar a Store para isso... !
            this.$store.dispatch("reseller/delete", item);

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
            const result = await this.$resellerServices.put(
              this.editedItem.Id,
              this.editedItem
            );
            console.log("update", result);
            this.$toast.success("Operação realizada com Sucesso!");
            this.close();
          }
        } else {
          if (this.checkRules()) {
            this.editedItem.cpf = this.editedItem.cpf
              .replace(/\./g, "")
              .replace("-", "");

            if (this.currentUser.role != "Administrador") {
              this.editedItem.role = "Revendedor";
            }
            let result = await this.$resellerServices.post(
              null,
              this.editedItem
            );
            console.log("insert", result);
            this.$toast.success("Operação realizada com Sucesso!");
            this.close();
          }
        }
      } catch (error) {
        if (error && error.message) {
          this.$toast.error(error.message);
        }
        console.error("Save/Update", error);
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
.app-resellers,
.app-resellers .v-application--wrap,
.app-resellers .theme--dark.v-application {
  background-color: transparent !important;
}
/* Status Cliente -> Centralizada */
.tb-resellers table tbody tr td:nth-child(6) {
  text-align: center;
}
</style>