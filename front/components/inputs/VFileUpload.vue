<template>
  <v-app dense id="file-upload">
    <label for="fileUpload">
      <h5>{{ label }}</h5>
    </label>
    <v-file-input
      v-model="files"
      :rules="rules"
      :show-size="1024"
      accept="image/png, image/jpeg, .pdf"
      color="green darken-3"
      dense
      outlined
      placeholder="Selecione um arquivo!"
      prepend-icon="mdi-paperclip"
      @click:clear="clear"
      v-on:change="selectedFile"
    >
      <template v-slot:selection="{ index, text }">
        <v-chip v-if="index < 2" color="green darken 3" dark label small>{{ text }}</v-chip>
        <span
          v-else-if="index === 2"
          class="overline grey--text text--darken-3 mx-2"
        >+{{ files.length - 2 }} File(s)</span>
      </template>
    </v-file-input>
    <v-btn class="upload-button" color="indigo" :disabled="invalid" @click="uploadFile()">
      Enviar Arquivo
      <v-icon right color="white">cloud_upload</v-icon>
    </v-btn>
    <loading :showLoading="showLoading" />
  </v-app>
</template>

<script>
export default {
  name: "FileUpload",
  props: {
    label: {
      type: String,
      default: ""
    },
    documentType: {
      type: String,
      default: ""
    },
    userId: {
      type: String,
      default: ""
    },
    upload: {
      type: Function,
      default: () => {}
    },
    fileSelected: {
      type: Function,
      default: () => {}
    }
  },

  data: () => ({
    showLoading: false,
    invalid: true,
    files: [],
    file: "",
    errorSize: "O arquivo deve ser menor que 2.5 MB!",
    rules: [
      value =>
        //!value || value.size < 10000000 || "O arquivo deve ser menor que 10 MB!"
        // Formula: B = MB x 1048576
        !value || value.size <= 2621440 || "O arquivo deve ser menor que 2.5 MB!"
    ]
  }),

  watch: {
    documentType(newValue, oldValue) {
      let documentType = newValue;
      if (documentType) {
        switch (documentType) {
          case "idFront":
            return "Escolha uma Imagem que represente um:";
            break;
          case "idVerse":
            return "Escolha uma Imagem que represente o Verso de um:";
            break;
          case "proofAddress":
            return "Escolha uma Image que represente um:";
            break;
          case "incomeTax":
            return "Escolha um .pdf que represente um:";
            break;
          default:
            return "Documento";
            break;
        }
      }
    }
  },

  methods: {
    async selectedFile(e) {
      //console.log("selectedFile", e);
      if (e) {
        try {
          if (e.size > 2621440){
            this.$toast.error(this.errorSize);
            this.invalid = true;
            return;
          }
          //console.log("selectedFile", this.files);
          this.invalid = false;
          this.file = await this.$core.getBase64(e);
          if (this.file) {
            this.fileSelected({
              documentType: this.documentType,
              base64File: this.file,
              fileType: this.base64MimeType(this.file)
            });
          }
        } catch (error) {
          console.error("selectedFile", error);
          this.invalid = true;
        }
      }
    },

    async uploadFile() {
      if (!this.file) {
        this.error = "Você precisa selecionar um arquivo primeiro!";
        this.$toast.error(this.error);
        this.invalid = true;
        return;
      }
      this.showLoading = true;
      try {
        let document = {
          userId: this.userId,
          documentType: this.documentType,
          base64Image: this.file
        };
        //console.log("uploadFile REQUEST:", document);
        let response = await this.$store.dispatch(
          "documents/uploadDocument",
          document
        );
        //console.log("uploadFile:", response);
        if (response && response.data && response.data.success) {
          this.$toast.success("Operação efetuada com sucesso!");
          this.upload();
          this.invalid = true;
        }
      } catch (error) {
        console.error("uploadFile", error);
        this.invalid = false;
      } finally {
        this.showLoading = false;
      }
    },

    base64MimeType(encoded) {
      let result = null;

      if (typeof encoded !== "string") {
        return result;
      }

      let mime = encoded.match(/data:([a-zA-Z0-9]+\/[a-zA-Z0-9-.+]+).*,.*/);
      if (mime && mime.length) {
        result = mime[1];
      }

      return result;
    },

    clear() {
      console.log(this.files);
      this.files = [];
      this.invalid = true;
    }
  }
};
</script>

<style lang="css">
  #file-upload {
    background-color: #2b3442 !important;
    display: block !important;
    min-height: unset !important;
  }
  #file-upload div:first-child {
    min-height: unset !important;
  }
</style>