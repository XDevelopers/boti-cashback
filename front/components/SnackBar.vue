<template>
  <v-snackbar v-model="snackbar" bottom right :color="status" timeout="5000">
    {{ text }}
    <v-btn dark text @click="snackbar = false">Close</v-btn>
  </v-snackbar>
</template>

<script>
export default {
  props: ["text", "status", "snackbar"]
};
</script>
