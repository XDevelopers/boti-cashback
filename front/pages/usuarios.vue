<template>
  <div class="row">
    <div class="col-md-12">
      <div class="card">
        <div class="card-header d-none">
          <h5 class="card-title">{{ title }}</h5>
        </div>
        <v-app>
          <v-card class="card">
            <v-data-table
              id="tb-users"
              class="card"
              sort-by="firstName"
              :headers="table.headers"
              :items="table.items"
              :search="table.search"
            >
              <!-- Colunas customizadas -->
              <template v-slot:item.roles="{ item }">{{ item.roleName }}</template>
              <template v-slot:item.regionId="{ item }">{{ nameByRegion(item.regionId) }}</template>
              <template v-slot:item.active="{ item }">
                <v-simple-checkbox v-model="item.active" disabled></v-simple-checkbox>
                <span style="display: none;">{{ item.active }}</span>
              </template>

              <template v-if="updateUser" v-slot:item.action="{ item }">
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
              <!-- Colunas customizadas -->

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
                  <btn-to-export :file-name="title" :sheet-name="title" html-selector="#tb-users" />
                  <v-spacer></v-spacer>
                  <!-- Modal Dialog -->
                  <v-dialog v-model="dialog" persistent max-width="800px">
                    <template v-slot:activator="{ on }">
                      <v-btn
                        v-if="createUser"
                        color="primary"
                        dark
                        class="mx-4"
                        v-on="on"
                        @click="newItem"
                      >{{ formTitle }}</v-btn>
                    </template>
                    <ValidationObserver ref="observer" v-slot="{ invalid }">
                      <v-card>
                        <v-card-title>
                          <span class="headline">{{ formTitle }}</span>
                        </v-card-title>
                        <v-card-text>
                          <v-container>
                            <v-row>
                              <v-col cols="12" sm="12" md="5">
                                <VInputWithValidation
                                  v-model="editedItem.name"
                                  label="Nome *"
                                  rules="required|max:200|min:3"
                                  counter="200"
                                />
                              </v-col>
                              <v-col cols="12" sm="6" md="3">
                                <VInputWithValidation
                                  v-model="editedItem.cpf"
                                  v-mask="['###########']"
                                  :disabled="editedIndex !== -1"
                                  :rules="{ 
                                    required: true, 
                                    max: 14, 
                                    min: 3, 
                                    unique: editedIndex === -1 ? 
                                      table.items.map(u => u.cpf) : 
                                      table.items.filter(u => u.cpf !== table.items[editedIndex].cpf).map(u => u.cpf) 
                                  }"
                                  label="CPF *"
                                  counter="14"
                                />
                              </v-col>

                              <v-col cols="12" sm="12" md="4">
                                <VInputWithValidation
                                  v-model="editedItem.email"
                                  label="Email *"
                                  rules="required|email|max:255|min:3"
                                  counter="255"
                                />
                              </v-col>
                            </v-row>
                            <v-row>
                              <v-col cols="12" sm="12" md="5" class="pt-6">
                                <VSelectWithValidation
                                  v-model="editedItem.regionId"
                                  :items="getRegions"
                                  item-text="value"
                                  item-value="key"
                                  label="Região"
                                />
                              </v-col>

                              <v-col cols="12" sm="12" md="5" class="pt-6">
                                <VSelectWithValidation
                                  v-model="editedItem.roleId"
                                  :items="roles"
                                  item-text="name"
                                  item-value="_id"
                                  rules="required"
                                  label="Perfil *"
                                />
                              </v-col>

                              <v-col cols="12" sm="6" md="2">
                                <v-checkbox v-model="editedItem.active" label="Ativo"></v-checkbox>
                              </v-col>
                            </v-row>
                          </v-container>
                        </v-card-text>

                        <v-card-actions>
                          <div class="form-action-buttons text-center justify-center align-center">
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
      </div>
    </div>
    <loading :showLoading="showLoading" />
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import asArray from "~/utils/as-array";
import { ValidationObserver } from "vee-validate";
import VInputWithValidation from "~/components/inputs/VInputWithValidation";
import VSelectWithValidation from "~/components/inputs/VSelectWithValidation";
import _ from "lodash";

export default {
  meta: {
    title: "Lista de Usuários do Sistema"
  },

  middleware: ["auth", "page-agent"],

  components: {
    ValidationObserver,
    VInputWithValidation,
    VSelectWithValidation
  },

  // To undestand "why fetch()"
  // See https://nuxtjs.org/api/pages-fetch#the-fetch-method
  async fetch({ store }) {
    // call a Service to request All Users from API and will set $store.users.list
    // /store/users/actions/getAllUsers()
    await store.dispatch("settings/getRegions");
    await store.dispatch("adminUsers/getAllRoles");
  },

  computed: {
    ...mapGetters({
      roles: "adminUsers/getAllRoles",
      getRegions: "settings/getRegions",
      checkActions: "checkActions"
    }),
    formTitle() {
      return this.editedIndex === -1 ? "Novo usuário" : "Editar usuário";
    },
    formButton() {
      return this.editedIndex === -1 ? "Salvar" : "Atualizar";
    }
  },

  // watch: {
  //   // dialog(val) {
  //   //   val || this.close();
  //   // }
  // },

  data() {
    return {
      title: this.$store.state.title,
      dialog: false,
      togglePassword: false,
      toggleRePassword: false,
      searchActive: true,
      on: false,
      // Table
      table: {
        title: this.$store.state.title,
        search: "",
        headers: [
          { text: "Nome", value: "name" },
          { text: "E-mail", value: "email" },
          { text: "CPF", value: "cpf" },
          { text: "Perfil", value: "roleName" },
          { text: "Região", value: "regionId" },
          { text: "Status", value: "active" },
          { text: "Ações", value: "action", sortable: false }
        ],
        items: []
      },
      hidePassword: true,
      hideConfirmPassword: true,
      error: "",
      editedIndex: -1,
      editedItem: {
        cpf: "",
        email: "",
        roleId: "",
        roleName: "",
        name: "",
        active: true,
        _id: "",
        regionId: ""
      },
      defaultItem: {
        name: "",
        cpf: "",
        email: "",
        active: true,
        roleId: "",
        roleName: "",
        _id: "",
        regionId: ""
      },
      showLoading: false,
      // Permissões por Action
      createUser: false,
      updateUser: false,
      listUser: false
    };
  },

  mounted() {
    this.initialize();
  },

  methods: {
    async initialize() {
      // Verificações de Permissões
      this.createUser = this.checkActions("createUser");
      this.updateUser = this.checkActions("updateUser");
      this.listUser = this.checkActions("listUser");

      this.showLoading = true;
      this.editedItem = Object.assign({}, this.defaultItem);
      await this.getAllUsers();
    },

    async getAllUsers() {
      try {
        console.log("getAllUsers");
        const response = await this.$adminUserServices.get("getAll");
        if (response) {
          this.table.items = response;
        }
      } catch (error) {
        console.error("getAllUsers", error);
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
      }, 200);
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
            this.$store.dispatch("adminUsers/delete", item);
            this.editedIndex = this.table.items.indexOf(item);
            // call Delete Process on endpoint!
            this.table.items.splice(this.editedIndex, 1);
            this.$swal.fire(
              "Deletado!",
              "Registro removido com sucesso",
              "success"
            );
          }
        });
    },

    async close() {
      this.dialog = false;
      //setTimeout(() => {
      this.editedItem = Object.assign({}, this.defaultItem);
      this.editedIndex = -1;
      //}, 200);
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
            const result = await this.$adminUserServices.update(
              "update",
              this.editedItem
            );
            if (result && result.success) {
              this.$toast.success("Operação realizada com Sucesso!");
              this.close();
            } else {
              this.$toast.error(result.message);
            }
          }
        } else {
          if (this.checkRules()) {
            this.editedItem.active = true;
            let result = await this.$adminUserServices.insert(
              "create",
              this.editedItem
            );
            if (result && result.success) {
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
      if (!this.$core.checkCPF(this.editedItem.cpf)) {
        this.$toast.error("O CPF informado é inválido.");
        this.showLoading = false;
        return false;
      }

      return true;
    },

    nameByRegion(region) {
      if (region && this.getRegions && this.getRegions.length > 0) {
        let result = asArray(this.getRegions).find(r => r.key === region);
        return result ? result.value : region;
      }
      return region;
    }
  }
};
</script>

<style scoped>
.card {
  background-color: #2b3442 !important;
  color: #f1f5f7 !important;
}
.v-card__title {
  color: #f1f5f7;
}
.theme--light.v-data-table thead tr th {
  color: #9ea4ba;
}
</style>