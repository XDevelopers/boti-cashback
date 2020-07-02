<template>
  <v-app class="layout-empty">
    <v-container>
      <v-card>
        <v-card-title>
          <h1 v-if="error.statusCode === 404">{{ pageNotFound }}</h1>
          <h1 v-else>{{ otherError }}</h1>
        </v-card-title>

        <h2>{{error}}</h2>

        <NuxtLink to="/" class="mr-4">Home page</NuxtLink>
        <a class="mr-4" @click="back()">Voltar</a>
      </v-card>
    </v-container>
  </v-app>
</template>

<script>
export default {
  layout: "empty",
  props: {
    error: {
      type: Object,
      default: null
    }
    // backgroundEmpty: {
    //   type: String,
    //   default: require("@/static/assets/images/BG-login.png")
    // }
  },
  data() {
    return {
      pageNotFound: "404 Not Found",
      otherError: "An error occurred"
    };
  },
  methods: {
    // backgroundImage() {
    //   return `background: url('${this.backgroundEmpty}') no-repeat top left;`;
    // }
    back() {
      this.$router.go(-1);
    }
  },
  head() {
    const title =
      this.error.statusCode === 404 ? this.pageNotFound : this.otherError;
    return {
      title
    };
  }
};
</script>

<style scoped>
h1 {
  font-size: 20px;
}
</style>
