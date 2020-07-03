<template>
  <div class="content">
    <!-- Área de Mensagens -->
    <alert v-if="msg" :message="msg" />
    <!-- end/ Área de Mensagens -->

    <div class="row">
      <div class="col-xl-3 col-md-3">
        <div class="card" :style="{ backgroundColor: '#546E7A' }">
          <div class="card-body">
            <div class="wigdet-two-content">
              <p
                class="m-0 text-white text-center text-uppercase"
              >Cashback Acumulado até o momento!</p>
              <h2 class="text-white text-center">
                <span data-plugin="counterup">{{ totalCashback | formatPrice }}</span>
                <i class="mdi mdi-arrow-up text-white font-30"></i>
              </h2>
              <p class="text-white text-center m-0">
                <b>Wow!</b>
                <br />Continue assim.
              </p>
            </div>
          </div>
        </div>
      </div>

      <div class="col-xl-3 col-md-3">
        <div class="card" :style="{ backgroundColor: '#546E7A' }">
          <div class="card-body">
            <div class="wigdet-two-content">
              <p class="m-0 text-white text-center text-uppercase">Escolha uma das opções no Menu!</p>
              <h2 class="text-white text-center">
                <span data-plugin="counterup"></span>
                <i class="mdi mdi-alert-octagram-outline text-white font-30"></i>
              </h2>
              <p class="text-white text-center m-0">
                <b>Aguarde!</b>
                <br />Novas Funcionalidades estão chegando.
              </p>
            </div>
          </div>
        </div>
      </div>
      <loading :showLoading="showLoading" />
    </div>

    <!-- end container-fluid -->

    <loading :showLoading="showLoading" />
  </div>
  <!-- </div> -->
</template>

<script>
import { mapGetters } from "vuex";

export default {
  meta: {
    title: "Dashboard"
  },

  middleware: ["auth", "page-agent"],

  components: {},

  data() {
    return {
      title: this.$store.state.title,
      // Msg -> Para exibir qualquer mensagem para o usuário!
      msg: "",
      showLoading: false,
      totalCashback: ""
    };
  },

  computed: {
    ...mapGetters({
      currentUser: "getUser"
    })
  },

  head() {
    // Set Meta Tags for this Page
    title: this.$store.state.title;
  },

  async mounted() {
    this.title = this.$store.state.title;
    await this.initialize();
  },

  methods: {
    async initialize() {
      //console.log("initialize - Dashboard");
      await this.getAccumulatedCashback();
    },
    async getAccumulatedCashback() {
      this.showLoading = true;
      try {
        if (this.currentUser && this.currentUser.cpf) {
          const response = await this.$cashbackServices.get(
            `${this.currentUser.cpf}/acumulado`
          );
          if (response) {
            //console.log("getAccumulatedCashback", response);
            this.totalCashback = response;
          }
        }
      } catch (error) {
        console.log("getAccumulatedCashback", error);
      } finally {
        this.showLoading = false;
      }
    }
  }
};
</script>
