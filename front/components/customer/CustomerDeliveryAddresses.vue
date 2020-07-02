<template>
  <v-card class="mb-3" style="box-shadow: unset;">
    <!-- Dados de Entrega -->
    <v-data-table
      class="transparent tb-delivery-address"
      :sort-by="delivery.sortBy"
      :sort-desc="delivery.sortDesc"
      :items-per-page="delivery.itemsPerPage"
      :headers="delivery.headers"
      :items="editedItem.deliveryAddress"
      :search="delivery.search"
      :footer-props="delivery.footerProps"
    >
      <!-- Colunas customizadas -->
      <template v-slot:item.action="{ item }">
        <v-tooltip top color="info">
          <template v-slot:activator="{ on }">
            <v-icon v-on="on" color="green darken-3" @click="editItem(item)" class="edit mr-2">edit</v-icon>
          </template>
          <span>Editar Item</span>
        </v-tooltip>
        <v-tooltip top color="info">
          <template v-slot:activator="{ on }">
            <v-icon v-on="on" color="red darken-1" @click="deleteItem(item)" class>delete</v-icon>
          </template>
          <span>Deletar Item</span>
        </v-tooltip>
      </template>
      <!-- end / Colunas customizadas -->
      <template v-slot:top>
        <v-toolbar flat color="#2b3442" class="transparent">
          <v-toolbar-title>{{ delivery.title }}</v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <!-- Search -->
          <v-text-field
            class="mx-4"
            v-on="on"
            v-model="delivery.search"
            append-icon="fas fa-search"
            label="Pesquisar"
            single-line
            hide-details
          ></v-text-field>
          <v-spacer></v-spacer>

          <!-- Modal -->
          <v-dialog v-model="showDelivery" max-width="800px">
            <template v-slot:activator="{ on }">
              <v-btn
                color="primary"
                dark
                class="mb-2"
                v-on="on"
                @click="newDelivery"
              >{{ formDelivery }}</v-btn>
            </template>

            <ValidationObserver ref="deliveryObserver" v-slot="{ invalid }">
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formDelivery }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row justify="space-between">
                      <v-col cols="12" md="3">
                        <VInputWithValidation
                          v-model="deliveryAddress.cep"
                          v-mask="'#####-###'"
                          :loading="showLoading"
                          dense
                          counter
                          label="CEP *"
                          rules="required|max:9"
                          @change="getAddressByCEP(deliveryAddress)"
                        />
                      </v-col>
                      <v-col cols="12" md="6">
                        <VInputWithValidation
                          v-model="deliveryAddress.street"
                          :loading="showLoading"
                          dense
                          label="Logradouro (R. Av. Trav.) *"
                          rules="required|max:255"
                          counter="255"
                        />
                      </v-col>
                      <v-col cols="12" md="3">
                        <VInputWithValidation
                          v-model="deliveryAddress.number"
                          dense
                          label="Número *"
                          rules="required|max:10"
                          counter="10"
                        />
                      </v-col>
                    </v-row>
                    <v-row justify="space-between">
                      <v-col cols="12" md="4">
                        <VInputWithValidation
                          v-model="deliveryAddress.complement"
                          dense
                          label="Complemento"
                          rules="max:255"
                          counter="255"
                        />
                      </v-col>
                      <v-col cols="12" md="3">
                        <VSelectWithValidation
                          v-model="deliveryAddress.state"
                          :items="states"
                          :loading="showLoading"
                          item-text="initials"
                          item-value="initials"
                          rules="required"
                          label="UF"
                        />
                      </v-col>
                      <v-col cols="12" md="4">
                        <VInputWithValidation
                          v-model="deliveryAddress.city"
                          :loading="showLoading"
                          dense
                          label="Cidade *"
                          rules="required|max:255"
                          counter="255"
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <div class="form-action-buttons text-center">
                    <v-btn v-if="!showLoading" color="green darken-1" @click="cancelEdit">Cancelar</v-btn>
                    <v-btn
                      v-if="!showLoading"
                      color="green darken-1"
                      class="ml-4"
                      :disabled="invalid"
                      @click="saveItem(deliveryAddress)"
                    >{{ formButton }}</v-btn>
                    <v-btn
                      v-else
                      :loading="showLoading"
                      color="green darken-1"
                    >Salvando</v-btn>
                  </div>
                </v-card-actions>
              </v-card>
            </ValidationObserver>
          </v-dialog>
          <!-- / Modal -->
        </v-toolbar>
      </template>
      <template v-slot:no-data>Nenhum Registro encontrado!</template>
    </v-data-table>

    <v-divider />
    <v-card-actions>
      <div class="form-action-buttons text-center">
        <v-btn
          color="primary"
          title="Volta para Listagem de Clientes"
          class="mr-5 mb-2"
          @click="$router.back();"
        >Fechar</v-btn>
        <v-btn color="primary" class="mr-5 mb-2" @click="previousStep(stepperIndex)">Anterior</v-btn>
        <v-btn v-if="updateClient" color="primary" class=" mb-2" @click="saveInfo()">Continuar</v-btn>
      </div>
    </v-card-actions>
    <!-- end / Dados de Entrega -->
    <loading :showLoading="showLoading" />
  </v-card>
</template>

<script>
import { mapMutations, mapGetters } from "vuex";
import _ from "lodash";
// Para o Formulário
import { ValidationObserver } from "vee-validate";
import VInputWithValidation from "~/components/inputs/VInputWithValidation";
import VSelectWithValidation from "~/components/inputs/VSelectWithValidation";

export default {
  name: "customer-delivery-addresses",
  components: {
    ValidationObserver,
    VInputWithValidation,
    VSelectWithValidation
  },

  props: {
    customerId: {
      type: [String],
      default: ""
    },
    isStepper: {
      type: Boolean,
      default: false
    },
    stepperIndex: {
      type: Number,
      default: 0
    },
    previousStep: {
      type: [Object, Function],
      default: () => {}
    },
    nextStep: {
      type: [Object, Function],
      default: () => {}
    }
  },

  // To undestand "why fetch()"
  // See https://nuxtjs.org/api/pages-fetch#the-fetch-method
  async fetch({ store }) {
    // call a Service to request All States from API and will set $store.generics.states
    await store.dispatch("generics/getStates");
  },

  data() {
    return {
      // Dados necessários
      showLoading: false,
      citiesByState: [],
      citizenship: {
        state: {
          name: "",
          id: ""
        },
        city: {
          name: "",
          id: ""
        }
      },
      // Insert and Update Item
      on: false,
      editedIndex: -1,
      editedItem: {
        userId: "",
        name: "",
        document: "",
        cpf: "",
        profession: "",
        maritalStatus: "",
        cellPhoneNumber: "",
        status: "",
        _id: "",
        isCivilServent: false,
        email: "",
        birthdate: "",
        isPep: false,
        pepDescription: "",
        citizenship: {
          _id: "",
          state: null,
          city: ""
        },
        deliveryAddress: [],
        createdAt: "",
        updatedAt: "",
        address: {
          _id: "",
          cep: "",
          city: "",
          complement: "",
          number: "",
          state: "",
          street: ""
        }
      },
      defaultItem: {
        userId: "",
        name: "",
        document: "",
        cpf: "",
        profession: "",
        maritalStatus: "",
        cellPhoneNumber: "",
        status: "",
        _id: "",
        isCivilServent: false,
        email: "",
        birthdate: "",
        isPep: false,
        pepDescription: "",
        citizenship: {
          _id: "",
          state: null,
          city: ""
        },
        deliveryAddress: [],
        createdAt: "",
        updatedAt: "",
        address: {
          _id: "",
          cep: "",
          city: "",
          complement: "",
          number: "",
          state: "",
          street: ""
        }
      },
      // DataTable
      showDelivery: false,
      delivery: {
        itemsPerPage: 20,
        footerProps: { "items-per-page-options": [10, 20, 30, 40, 50] },
        title: "Endereços de Entrega",
        search: "",
        headers: [
          { text: "CEP", value: "cep" },
          { text: "Logradouro", value: "street" },
          { text: "Número", value: "number" },
          { text: "Complemento", value: "complement" },
          { text: "Estado", value: "state" },
          { text: "Cidade", value: "city" },
          {
            text: "Ações",
            value: "action",
            sortable: false,
            class: ["text-center"]
          }
        ],
        items: []
      },
      deliveryAddress: {
        cep: "",
        city: "",
        complement: "",
        number: "",
        state: "",
        street: ""
      },
      // Permissões por Action
      updateClient: false,
      getByCep: false,
      getState: false,
      getCityByState: false
    };
  },

  watch: {
    customerId(newValue, oldValue) {
      if (newValue) {
        this.getCustomerById(newValue);
      }
    }
  },

  computed: {
    ...mapGetters({
      enumCustomerStatus: "enumCustomerStatus",
      enumMaritalStatus: "generics/enumMaritalStatus",
      states: "generics/states",
      checkActions: "checkActions"
    }),
    formDelivery() {
      return this.editedIndex === -1 ? "Novo Endereço" : "Editar Endereço";
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
      this.resetForm();

      // Verificações de Permissões
      this.checkPermissions();

      if (this.customerId) {
        this.getCustomerById(this.customerId);
      }
    },

    resetForm() {
      this.editedItem = Object.assign({}, this.defaultItem);
    },

    async getCustomerById(userId) {
      this.showLoading = true;
      try {
        const response = await this.$customerServices.getById(
          "getClientDetails?userId=",
          userId
        );
        this.editedItem = Object.assign({}, response);
        this.showLoading = false;
      } catch (error) {
        console.error("getCustomerById", error);
        this.showLoading = false;
      }
    },

    async getAddressByCEP(obj) {
      if (obj.cep && obj.cep.length > 8) {
        this.showLoading = true;
        try {
          let cep = obj.cep;
          const response = await this.$addressServices.get(
            `getByCep?cep=${cep}`
          );
          if (response) {
            obj.street = response.logradouro;
            obj.state = response.uf;
            obj.city = response.localidade;
            obj.complement = response.bairro;
          }
          this.showLoading = false;
        } catch (error) {
          console.error("getAddressByCEP", error);
          this.showLoading = false;
        }
      }
    },

    newDelivery() {
      this.resetDelivery();
      this.showDelivery = true;
    },

    editItem(item) {
      this.editedIndex = this.editedItem.deliveryAddress.indexOf(item);
      this.deliveryAddress = Object.assign(
        {},
        this.editedItem.deliveryAddress[this.editedIndex]
      );
      this.showDelivery = true;
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
            //this.$store.commit("customer/deleteDeliveryAddress", this.editedItem.userId, item);
            this.editedIndex = this.editedItem.deliveryAddress.indexOf(item);
            // call Delete Process on endpoint!
            this.editedItem.deliveryAddress.splice(this.editedIndex, 1);
            this.$swal.fire(
              "Deletado!",
              "Registro removido com sucesso",
              "success"
            );
          }
        });
    },

    saveItem(item) {
      if (this.editedIndex > -1) {
        // update
        this.editedItem.deliveryAddress[this.editedIndex] = item;
      } else {
        // insert
        // Remove _id
        delete item._id;
        this.editedItem.deliveryAddress.push(item);
      }
      this.$toast.success("Operação realizada com Sucesso!");
      this.showDelivery = false;
    },

    cancelEdit() {
      this.resetDelivery();
      this.showDelivery = false;
    },

    resetDelivery() {
      // clear errors from validations
      if (this.$refs.deliveryObserver) {
        this.$refs.deliveryObserver.reset();
      }
      this.editedIndex = -1;
      this.deliveryAddress = Object.assign({}, this.defaultItem.address);
    },

    async saveInfo() {
      this.showLoading = true;
      try {
        let birthday = this.$moment(
          this.$core.formatDateStringBR(this.editedItem.birthdate)
        ).format("YYYY-MM-DDTHH:mm:ss.SSSZ");
        this.editedItem.birthdate = birthday;

        // Indiferente de Insert ou Update o back-end fará o serviço
        const response = await this.$store.dispatch(
          "customer/save",
          this.editedItem
        );

        if (response && response.success) {
          this.$toast.success("Operação realizada com Sucesso!");
          this.nextStep(this.stepperIndex);
        } else if (response) {
          if (response.message && typeof response.message === "string") {
            this.$toast.error(`Ops: ${response.message}`);
          } else {
            this.$toast.error("Oops ocorreu um erro! Contate o Administrador.");
          }
        }
      } catch (error) {
        console.error("saveInfo", error);
      } finally {
        this.showLoading = false;
      }
    },

    cancel() {
      this.initialize();
    },

    checkPermissions() {
      this.updateClient = this.checkActions("updateClient");      
    }
  }
};
</script>