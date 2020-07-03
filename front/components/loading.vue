<template lang="html">
  <div class="w-100 text-center overlay" v-if="showLoading">
    <div class="spinner"></div>
    <h4>{{ message }}</h4>
  </div>
</template>

<script>
export default {
  name: "loading",

  props: ["message", "showLoading"],

  data: () => ({
    loading: false
  }),

  methods: {
    // start() {
    //   this.loading = true;
    // },
    // finish() {
    //   this.loading = false;
    // }
  }
};
</script>
<style scoped>
.spinner {
  position: fixed;
  left: 50%;
  top: 40%;
  height: 60px;
  width: 60px;
  margin: 0px auto;
  -webkit-animation: rotation 0.6s infinite linear;
  -moz-animation: rotation 0.6s infinite linear;
  -o-animation: rotation 0.6s infinite linear;
  animation: rotation 0.6s infinite linear;
  border-left: 6px solid rgba(255, 255, 255, 0.15);
  border-right: 6px solid rgba(255, 255, 255, 0.15);
  border-bottom: 6px solid rgba(255, 255, 255, 0.15);
  border-top: 6px solid rgba(255, 255, 255, 0.8);
  border-radius: 100%;
  /* customizado para o Overlay */
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
}

@-webkit-keyframes rotation {
  from {
    -webkit-transform: rotate(0deg);
  }
  to {
    -webkit-transform: rotate(359deg);
  }
}
@-moz-keyframes rotation {
  from {
    -moz-transform: rotate(0deg);
  }
  to {
    -moz-transform: rotate(359deg);
  }
}
@-o-keyframes rotation {
  from {
    -o-transform: rotate(0deg);
  }
  to {
    -o-transform: rotate(359deg);
  }
}
@keyframes rotation {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(359deg);
  }
}
.overlay h4 {
  position: relative;
  top: 55%;
  left: 2%;
}
.overlay {
  position: fixed; /* Sit on top of the page content */
  display: block; /* Hidden by default */
  width: 100%; /* Full width (cover the whole page) */
  height: 100%; /* Full height (cover the whole page) */
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.7); /* Black background with opacity */
  z-index: 2; /* Specify a stack order in case you're using a different order for other elements */
  cursor: pointer; /* Add a pointer on hover */
}
</style>
