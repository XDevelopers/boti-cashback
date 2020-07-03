<template>
  <div class="row">
    <div class="col-xl-3 col-md-3">
      <div class="card" :style="{ backgroundColor: '#546E7A' }">
        <div class="card-body">
          <div class="wigdet-two-content">
            <p class="m-0 text-white text-center text-uppercase">Cashback Acumulado até o momento!</p>
            <h2 class="text-white text-center">
              <span data-plugin="counterup">{{ totalCashback | formatPrice }}</span>
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
  <!--
    <div class="row">
    <div v-for="(item, i) in dashs" :key="i" class="col-xl-3 col-md-3">
      <div v-if="$auth.user" class="card" :style="{ backgroundColor: '#546E7A' }">
        <div class="card-body">
          <div class="wigdet-two-content">
            <p
              class="m-0 text-white text-uppercase"
              title="Intenções de Compra"
            >{{item.description}}</p>
            <h2 class="text-white">
              <span data-plugin="counterup">{{item.value}}</span>
              <i class="mdi mdi-arrow-up text-white font-22"></i>
            </h2>
            <p class="text-white m-0">
              <b></b>
              {{item.sub}}
            </p>
          </div>
        </div>
      </div>
    </div>
    <loading :showLoading="showLoading" />
  </div>
  -->
</template>

<script>
import { mapGetters } from "vuex";

export default {
  data() {
    return {
      dashs: [],
      showLoading: false,
      colors: [
        "#008FFB",
        "#FEB019",
        "#FF4560",
        "#775DD0",
        "#3F51B5",
        "#03A9F4",
        "#4CAF50",
        "#FF9800",
        "#546E7A",
        "#D4526E",
        "#F86624",
        "#EA3546",
        "#662E9B"
      ],
      totalCashback: ""
    };
  },

  computed: {
    ...mapGetters({
      currentUser: "getUser"
    })
  },

  mounted() {
    this.initialize();
  },

  methods: {
    initialize() {
      console.log("initialize - Dashboard");
      this.getAccumulatedCashback();
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