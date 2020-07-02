<template>
  <v-card class="mb-3" style="box-shadow: unset;">
    <!-- Imagens dos Documentos -->

    <v-row justify="center" class="text-center">
      <table class="tb-img-customers mx-4">
        <thead>
          <tr>
            <th>Tipo de Documento</th>
            <th>Miniatura</th>
            <th>Upload de Arquivos</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(doc, i) in defaultDocuments" :key="`doc-${i}`">
            <td>{{ switcher(doc.documentType) }}</td>
            <td style=" width: 10%">
              <a
                v-if="doc.thumbnail"
                @click="openDoc(`data:${doc.imageType};base64,${doc.docImage}`)"
              >
                <v-img
                  v-if="doc.thumbnail"
                  :src="`data:${doc.imageType};base64,${doc.thumbnail}`"
                  aspect-ratio="1"
                  contain
                  class="green lighten-2"
                  style="background-size: contain;"
                />
              </a>
              <a v-if="!doc.thumbnail && doc.docImage" @click="openPDFFile(doc)">
                <img src="/assets/images/pdf-icon.png" alt="PDF Document" />
              </a>
            </td>
            <td>
              <div style="width: 70%; margin: auto auto;">
                <v-file-upload
                  :label="switcher(doc.documentType)"
                  :documentType="doc.documentType"
                  :userId="customerId"
                  :upload="uploadDocument"
                  :fileSelected="fileSelected"
                />
              </div>
            </td>
          </tr>
        </tbody>
      </table>

      <v-dialog v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
        <v-card>
          <v-toolbar dark color="primary">
            <v-toolbar-title>Tamanho Original</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items>
              <v-btn icon dark @click="dialog = false">
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
        <v-btn color="primary" class="mr-5 mb-2" @click="previousStep(stepperIndex)">Anterior</v-btn>
        <v-btn color="primary" class="mb-2" @click="nextStep(stepperIndex);">Continuar</v-btn>
      </div>
    </v-card-actions>
    <!-- end / Imagens dos Documentos -->

    <loading :showLoading="showLoading" />
  </v-card>
</template>

<script>
import { mapGetters } from "vuex";
import _ from "lodash";
import VFileUpload from "~/components/inputs/VFileUpload";

export default {
  components: {
    VFileUpload
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
      // Objeto das Imagens
      documents: [],
      defaultDocuments: [
        {
          documentType: "idFront"
        },
        {
          documentType: "idVerse"
        },
        {
          documentType: "proofAddress"
        },
        {
          documentType: "incomeTax"
        }
      ],
      document: {
        documentType: "",
        thumbnail: "",
        docImage: "",
        imageType: ""
      },
      currentImage: "",
      currentDocument: "",
      dialog: false,
      // Objeto Customer
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
      getDocumentImages: false,
      getDocuments: false
    };
  },

  watch: {
    customerId(newValue, oldValue) {
      if (newValue) {
        this.getDocumentsByCustomerId(newValue);
      }
    },
    dialog(val) {
      val || this.closeDialog();
    }
  },

  computed: {
    ...mapGetters({
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

      if (
        this.customerId &&
        this.getDocuments &&
        this.getDocumentsByCustomerId
      ) {
        this.getDocumentsByCustomerId(this.customerId);
      }
    },

    async getDocumentsByCustomerId(userId) {
      this.showLoading = true;
      try {
        const response = await this.$store.dispatch(
          "documents/getDocuments",
          userId
        );
        if (response) {
          let dataBaseReturn = response;
          this.documents = _.orderBy(dataBaseReturn, ["documentType"], ["asc"]); // Use Lodash to sort array by "documentType"
          //console.log("getDocumentsByCustomerId", this.documents);
          if (this.documents && this.documents.length >= 4) {
            this.defaultDocuments = this.documents;
          } else if (this.documents && this.documents.length < 4) {
            let docTypes = this.defaultDocuments.map(d => {
              return d.documentType;
            });
            //console.log("documentsTypes", docTypes);
            this.defaultDocuments.map((doc, i) => {
              let document = this.documents.find(
                d => d.documentType === doc.documentType
              );
              if (
                document &&
                document.documentType &&
                docTypes.includes(document.documentType)
              ) {
                doc.docImage = document.docImage;
                doc.imageType = document.imageType;
                doc.thumbnail = document.thumbnail;
              } else {
                doc.docImage = null;
                doc.imageType = null;
                doc.thumbnail = null;
              }
            });
            //console.log("defaultDocuments", this.defaultDocuments);
          }
        }
      } catch (error) {
        console.error("getDocumentsByCustomerId", error);
      } finally {
        this.showLoading = false;
      }
    },

    switcher(documentType) {
      if (documentType) {
        switch (documentType) {
          case "idFront":
            return "RG, Habilitação ou Passaporte - ( Frente )";
            break;
          case "idVerse":
            return "RG, Habilitação ou Passaporte - ( Verso )";
            break;
          case "proofAddress":
            return "Comprovante de Residência";
            break;
          case "incomeTax":
            return "Comprovante de Imposto de Renda";
            break;
        }
      }
    },

    openDoc(base64) {
      this.dialog = true;
      this.currentImage = base64;
    },

    closeDialog() {
      this.currentImage = "";
      this.currentDocument = "";
      this.dialog = false;
    },

    openPDFFile(doc) {
      if (doc && doc.docImage) {
        this.dialog = true;
        const filePath = `data:${doc.imageType};base64,${doc.docImage}`;
        const blob = this.base64ToBlob(doc.docImage);
        this.showFile(blob);
      }
    },

    base64ToBlob(base64, mimeType) {
      const byteString = atob(base64);
      var ab = new ArrayBuffer(byteString.length);
      var ia = new Uint8Array(ab);

      for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
      }
      return new Blob([ab], { type: mimeType });
    },

    showFile(blob) {
      // It is necessary to create a new blob object with mime-type explicitly set
      // otherwise only Chrome works like it should
      const newBlob = new Blob([blob], { type: "application/pdf" });

      // IE doesn't allow using a blob object directly as link href
      // instead it is necessary to use msSaveOrOpenBlob
      if (window.navigator && window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveOrOpenBlob(newBlob);
        return;
      }

      // For other browsers:
      // Create a link pointing to the ObjectURL containing the blob.
      const data = window.URL.createObjectURL(newBlob);
      const link = document.createElement("a");
      link.href = data;
      this.currentDocument = link.href;
    },

    checkPermissions() {
      this.getDocumentImages = this.checkActions("getDocumentImages");
      this.getDocuments = this.checkActions("getDocuments");
    },

    //
    // doc { documentType: "",base64File: "", fileType: "" }
    //
    fileSelected(doc) {
      //console.log("fileSelected", doc);
      // if (doc && doc.base64File) {
      //   let index = this.defaultDocuments.findIndex(d => d.documentType === doc.documentType);
      //   let edited = this.defaultDocuments[index];
      //   if (edited) {
      //     edited.thumbnail = doc.base64File.split(",")[1];
      //     edited.docImage = doc.base64File.split(",")[1];
      //     edited.imageType = doc.fileType;
      //     this.defaultDocuments[index] = Object.assign({}, edited);
      //     // if (!this.documents.map(d => d.documentType).includes(doc.documentType)){
      //     //   this.documents.push(edited);
      //     // }
      //   }
      // }
    },

    uploadDocument() {
      //console.log("uploadDocument");
      this.initialize();
      //this.nextStep(this.stepperIndex);
    }
  }
};
</script>

<style>
.tb-img-customers {
  width: 90%;
  border: solid red 1px;
}
.tb-img-customers thead tr th {
  border: solid 1px #ededed;
  padding: 10px 10px;
}
.tb-img-customers thead tr th:first-child {
  text-align: left;
}
.tb-img-customers tbody tr td {
  border: solid 1px #ededed;
  padding: 10px 5px;
}
.tb-img-customers tbody tr td:first-child {
  text-align: left;
}
</style>