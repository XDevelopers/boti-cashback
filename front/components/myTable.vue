<template>
  <v-app>
    <v-card class="card">
      <v-card-title>
        {{data.title}}
        <v-spacer></v-spacer>
        <v-text-field
          v-model="data.search"
          append-icon="fas fa-search"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>
      <v-data-table class="card" :headers="data.headers" :items="data.items" :search="data.search">
        <template v-slot:item.status="{ item }">
          <v-chip :color="getColor(item.status)" dark>{{ item.status }}</v-chip>
        </template>
        <template v-slot:item.firstName="{ item }">{{ combineName(item) }}</template>
        <template v-slot:item.action="{ item }">
          <span title="login">
            <v-icon small class="mr-2" @click="connect(item)">mdi-login</v-icon>
          </span>
        </template>
          <template v-slot:item.connect="{ item }">
            <span title="Request Access">
               <v-icon small class="mr-2" tag="Connect" @click="login(item)">mdi-power-plug</v-icon>
            </span>
          </template>
      </v-data-table>
    </v-card>
  </v-app>
</template>
<script>
import { mapGetters } from "vuex";

export default {
  name: "myTable",
  props: ["data"],

  computed: {
    ...mapGetters(["currentTenant"])
  },

  mounted(){
  },

  methods: {
    connect(item){
      this.$root.$emit("loading");
      var model = this.currentTenant.tenants.filter(m => m.apiKey === item.apiKey);

      this.$store.commit("tenant/selectTenant", model[0]);
      this.$root.$emit("changeTenant");
       this.$axios
          .get(`/tenant/inviteKey?apiKey=${encodeURIComponent(item.apiKey)}&email=${this.currentTenant.email}`)
          .then(res => {
            let url = `${res.data.url}?inviteKey=${res.data.inviteKey}`;
            window.open(url, '_blank');
           this.$root.$emit("loadingDone");
          })
          .catch(e => {
            if (e.response.status === 400 && e.response.data.includes("User was not allowed.")) {
              this.$toast.error("Looks like you don't have permission yet, requesting access...");
            }
            this.$root.$emit("loadingDone");
            return null;
          });
    },
    login(item){
      this.$root.$emit("connect", item);
    },
    getColor(status) {
      if (status == "Accepted") return "green";
      else if (status == "Deleted") return "red";
      else return "orange";
    },
    combineName(item) {
      if (item.hasOwnProperty("firstName") && item.hasOwnProperty("lastName")) {
        return item.firstName + " " + item.lastName;
      }

      return item.firstName;
    }
  }
};
</script>
<style scoped>
.card {
  background-color: #2b3442 !important;
  color: #f1f5f7 !important;
}
.v-card__title {
  color: #f1f5f7;
}
.theme--light.v-data-table thead tr th {
  color: #9ea4ba;
}
</style>