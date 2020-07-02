<template>
  <v-card class="mb-3" style="box-shadow: unset;">
    <!-- Dados Pessoais -->

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
            @change="getCitiesByState()"
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
            @click="saveInfo()"
          >Continuar</v-btn>
        </div>
      </v-card-actions>
    </ValidationObserver>
    <!-- end / Dados Pessoais -->

    <loading :showLoading="showLoading" />
  </v-card>
</template>

<script>
import { mapMutations, mapGetters } from "vuex";
import _ from "lodash";
// Para o Formulário
import { ValidationObserver, extend } from "vee-validate";
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

export default {
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
    await store.dispatch("settings/getConfigurations");
  },

  data() {
    return {
      // Dados necessários
      showLoading: false,
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
    })
  },

  mounted() {
    this.initialize();
  },

  methods: {
    initialize() {
      window.checkCPF = this.$core.checkCPF;
      window.checkBirthday = this.$core.checkBirthday;
      this.resetForm();

      // Verificações de Permissões
      this.checkPermissions();

      if (this.customerId) {
        this.getCustomerById(this.customerId);
      }
    },

    resetForm() {
      this.editedItem = Object.assign({}, this.defaultItem);
      // clear errors from validations
      if (this.$refs.personalDataObserver) {
        this.$refs.personalDataObserver.reset();
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
                this.getCitiesByState(currentState.id);
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
      if (!this.getCityByState) {
        this.loadingFields = false;
        return;
      }
      this.editedItem.citizenship.state = this.citizenship.state.id
        ? this.citizenship.state.id
        : this.citizenship.state;

      if (this.editedItem.citizenship.state) {
        this.loadingFields = true;
        this.citiesByState = [];
        this.$regionServices
          .get(`getCitiesByState?uf=${this.editedItem.citizenship.state}`)
          .then(res => {
            this.citiesByState = res.data.map(c => {
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
      if (!this.getByCep) {
        return;
      }
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

    async saveInfo() {
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
          this.nextStep(this.stepperIndex);
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
      this.getByCep = this.checkActions("getByCep");
      this.getState = this.checkActions("getState");
      this.getCityByState = this.checkActions("getCityByState");
    }
  }
};
</script>