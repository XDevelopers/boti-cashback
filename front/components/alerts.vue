<template>
  <!-- Área de Mensagens -->
  <div
    v-if="message"
    class="alert alert-danger alert-dismissible bg-white text-danger fade show"
    role="alert"
  >
    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
      <span aria-hidden="true">×</span>
    </button>
    <i class="mdi mdi-information-outline"></i>
    <strong>{{ message }}</strong>
  </div>
  <!-- end/ Área de Mensagens -->
</template>
<script>
export default {
  name: "alert",
  props: {
    message: {
      type: String,
      default: ""
    }
  }
};
</script>