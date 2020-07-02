<template>
  <!-- Detalhes do Pedido -->
  <div class="v-data-table v-data-table--dense">
    <div class="v-data-table__wrapper">
      <table class="tb-order-details">
        <thead>
          <tr>
            <th colspan="8" class="text-center text-no-wrap">
              <strong class="body-1 text-center">Detalhes do Pedido</strong>
            </th>
          </tr>
          <tr>
            <th class="text-left text-no-wrap">
              <strong class="body-1 text-left">Status do Pedido</strong>
            </th>
            <th
              colspan="5"
              class="text-left text-no-wrap"
              style="border-left:0px transparent;border-right: 0px transparent;"
            >
              <!-- OrderStatus -->
              <v-col cols="12" sm="12" md="6" class="pa-0 pt-4 pb-0">
                <v-select
                  v-model="editedItem.orderStatus"
                  :items="enumOrderStatus"
                  label="Status do Pedido"
                  item-text="name"
                  item-value="code"
                  chips
                  max-height="auto"
                  dense
                  :disabled="isNotEditabled"
                  :multiple="false"
                >
                  <template slot="selection" slot-scope="data">
                    <v-chip @input="data.parent.selectItem(data.item)">
                      <v-list-item-content>
                        <v-list-item-title>
                          <v-icon :color="data.item.color">{{ data.item.icon }}</v-icon>&nbsp;
                          <span>{{ data.item.name }}</span>
                        </v-list-item-title>
                      </v-list-item-content>
                    </v-chip>
                  </template>
                  <template slot="item" slot-scope="data">
                    <template>
                      <v-list-item-icon>
                        <v-icon :color="data.item.color">{{ data.item.icon }}</v-icon>
                      </v-list-item-icon>
                      <v-list-item-content>
                        <v-list-item-title :class="`${data.item.color}--text`">{{ data.item.name }}</v-list-item-title>
                      </v-list-item-content>
                    </template>
                  </template>
                </v-select>
              </v-col>
              <!-- end / OrderStatus -->
            </th>
            <th colspan="2" class="text-center">
              <!-- Comprovante de Pagamento -->
              <v-btn
                v-if="editedItem.proofPayment"
                @click="openProofPayment(editedItem.contentType, editedItem.proofPayment)"
                color="primary"
              >Comprovante de Pagamento</v-btn>
              <v-btn
                v-else
                :disabled="!editedItem.proofPayment"
                color="primary"
              >Comprovante de Pagamento ainda pendente</v-btn>
            </th>
          </tr>
          <tr>
            <th class="text-center">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Moeda</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Valor (M.E.)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Taxa do Site (R$)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Total (R$)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Spread Cash Padrão (%)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Spread (R$)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2" title="Spread Cash Ajustado(%)">Spread Cash Ajustado (%)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Spread Banco (%)</strong>
              </div>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td width="10%" class="text-center">
              <!-- Moeda -->
              <span class="body-1 font-weight-light text-no-wrap">{{ editedItem.currency }}</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Valor (M.E.) -->
              <span class="body-1 font-weight-light text-no-wrap">$ {{ editedItem.value }}</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Taxa do Site (R$) -->
              <span v-if="isEditedSiteTax" class="body-1 font-weight-light text-no-wrap pt-3">
                <ValidationProvider rules="required" v-slot="{ errors }" name="Taxa Site">
                  <v-text-field
                    v-model="editedItem.siteTax"
                    :value="editedItem.siteTax"
                    v-mask="['#.##','##.##','###.###','####.####','#####.####']"
                    :error-messages="errors[0]"
                    class="edited-field"
                    @change="onSiteTaxChanged()"
                    dense
                  />
                </ValidationProvider>
              </span>

              <span v-else class="body-1 font-weight-light text-no-wrap">R$ {{ editedItem.siteTax }}</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Total (R$) -->
              <span class="body-1 font-weight-light">R$ {{ editedItem.total }}</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Spread Cash Padrão (%) -->
              <span class="body-1 font-weight-light">{{ editedItem.spreadCash }}</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Spread (R$) -->
              <span class="body-1 font-weight-light">R$ {{ editedItem.calculatedSpreadRS }}</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Spread Cash Ajustado (%) -->
              <span class="body-1 font-weight-light">{{ editedItem.calculatedSpread }} %</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Spread Banco (%) -->
              <span class="body-1 font-weight-light">{{ editedItem.spreadBank }}</span>
            </td>
          </tr>
          <!-- Headers (Segunda Linha) -->
          <tr>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">IOF {{ editedItem.iof }}% (R$)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Delivery (R$)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">VET (R$)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Taxa Comercial (API)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap" colspan="2">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Valor Total da Compra (TED ou PMT)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Taxa Banco Agora</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Resultado Cash (R$)</strong>
              </div>
            </th>
          </tr>
          <tr>
            <td class="text-center">
              <!-- IOF 1,1% (R$) -->
              <span class="body-1 font-weight-light text-no-wrap">{{ editedItem.calculatedIof }}</span>
            </td>
            <td class="text-center">
              <!-- 
                Delivery (R$) 30/05/2020 - Novamente !...  :) 
                v-mask="['#.##','##.##','##.###','##.####','###.####','####.####','#####.####']"
              -->
              <span
                v-if="isNotEditabled"
                class="body-1 font-weight-light text-no-wrap pt-3"
              >{{ editedItem.delivery }}</span>
              <span v-else class="body-1 font-weight-light text-no-wrap pt-3">
                <ValidationProvider rules="required|delivery" v-slot="{ errors }" name="Delivery">
                  <v-text-field
                    v-model="editedItem.delivery"
                    :value="editedItem.delivery"
                    v-mask="['#.##','##.##','###.##','####.##','#####.##']"
                    :error-messages="errors[0]"
                    prefix="R$"
                    class="edited-field"
                    @change="onDeliveryChanged()"
                    dense
                  />
                </ValidationProvider>
              </span>
            </td>
            <td class="text-center">
              <!-- VET (R$) -->
              <span class="body-1 font-weight-light text-no-wrap pt-3">{{ editedItem.vet }}</span>
            </td>
            <td class="text-center">
              <!-- Taxa Comercial (API) -->
              <span class="body-1 font-weight-light">R$ {{ editedItem.comercialTax }}</span>
            </td>
            <td class="text-center" colspan="2">
              <!-- Valor Total da Compra (TED ou PMT) -->
              <span class="body-1 font-weight-light">R$ {{ editedItem.totalOrderValue }}</span>
            </td>
            <td class="text-center">
              <!-- Taxa Banco Agora -->
              <span
                v-if="isNotEditabled"
                class="body-1 font-weight-light text-no-wrap pt-3"
              >{{ editedItem.bankTaxNow }}</span>
              <span v-else class="body-1 font-weight-light text-no-wrap pt-3">
                <ValidationProvider rules="required" v-slot="{ errors }" name="Taxa Banco Agora">
                  <v-text-field
                    v-model="editedItem.bankTaxNow"
                    :value="editedItem.bankTaxNow"
                    v-mask="['#.####','##.####','###.####','####.####','#####.####']"
                    :error-messages="errors[0]"
                    prefix="R$"
                    class="edited-field"
                    @change="onBankTaxNowChanged()"
                    dense
                  >{{ editedItem.bankTaxNow }}</v-text-field>
                </ValidationProvider>
              </span>
            </td>
            <td class="text-center">
              <!-- Resultado Cash (R$) -->
              <span class="body-1 font-weight-light">R$ {{ editedItem.cashResult }}</span>
            </td>
          </tr>
          <!-- Headers (Terceira Linha) -->
          <tr>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Opção de Pagamento</strong>
              </div>
            </th>
            <th colspan="4" class="text-Center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Endereço de Entrega</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Total Comprado no ano (R$)</strong>
              </div>
            </th>
            <th class="text-center text-no-wrap">
              <div class="d-flex align-center justify-center">
                <strong class="body-2">Total comprado com CASH (R$)</strong>
              </div>
            </th>
            <th class="text-left text-no-wrap">
              <div class="d-flex align-left justify-left">
                <strong class="body-2">Documentos</strong>
              </div>
            </th>
          </tr>
          <!-- Linha de Dados 3 -->
          <tr>
            <td width="10%" class="text-center">
              <!-- Opção de Pagamento -->
              <span
                class="body-1 font-weight-light text-no-wrap"
              >{{ editedItem.payOption | upperCase }}</span>
            </td>
            <td colspan="4" width="10%" class="text-center">
              <!-- Endereço de Entrega -->
              <span
                class="body-1 font-weight-light text-no-wrap"
              >{{ editedItem.address | formatAddress }}</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Total Comprado no ano (R$) -->
              <span
                class="body-1 font-weight-light text-no-wrap pt-3"
              >R$ {{ editedItem.yearTotalPurchase }}</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Total comprado com CASH (R$) -->
              <span class="body-1 font-weight-light">R$ {{ editedItem.totalPurchaseInCash }}</span>
            </td>
            <td width="10%" class="text-center">
              <!-- Documentos (Foram descontinuados) -->
              <span class="body-1 font-weight-light">OBS: {{ editedItem.observation }}</span>
            </td>
          </tr>
        </tbody>
        <tfoot class="text-center">
          <tr>
            <td colspan="9" class="caption font-weight-light grey--text" style="height: auto;">
              <v-row justify="center">
                <div class="my-2 mr-5">
                  <v-btn @click="onHistory()" color="warning">Histórico</v-btn>
                </div>
                <div class="my-2 ml-4 mr-5">
                  <v-btn @click="onCancel()" color="primary">Fechar</v-btn>
                </div>
                <div v-if="updatePurchase" class="my-2 ml-4 mr-4">
                  <v-btn v-if="!isNotEditabled" @click="onSave()" color="warning">Atualizar</v-btn>
                </div>
              </v-row>
            </td>
          </tr>
        </tfoot>
      </table>
    </div>

    <!-- Modal Dialog Comprovante de Pagamento -->
    <v-dialog
      v-model="dialogProofPayment"
      fullscreen
      hide-overlay
      transition="dialog-bottom-transition"
    >
      <v-card>
        <v-toolbar dark color="primary">
          <v-toolbar-title>Comprovante de Pagamento</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn icon dark @click="dialogProofPayment = false">
              <v-icon>mdi-close</v-icon>
            </v-btn>
          </v-toolbar-items>
        </v-toolbar>
        <v-card-text>
          <v-container>
            <v-row justify="center">
              <v-col cols="12" sm="12" md="10">
                <v-img
                  v-if="currentImage"
                  :src="currentImage"
                  aspect-ratio="1"
                  contain
                  class="grey lighten-2"
                  style="background-size: contain;"
                ></v-img>
                <iframe
                  v-if="currentDocument"
                  :src="`${currentDocument}`"
                  style="width: 100%; height: 800px;"
                  frameborder="0"
                  scrolling="no"
                >
                  <p>Seu navegador não suporta iframes.</p>
                </iframe>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <!-- <v-btn color="blue darken-1" text @click="close">Cancel</v-btn>
          <v-btn color="blue darken-1" text @click="save">Save</v-btn>-->
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- /Modal Dialog Comprovante de Pagamento -->
  </div>
  <!-- end / Detalhes do Pedido  -->
</template>

<script>
import { mapGetters } from "vuex";
import { mask } from "vue-the-mask";
import { ValidationProvider, extend } from "vee-validate";

extend("delivery", value => {
  if (parseFloat(value) >= 0) {
    return true;
  }

  return "Delivery não poder ser menor que zero!";
});

export default {
  components: {
    ValidationProvider
  },

  props: {
    editedItem: {
      type: [Object],
      required: true,
      default: () => {
        return {
          purchaseId: "",
          currency: "",
          value: "",
          siteTax: "",
          total: "",
          calculatedSpread: "",
          orderStatus: "",
          createdAt: "",
          updatedAt: "",
          iof: "",
          calculatedIof: "",
          yearTotalPurchase: "",
          totalPurchaseInCash: "",
          name: "",
          email: "",
          phoneNumber: "",
          status: "",
          comercialTax: "",
          spreadCash: "",
          spreadBank: "",
          payOption: "",
          bankTaxNow: "",
          cashResult: "",
          delivery: "",
          address: {
            _id: "",
            cep: "",
            street: "",
            number: "",
            complement: "",
            city: "",
            state: ""
          },
          vet: "",
          calculatedSpreadRS: "",
          observation: "",
          totalOrderValue: "",
          proofPayment: "",
          thumbProofPayment: ""
        };
      }
    },
    enumOrderStatus: {
      required: true,
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
    ...mapGetters({
      checkActions: "checkActions"
    })
  },

  data() {
    return {
      // Modal de Vinculação
      dialog: false,
      loadingField: false,
      intentionId: "",
      intentionsByEmail: [],
      // Permissões por Action
      updatePurchase: false,
      // Quando não pode ser editado
      isNotEditabled: false,
      // Modal para exibir o Comprovante de Pagamento
      dialogProofPayment: false,
      currentImage: "",
      currentDocument: ""
    };
  },

  watch: {
    editedItem(newValue, oldValue) {
      //console.log("editemItem", oldValue.orderStatus);
      //console.log("editemItem newValue:", newValue.orderStatus);
      if (newValue && newValue.orderStatus) {
        this.checkStatus(newValue.orderStatus);
      }
    }
  },

  mounted() {
    this.initialize();
  },

  methods: {
    initialize() {
      // Verificações de Permissões
      this.updatePurchase = this.checkActions("updatePurchase");
    },

    close() {
      this.dialog = false;
    },

    checkStatus(orderStatus) {
      if (orderStatus) {
        const notAllowed = ["Done", "CanceledByAdmin", "CanceledByCustomer"];
        this.isNotEditabled = notAllowed.includes(orderStatus);
      }
      return false;
    },

    checkProofPayment(editedItem) {
      if (editedItem) {
        return editedItem.proofPayment ? 5 : 7;
      }
      return 7;
    },

    openProofPayment(mimeType, base64) {
      if (base64) {
        this.dialogProofPayment = true;
        // `data:image/png;base64,
        if (mimeType === "application/pdf") {
          // src="data:application/pdf;base64,JVBERi0xLjQKJeL
          this.currentDocument = `data:${mimeType};base64,${base64}`;
        } else {
          this.currentImage = `data:${mimeType};base64,${base64}`;
        }
      }
    },

    detectMimeType(base64) {
      // Algumas Assinaturas Conhecidas
      const signatures = {
        JVBERi0: "application/pdf",
        R0lGODdh: "image/gif",
        R0lGODlh: "image/gif",
        iVBORw0KGgo: "image/png"
      };
      for (let s in signatures) {
        if (base64.indexOf(s) === 0) {
          return signatures[s];
        }
      }
    }
  }
};
</script>

<style lang="css">
.edited-field .v-input__control .v-input__slot .v-text-field__slot input {
  text-align: center !important;
}
.tb-order-details {
  width: 96% !important;
  margin: auto auto;
}
.tb-order-details th,
.tb-order-details td {
  padding: 10px 12px !important;
}
</style>
