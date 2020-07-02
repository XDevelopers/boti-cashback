<template>
  <div class="content n-content">
    <!-- Área de Mensagens -->
    <alert v-if="msg" :message="msg" />
    <!-- end/ Área de Mensagens -->

    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <!-- Wizard -->
            <v-stepper v-model="stepper" dark style="background:#2b3442 !important">
              <template>
                <!-- stepper header -->
                <v-stepper-header>
                  <template v-for="(step, i) in steps">
                    <v-stepper-step
                      :key="i"
                      :step="step.index"
                      :editable="editable"
                      :complete="stepper > step.index"
                      style="background:#2b3442 !important"
                    >{{ step.title }}</v-stepper-step>
                  </template>
                </v-stepper-header>
                <!-- end / stepper header -->

                <v-stepper-items>
                  <v-stepper-content step="1">
                    <!-- Dados Pessoais -->
                    <!--
                    <customer-form
                      :key="currentKey"
                      :customer-id="editedItem.userId"
                      :is-stepper="true"
                      :stepper-index="1"
                      :next-step="nextStep"
                    />
                    -->
                    <ValidationObserver ref="personalDataObserver" v-slot="{ invalid }">
                      <v-row justify="space-between">
                        <v-col cols="12" md="4">
                          <VInputWithValidation
                            v-model="editedItem.name"
                            label="Nome Completo *"
                            rules="required|min:3|max:255"
                            dense
                            counter="255"
                          />
                        </v-col>
                        <v-col cols="12" md="3">
                          <VInputWithValidation
                            v-model="editedItem.cpf"
                            v-mask="'###.###.###-##'"
                            dense
                            label="CPF *"
                            rules="required|min:14|max:14|cpf"
                            counter="14"
                          />
                        </v-col>
                        <v-col cols="12" md="3">
                          <VInputWithValidation
                            v-model="editedItem.document"
                            label="RG, CNH, Passaporte *"
                            rules="required|min:3|max:255"
                            counter="255"
                            dense
                          />
                        </v-col>
                        <v-col cols="12" md="2">
                          <v-menu
                            v-model="menuBirthdate"
                            :close-on-content-click="false"
                            transition="scale-transition"
                            offset-y
                            max-width="290px"
                            min-width="290px"
                          >
                            <template v-slot:activator="{ on }">
                              <VInputWithValidation
                                v-model="editedItem.birthdate"
                                v-mask="'##/##/####'"
                                label="Data de Nascimento *"
                                :rules="`required|birthday:${minimalAge}`"
                                prepend-icon="event"
                                dense
                                v-on="on"
                                @blur="date = parseDate(editedItem.birthdate)"
                              />
                            </template>
                            <v-date-picker
                              v-model="date"
                              locale="pt-BR"
                              scrollable
                              no-title
                              @input="menuBirthdate = false"
                            />
                          </v-menu>
                        </v-col>
                      </v-row>

                      <v-row justify="space-between">
                        <v-col cols="12" md="3">
                          <VSelectWithValidation
                            v-model="citizenship.state"
                            :items="states"
                            :loading="loadingFields"
                            :disabled="loadingFields"
                            @change="getCitiesByState(citizenship.state)"
                            item-text="name"
                            item-value="id"
                            rules="required"
                            label="Estado de Nascimento *"
                          />
                        </v-col>
                        <v-col cols="12" md="3">
                          <VSelectWithValidation
                            v-model="editedItem.citizenship.city"
                            v-if="editedItem.citizenship"
                            :items="citiesByState"
                            :loading="loadingFields"
                            :disabled="loadingFields"
                            @change="setCitizenshipCity()"
                            @click="setCitizenshipCity()"
                            item-text="name"
                            item-value="id"
                            label="Cidade de Nascimento *"
                            rules="required"
                            dense
                          />
                        </v-col>
                        <v-col cols="12" md="3">
                          <VInputWithValidation
                            v-model="editedItem.email"
                            label="E-mail *"
                            rules="required|email|max:255"
                            dense
                            counter="255"
                          />
                        </v-col>
                        <v-col cols="12" md="3">
                          <VInputWithValidation
                            v-model="editedItem.cellPhoneNumber"
                            v-mask="['(##) #####-####','(##) ####-####']"
                            label="Celular *"
                            rules="required"
                            dense
                          />
                        </v-col>
                      </v-row>

                      <v-row justify="space-between">
                        <v-col cols="12" md="3">
                          <VInputWithValidation
                            v-model="editedItem.profession"
                            dense
                            label="Profissão"
                            rules="max:255"
                            counter="255"
                          />
                        </v-col>
                        <v-col cols="12" md="3">
                          <VSelectWithValidation
                            v-model="editedItem.maritalStatus"
                            :items="enumMaritalStatus"
                            item-text="name"
                            item-value="code"
                            rules="required"
                            label="Estado Cívil *"
                          />
                        </v-col>
                        <v-col cols="12" md="3"></v-col>
                        <v-col cols="12" md="3"></v-col>
                      </v-row>

                      <v-row justify="space-between">
                        <v-col cols="12" md="3">
                          <v-switch
                            v-model="editedItem.isCivilServent"
                            class="mx-2"
                            color="success"
                            label="Servidor Público"
                          ></v-switch>
                        </v-col>
                        <v-col cols="12" md="4">
                          <v-switch
                            v-model="editedItem.isPep"
                            class="mx-2"
                            color="success"
                            label="Pessoa Exposta Publicamente"
                          ></v-switch>
                        </v-col>
                        <v-col cols="12" md="4">
                          <VInputWithValidation
                            v-if="editedItem.isPep"
                            v-model="editedItem.pepDescription"
                            :rules="{ required: editedItem.isPep, max:255 }"
                            dense
                            label="Nome Pessoa Exposta Publicamente"
                            counter="255"
                          />
                        </v-col>
                        <v-col cols="12" md="1"></v-col>
                      </v-row>

                      <fieldset v-if="editedItem.address">
                        <legend>Endereço de Cadastro</legend>
                        <v-row justify="space-between">
                          <v-col cols="12" md="1">
                            <VInputWithValidation
                              v-model="editedItem.address.cep"
                              v-mask="'#####-###'"
                              :disabled="!getByCep"
                              dense
                              counter
                              label="CEP *"
                              rules="required|max:9"
                              @change="getAddressByCEP(editedItem.address)"
                            />
                          </v-col>
                          <v-col cols="12" md="3">
                            <VInputWithValidation
                              v-model="editedItem.address.street"
                              dense
                              label="Logradouro (R. Av. Trav.) *"
                              rules="required|max:255"
                              counter="255"
                            />
                          </v-col>
                          <v-col cols="12" md="1">
                            <VInputWithValidation
                              v-model="editedItem.address.number"
                              dense
                              label="Número *"
                              rules="required|max:10"
                              counter="10"
                            />
                          </v-col>
                          <v-col cols="12" md="2">
                            <VInputWithValidation
                              v-model="editedItem.address.complement"
                              dense
                              label="Complemento"
                              rules="max:255"
                              counter="255"
                            />
                          </v-col>
                          <v-col cols="12" md="2">
                            <VSelectWithValidation
                              v-model="editedItem.address.state"
                              :items="states"
                              :disabled="!getState"
                              item-text="initials"
                              item-value="initials"
                              rules="required"
                              label="UF *"
                            />
                          </v-col>
                          <v-col cols="12" md="2">
                            <VInputWithValidation
                              v-model="editedItem.address.city"
                              dense
                              label="Cidade *"
                              rules="required|max:255"
                              counter="255"
                            />
                          </v-col>
                        </v-row>
                        <v-row justify="space-between">
                          <v-col cols="12" md="3"></v-col>
                          <v-col cols="12" md="3"></v-col>
                          <v-col cols="12" md="2"></v-col>
                          <v-col cols="12" md="2"></v-col>
                          <v-col cols="12" md="2"></v-col>
                        </v-row>
                      </fieldset>
                      <!--
                        {{ valid }}
                        <br />
                        {{ invalid }}
                        <br />
                        {{ errors }}
                      -->
                      <v-divider />
                      <v-card-actions>
                        <div class="form-action-buttons text-center">
                          <v-btn
                            color="primary"
                            title="Volta para Listagem de Clientes"
                            class="mr-5 mb-2"
                            @click="$router.back();"
                          >Fechar</v-btn>
                          <v-btn
                            v-if="updateClient"
                            color="primary"
                            :disabled="invalid"
                            class="mb-2"
                            @click="saveInfo(1)"
                          >Continuar</v-btn>
                        </div>
                      </v-card-actions>
                    </ValidationObserver>
                    <!-- end / Dados Pessoais -->
                  </v-stepper-content>

                  <v-stepper-content step="6">
                    <!-- Dados de Entrega -->
                    <!--
                    <customer-delivery-addresses
                      :customer-id="editedItem.userId"
                      :is-stepper="true"
                      :stepper-index="2"
                      :previous-step="previousStep"
                      :next-step="nextStep"
                    />
                    -->
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
                            <v-icon
                              v-on="on"
                              color="green darken-3"
                              @click="editItem(item)"
                              class="edit mr-2"
                            >edit</v-icon>
                            <!-- <span v-if="item._id">{{ item._id }}</span> -->
                          </template>
                          <span>Editar Item</span>
                        </v-tooltip>
                        <v-tooltip top color="info">
                          <template v-slot:activator="{ on }">
                            <v-icon
                              v-on="on"
                              color="red darken-1"
                              @click="deleteItem(item)"
                              class
                            >delete</v-icon>
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
                                    <v-btn
                                      v-if="!showLoading"
                                      color="green darken-1"
                                      @click="cancelEdit"
                                    >Cancelar</v-btn>
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
                        <v-btn color="primary" class="mr-5 mb-2" @click="previousStep(2)">Anterior</v-btn>
                        <v-btn
                          v-if="updateClient"
                          color="primary"
                          class="mb-2"
                          @click="saveInfo(2)"
                        >Continuar</v-btn>
                      </div>
                    </v-card-actions>
                    <!-- end / Dados de Entrega -->
                  </v-stepper-content>

                  <v-stepper-content step="2">
                    <!-- Dados de Monetários -->
                    <customer-documentation
                      :customer-id="editedItem.userId"
                      :is-stepper="true"
                      :stepper-index="2"
                      :next-step="nextStep"
                      :previous-step="previousStep"
                    />
                    <!-- end Dados de Monetários -->
                  </v-stepper-content>

                  <v-stepper-content step="3">
                    <!-- Documentos -->
                    <!--
                    <customer-form-observation
                      :customer-id="editedItem.userId"
                      :is-stepper="true"
                      :stepper-index="4"
                      :previous-step="previousStep"
                    />
                    -->
                    <ValidationObserver ref="observationDataObserver" v-slot="{ invalid }">
                      <v-row justify="space-between">
                        <v-col cols="12" md="4">
                          <VSelectWithValidation
                            v-model="editedItem.status"
                            :items="enumCustomerStatus"
                            item-text="name"
                            item-value="code"
                            rules="required"
                            label="Status do Cliente *"
                          />
                        </v-col>

                        <v-col cols="12" md="6">
                          <v-textarea
                            v-model="editedItem.observation"
                            label="Observações"
                            dense
                            outlined
                            counter
                          />
                        </v-col>
                      </v-row>

                      <v-divider />
                      <v-card-actions>
                        <div class="form-action-buttons text-center">
                          <v-btn
                            color="primary"
                            title="Volta para Listagem de Clientes"
                            class="mr-5 mb-2"
                            @click="$router.back();"
                          >Fechar</v-btn>
                          <v-btn color="primary" class="mr-5 mb-2" @click="previousStep(3)">Anterior</v-btn>
                          <v-btn
                            v-if="updateClient"
                            color="primary"
                            :disabled="invalid"
                            class="mb-2"
                            @click="saveInfo(3)"
                          >Salvar Dados</v-btn>
                        </div>
                      </v-card-actions>
                    </ValidationObserver>
                    <!-- end Documentos -->
                  </v-stepper-content>
                </v-stepper-items>
              </template>
            </v-stepper>
            <!-- end/ Wizard -->
          </div>
        </div>
      </div>
    </div>
    <loading :showLoading="showLoading" />
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import { ValidationObserver, extend, validate } from "vee-validate";
extend("cpf", value => {
  let result = window.checkCPF(value.replace(/\./g, "").replace("-", ""));
  return result ? true : "O {_field_} informado não é válido!";
});
extend("birthday", {
  validate(value, args) {
    let result = window.checkBirthday(value, args.minimalAge);
    return result
      ? true
      : `A {_field_} não é válida! Menor de ${args.minimalAge} Anos.`;
  },
  params: ["minimalAge"]
});
import VInputWithValidation from "~/components/inputs/VInputWithValidation";
import VSelectWithValidation from "~/components/inputs/VSelectWithValidation";

//import CustomerForm from "~/components/customer/CustomerForm";
//import CustomerDeliveryAddresses from "~/components/customer/CustomerDeliveryAddresses";
//import CustomerFormObservation from "~/components/customer/CustomerFormObservation";
import CustomerDocumentation from "~/components/customer/CustomerDocumentation";

export default {
  meta: {
    title: "Cliente"
  },

  middleware: ["auth", "page-agent"],

  components: {
    ValidationObserver,
    VInputWithValidation,
    VSelectWithValidation,
    //  CustomerForm,
    //CustomerDeliveryAddresses,
    //CustomerFormObservation,
    CustomerDocumentation
  },

  // To undestand "why fetch()"
  // See https://nuxtjs.org/api/pages-fetch#the-fetch-method
  async fetch({ store }) {
    // call a Service to request All States from API and will set $store.generics.states
    await store.dispatch("settings/getConfigurations");
  },

  data() {
    return {
      title: this.$store.state.title,
      msg: "",
      showLoading: false,
      currentKey: 0,
      // Steppers
      editable: true,
      stepper: 0,
      steps: [
        {
          index: "1",
          title: "Dados Pessoais",
          type: "personalData"
        },
        // {
        //   index: "2",
        //   title: "Dados de Entrega",
        //   type: "deliveryData"
        // },
        {
          index: "2",
          title: "Imagens dos Documentos",
          type: "documentImages"
        },
        {
          index: "3",
          title: "Observações",
          type: "observations"
        }
      ],
      // Dados Pessoais
      loadingFields: false,
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
      // Insert and Update Item (Formulário Principal)
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
        birthdate: "", //new Date().toISOString().substr(0, 10),
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
      // Date Picker
      menuBirthdate: false,
      date: "", //new Date().toISOString().substr(0, 10),
      // DataTable (Endereços de Entrega)
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
      emptyDeliveryAddress: {
        cep: "",
        city: "",
        complement: "",
        number: "",
        state: "",
        street: ""
      },
      // Permissões por Action
      updateClient: false,
      getByCep: true,
      getState: true,
      getCityByState: true
    };
  },

  watch: {
    customerId(newValue, oldValue) {
      if (newValue) {
        this.getCustomerById(newValue);
      }
    },
    date(val) {
      //console.log("watch", this.formatDate(this.date));
      this.editedItem.birthdate = this.formatDate(this.date);
    }
  },

  computed: {
    ...mapGetters({
      enumCustomerStatus: "enumCustomerStatus",
      enumMaritalStatus: "generics/enumMaritalStatus",
      states: "generics/states",
      checkActions: "checkActions",
      minimalAge: "settings/getMinimalAge"
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
    async initialize() {
      // Verificações de Permissões
      this.checkPermissions();

      console.clear();

      window.checkCPF = this.$core.checkCPF;
      window.checkBirthday = this.$core.checkBirthday;
      this.resetForm();

      if (this.states && this.states.length <= 0) {
        await this.$store.dispatch("generics/getStates");
      }

      if (this.$route.params.id || this.$route.query.id) {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.stepper = 1;
        this.editedItem.userId = this.$route.params.id
          ? this.$route.params.id
          : this.$route.query.id;
        this.getCustomerById(this.editedItem.userId);
      }
    },

    previousStep(n) {
      if (n === this.steps) {
        this.stepper = 1;
      } else {
        this.stepper = n - 1;
      }

      switch (this.stepper.toString()) {
        case "0":
          // Dados Pessoais
          // See https://michaelnthiessen.com/force-re-render/
          this.currentKey += 1;
          break;
        default:
          break;
      }
    },

    nextStep(n) {
      if (n === this.steps.length) {
        this.stepper = 1;
      } else {
        this.stepper = n + 1;
      }
    },

    //
    // Metodos dos Dados Pessoais
    //
    resetForm() {
      this.editedItem = Object.assign({}, this.defaultItem);
      // clear errors from validations
      if (this.$refs.personalDataObserver) {
        this.$refs.personalDataObserver.reset();
      }
      // clear errors from validations
      if (this.$refs.observationDataObserver) {
        this.$refs.observationDataObserver.reset();
      }
    },

    async getCustomerById(userId) {
      this.showLoading = true;
      try {
        const response = await this.$customerServices.getById(
          "getClientDetails?userId=",
          userId
        );
        if (response && response.userId) {
          this.editedItem = Object.assign({}, response);
          // Setando o DatePicker
          this.date = this.parseDate(this.editedItem.birthdate);
          if (!this.editedItem.address) {
            this.editedItem.address = Object.assign(
              {},
              this.defaultItem.address
            );
          }
          // Processo para setState and LoadCitiesByState
          setTimeout(() => {
            if (
              this.editedItem &&
              this.editedItem.citizenship &&
              this.editedItem.citizenship.state
            ) {
              let currentState = this.states.find(
                s => s.id == this.editedItem.citizenship.state
              );
              if (currentState) {
                this.citizenship.state = currentState;
                this.getCitiesByState();
              }
            }
          }, 500);
        }
      } catch (error) {
        console.error("getCustomerById", error);
      } finally {
        this.showLoading = false;
      }
    },

    getCitiesByState() {
      // if (!this.getCityByState) {
      //   this.loadingFields = false;
      //   return;
      // }
      this.editedItem.citizenship.state = this.citizenship.state.id
        ? this.citizenship.state.id
        : this.citizenship.state;

      if (this.editedItem.citizenship.state) {
        this.loadingFields = true;
        this.citiesByState = [];
        this.$regionServices
          .get(`getCitiesByState?uf=${this.editedItem.citizenship.state}`)
          .then(res => {
            this.citiesByState = res.map(c => {
              return {
                name: c.name,
                id: c.id.toString()
              };
            });
            // Processo para setState and LoadCitiesByState
            setTimeout(() => {
              if (
                this.editedItem &&
                this.editedItem.citizenship &&
                this.editedItem.citizenship.city
              ) {
                let currentCity = this.citiesByState.find(
                  s => s.id == this.editedItem.citizenship.city
                );
                if (currentCity) {
                  this.citizenship.city = currentCity;
                  this.loadingFields = false;
                }
              }
            }, 500);
            this.loadingFields = false;
          })
          .catch(res => {
            this.loadingFields = false;
          });
      }
    },

    setCitizenshipCity() {
      if (
        this.citizenship &&
        this.citizenship.city &&
        this.citizenship.city.id
      ) {
        this.editedItem.citizenship.city = this.citizenship.city.id
          ? this.citizenship.city.id
          : this.citizenship.city;
      }
    },

    async getAddressByCEP(obj) {
      // if (!this.getByCep) {
      //   return;
      // }
      if (obj.cep && obj.cep.length > 8) {
        this.showLoading = true;
        try {
          let cep = obj.cep;
          const response = await this.$addressServices.get(
            `getByCep?cep=${cep}`
          );
          if (response && response.logradouro) {
            obj.street = response.logradouro;
            obj.state = response.uf;
            obj.city = response.localidade;
            //obj.complement = response.bairro;
          }
          this.showLoading = false;
        } catch (error) {
          console.error("getAddressByCEP", error);
          this.showLoading = false;
        }
      }
    },

    async saveInfo(stepperIndex) {
      this.showLoading = true;
      try {
        let urlRedirect = this.editedItem.userId ? "/clientes" : null;
        // format 1984-08-20T00:00:00.000Z
        let brFormat = this.editedItem.birthdate;
        let birthday = this.$moment(
          this.$core.formatDateStringBR(brFormat)
        ).format("YYYY-MM-DDTHH:mm:ss.SSSZ");
        this.editedItem.birthdate = birthday;

        // Indiferente de Insert ou Update o back-end fará o serviço
        const response = await this.$store.dispatch(
          "customer/save",
          this.editedItem
        );

        if (response && response.success) {
          this.nextStep(stepperIndex);
          this.$toast.success("Operação realizada com Sucesso!");
          this.editedItem.birthdate = brFormat;
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
      this.resetForm();
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
    },

    checkPermissions() {
      this.updateClient = this.checkActions("updateClient");
      this.getDocumentImages = this.checkActions("getDocumentImages");
      this.getDocuments = this.checkActions("getDocuments");

      // Dimas e Wesley Removeram as Actions Abaixo e não me avisaram :(
      //this.getByCep = this.checkActions("getByCep");
      //this.getState = this.checkActions("getState");
      //this.getCityByState = this.checkActions("getCityByState");
    },
    //
    // Final dos metodos dos Dados Pessoais
    //
    //
    // Funções do Fluxo de CRUD do Endereços de entrega
    //
    newDelivery() {
      this.resetDelivery();
      this.showDelivery = true;
    },

    editItem(item) {
      console.log("editem", item);
      this.editedIndex = this.editedItem.deliveryAddress //.indexOf(item);
        .findIndex(
          de =>
            de.cep === item.cep &&
            de.street === item.street &&
            de.state === item.state &&
            de.city.toLowerCase() === item.city.toLowerCase() &&
            de.number === item.number &&
            de.complement === item.complement
        );
      console.log(this.editedIndex);
      this.deliveryAddress = Object.assign({}, item);
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
      try {
        if (this.editedIndex > -1) {
          // update
          this.editedItem.deliveryAddress[this.editedIndex] = item;
        } else {
          // insert
          // Remove _id
          delete item._id;
          if (
            this.editedItem.deliveryAddress.find(de => {
              return (
                de.cep === item.cep &&
                de.street === item.street &&
                de.state === item.state &&
                de.city.toLowerCase() === item.city.toLowerCase() &&
                de.number === item.number &&
                de.complement === item.complement
              );
            })
          ) {
            this.$toast.error(
              "Já existe um endereço informado com os mesmos dados. Verifique!"
            );
            return;
          }
          this.editedItem.deliveryAddress.push(item);
        }
        this.$toast.success("Operação realizada com Sucesso!");
      } catch (error) {
        console.error("saveItem", error);
      } finally {
        this.cancelEdit();
      }
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
      this.deliveryAddress = Object.assign({}, this.emptyDeliveryAddress);
    }
  }
};
</script>

<style>
/* Status, StatusPedidos, Ações */
.tb-delivery-address table tbody tr td:nth-child(7) {
  text-align: center;
}
</style>