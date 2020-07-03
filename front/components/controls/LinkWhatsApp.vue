<template>
  <a
    v-if="cellPhoneNumber"
    :href="getWhatApp(cellPhoneNumber, name)"
    rel="noopener"
    target="_blank"
  >
    <v-chip @click="() => {}" class="ma-0" color="green" text-color="white" small>
      <v-icon>mdi mdi-whatsapp</v-icon>
      &nbsp;
      {{ text }}
    </v-chip>
  </a>
</template>

<script>
export default {
  props: {
    text: {
      type: String,
      default: ""
    },
    name: {
      type: String,
      default: ""
    },
    cellPhoneNumber: {
      type: String,
      default: ""
    }
  },

  data() {
    return {};
  },
  methods: {
    getWhatApp(cellPhoneNumber, name) {
      if (cellPhoneNumber) {
        let fixedNumber = cellPhoneNumber
          .toString()
          .replace("(", "")
          .replace(")", "")
          .replace("-", "")
          .replace(/\s/g, "");
        let message = `Olá ${name}, nós somos da Boticário | Eu Revendedor.`;
        return `https://wa.me/55${fixedNumber}?text=${encodeURI(message)}`;
      }
    }
  }
};
</script>