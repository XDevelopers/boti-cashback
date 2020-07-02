<template>
  <v-text-field v-model="innerValue" v-bind="$attrs" v-on="$listeners"></v-text-field>
</template>

<script>

export default {
  components: {},
  props: {
    // must be included in props
    value: {
      type: null,
      default: ""
    }
  },
  data: () => ({
    innerValue: ""
  }),
  watch: {
    // Handles internal model changes.
    innerValue (newVal) {
      this.$emit("input", newVal);
    },
    // Handles external model changes.
    value (newVal) {
      this.innerValue = newVal;
    }
  },
  created () {
    if (this.value) {
      this.innerValue = this.value;
    }
  }
};
</script>
