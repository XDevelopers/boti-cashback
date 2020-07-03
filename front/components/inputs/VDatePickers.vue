<template>
  <div style="display: flex;">
    <!-- DatePicker -->
    <v-menu
      v-model="menuStartDate"
      :close-on-content-click="false"
      transition="scale-transition"
      offset-y
      max-width="290px"
      min-width="290px"
    >
      <template v-slot:activator="{ on }">
        <VInputWithValidation
          v-model="dataInicial"
          v-mask="'##/##/####'"
          label="Data Atualização Inicial"
          rules="required"
          prepend-icon="event"
          class="mt-5 ml-6 mr-6"
          v-on="on"
          @blur="startDate = parseDate(dataInicial)"
        />
      </template>
      <v-date-picker
        v-model="startDate"
        locale="pt-BR"
        scrollable
        no-title
        @input="menuStartDate = false"
      />
    </v-menu>
    <v-menu
      v-model="menuEndDate"
      :close-on-content-click="false"
      transition="scale-transition"
      offset-y
      max-width="290px"
      min-width="290px"
    >
      <template v-slot:activator="{ on }">
        <VInputWithValidation
          v-model="dataFinal"
          v-mask="'##/##/####'"
          label="Data Atualização Final"
          rules="required"
          prepend-icon="event"
          class="mt-5 mr-4"
          v-on="on"
          @blur="endDate = parseDate(dataFinal)"
        />
      </template>
      <v-date-picker
        v-model="endDate"
        locale="pt-BR"
        scrollable
        no-title
        @input="menuEndDate = false"
      />
    </v-menu>
    <v-btn color="primary" class="ml-6 mr-4 mt-5" @click="search()">Pesquisar</v-btn>
    <!-- end / DatePicker -->
  </div>
</template>

<script>
import VInputWithValidation from "~/components/inputs/VInputWithValidation";

export default {
  components: {
    VInputWithValidation
  },

  props: {
    searchByDate: {
      type: [Function],
      default: () => {}
    }
  },

  data() {
    return {
      // Datepicker
      dataInicial: "",
      dataFinal: "",
      menuStartDate: false,
      menuEndDate: false,
      startDate: "", //new Date().toISOString().substr(0, 10),
      endDate: "" //new Date().toISOString().substr(0, 10)
    };
  },

  watch: {
    startDate(val) {
      this.dataInicial = this.formatDate(this.startDate);
    },
    endDate(val) {
      this.dataFinal = this.formatDate(this.endDate);
    }
  },

  created() {
    let date = new Date();
    let firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    this.startDate = this.$moment()
      .subtract(30, "days")
      .format(); //firstDay.toISOString().substr(0, 10);
    this.endDate = new Date().toISOString().substr(0, 10);
  },

  mounted() {
    this.initialize();
  },

  methods: {
    initialize() {
      let startDate = this.$moment()
        //.startOf("month")
        .subtract(30, "days")
        .format("DD/MM/YYYY");
      let endDate = this.$moment()
        .endOf("month")
        .format("DD/MM/YYYY");

      this.dataInicial = startDate;
      this.dataFinal = this.$moment().format("DD/MM/YYYY");
    },

    formatDate(date) {
      return this.$core.formatDate(date);
    },

    parseDate(date) {
      return this.$core.parseDate(date);
    },

    search() {
      this.searchByDate(this.dataInicial, this.dataFinal);
    }
  }
};
</script>