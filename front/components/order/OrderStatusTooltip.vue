<template>
  <v-tooltip top color="info">
    <template v-slot:activator="{ on }">
      <v-icon v-on="on" :color="getColorByOrderStatus(status)">mdi {{ status | iconByOrderStatus }}</v-icon>
    </template>
    <span>{{ status | nameOrderStatus }}</span>
  </v-tooltip>
</template>

<script>
export default {
  props: {
    status: {
      type: String,
      default: ""
    },
    enum: {
      type: [Function, Array],
      default: null
    }
  },

  data() {
    return {};
  },
  methods: {
    getColorByOrderStatus(orderStatus) {
      if (orderStatus) {
        let currentStatus = this.enum.find(os => os.code === orderStatus);
        if (currentStatus) {
          return currentStatus.color;
        }
      }
      return "grey darken-4";
    }
  }
};
</script>
