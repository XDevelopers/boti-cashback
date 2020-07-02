<template>
  <v-card class="mb-3" style="box-shadow: unset;">
    <!-- Observações e Status -->

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
          <v-btn color="primary" title="Volta para Listagem de Clientes" class="mr-5 mb-2" @click="$router.back();">Fechar</v-btn>
        <v-btn
            color="primary"
            class="mr-5 mb-2"
            @click="previousStep(stepperIndex)"
          >Anterior</v-btn>
          <v-btn
            v-if="updateClient"
            color="primary"
            :disabled="invalid"
            class=" mb-2"
            @click="saveInfo()"
          >Salvar Dados</v-btn>
        </div>
      </v-card-actions>
    </ValidationObserver>
    <!-- end / Dados Pessoais -->

    <loading :showLoading="showLoading" />
  </v-card>
</template>

<script>
import { mapGetters } from "vuex";
import _ from "lodash";
// Para o Formulário
import { ValidationObserver } from "vee-validate";
import VSelectWithValidation from "~/components/inputs/VSelectWithValidation";

export default {
  components: {
    ValidationObserver,
    VSelectWithValidation
  },

  props: {
    customerId: {
      type: String,
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

  data() {
    return {
      // Dados necessários
      showLoading: false,
      loadingFields: false,
      editedIndex: -1,
      editedItem: {
        userId: "",
        status: "",
        observation: ""
      },
      defaultItem: {
        userId: "",
        status: "",
        observation: ""
      },
      // Permissões por Action
      updateClient: false
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
      checkActions: "checkActions"
    })
  },

  mounted() {
    this.initialize();
  },

  methods: {
    initialize() {

      // Verificações de Permissões
      this.checkPermissions();

      this.resetForm();
      if (this.customerId) {
        this.getCustomerById(this.customerId);
      }
    },

    resetForm() {
      this.editedItem = Object.assign({}, this.defaultItem);
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
        this.editedItem = Object.assign({}, response);
      } catch (error) {
        console.error("getCustomerById", error);
      } finally {
        this.showLoading = false;
      }
    },

    async saveInfo() {
      this.showLoading = true;
      try {
        let urlRedirect = this.editedItem.userId ? "/clientes" : null;
        // format 1984-08-20T00:00:00.000Z
        let birthday = this.$moment(
            this.$core.formatDateStringBR(this.editedItem.birthdate)
        ).format("YYYY-MM-DDTHH:mm:ss.SSSZ");

        this.editedItem.birthdate = birthday;
        
        // Indiferente de Insert ou Update o back-end fará o serviço
        const response = await this.$store.dispatch("customer/save", this.editedItem);
        
        if (response && response.success) {
          this.$toast.success("Operação realizada com Sucesso!");

          if (urlRedirect){
            this.$router.push(urlRedirect);
          }
        } else if (response) {
          if (response.message && typeof response.message === "string") {
            this.$toast.error(`Ops: ${response.message}`);
          } else {
            this.$toast.error("Oops ocorreu um erro! Contate o Administrador.");
          }
        }
      } catch (error) {
        console.error(error);
      } finally {
        this.showLoading = false;
      }
    },

    cancel() {
      this.resetForm();
    },

    checkPermissions() {
      this.updateClient = this.checkActions("updateClient");
    }
  }
};
</script>