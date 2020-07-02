<template>
  <v-tooltip top color="info">
    <template v-slot:activator="{ on }">
      <v-icon v-on="on" :color="getColorByStatus(status)">mdi {{ status | iconByIntentionStatus }}</v-icon>
    </template>
    <span>{{ status | nameIntentionStatus }}</span>
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
    getColorByStatus(status) {
      if (status) {
        let currentStatus = this.enum.find(s => s.code === status);
        if (currentStatus) {
          return currentStatus.color;
        }
      }
      return "grey darken-4";
    }
  }
};
</script>
