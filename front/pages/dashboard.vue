<template>
  <div class="content">
    <!-- Área de Mensagens -->
    <alert v-if="msg" :message="msg" />
    <!-- end/ Área de Mensagens -->

    <dashboard />

    <!-- end container-fluid -->

    <loading :showLoading="showLoading" />
  </div>
  <!-- </div> -->
</template>

<script>
import { mapGetters } from "vuex";
import Dashboard from "~/components/dashboard";

export default {
  meta: {
    title: "Dashboard"
  },

  middleware: ["auth", "page-agent"],

  components: {
    Dashboard
  },

  data() {
    return {
      title: this.$store.state.title,
      // Msg -> Para exibir qualquer mensagem para o usuário!
      msg: "",
      id: 0,
      name: "",
      createdAt: "",
      updatedAt: "",
      scheduleHistory: "",
      lastUpdated: {
        schedule: {
          fileName: ""
        }
      },
      showLoading: false,
      list: [],
      tenants: []
    };
  },

  computed: {
    ...mapGetters({
      loggedInUser: "loggedInUser",
      states: "generics/states"
    })
  },

  head() {
    // Set Meta Tags for this Page
    title: this.$store.state.title;
  },

  mounted() {
    this.title = this.$store.state.title;
    this.initialize();
  },

  methods: {
    initialize() {
      console.log("initialize");
    }
  }
};
</script>
