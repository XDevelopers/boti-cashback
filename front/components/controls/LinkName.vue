<template>
  <a :href="getMailTo(email, name)" target="_blank">{{ text }}</a>
</template>

<script>
export default {
  name: "link-name",
  props: {
    text: {
      type: String,
      default: ""
    },
    name: {
      type: String,
      default: ""
    },
    email: {
      type: String,
      default: ""
    }
  },

  data() {
    return {};
  },

  methods: {
    getMailTo(email, name) {
      if (email && name) {
        let message = `Olá ${name}, nós somos do Boticário | Eu Revendedor.`;
        let mailTo = `mailto:${email}?subject=O Boticário | Eu Revendedor&body=${message},`;
        return mailTo;
      }
    }
  }
};
</script>
