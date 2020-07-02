<template>
  <!-- Listagem de Logs do Pedido selecionado -->
  <v-dialog v-model="showLogs" scrollable max-width="1290px" class="logs">
    <v-card>
      <v-card-text style="height: 700px;">
        <v-data-table
          class="card"
          sort-by="firstName"
          :headers="logs.headers"
          :items="logs.items"
          :search="logs.search"
        >
          <!-- Colunas customizadas -->
          <template v-slot:item.createdAt="{ item }">{{ item.createdAt | formatDatetime }}</template>
          <template v-slot:top>
            <v-toolbar flat color="#2b3442">
              <v-toolbar-title>{{ logs.title }}</v-toolbar-title>
              <v-divider class="mx-4" inset vertical></v-divider>
              <v-text-field
                class="mx-4"
                v-model="logs.search"
                append-icon="fas fa-search"
                label="Pesquisar"
                single-line
                hide-details
              ></v-text-field>
              <v-spacer></v-spacer>
            </v-toolbar>
          </template>
          <template v-slot:no-data>Não há histórico para esse Pedido!</template>
        </v-data-table>
      </v-card-text>
      <v-divider></v-divider>
      <v-card-actions>
        <div class="form-action-buttons text-center justify-center align-center">
          <v-btn color="primary" @click="hideLogs">Fechar</v-btn>
        </div>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <!-- end / Listagem de Logs do Pedido selecionado -->
</template>

<script>
export default {
  props: {
    logs: {
      type: [Object],
      required: true,
      default: null
    },
    showLogs: {
      type: [Boolean],
      default: false
    },
    hideLogs: {
      type: [Function]
    }
  }
};
</script>