<template>
  <v-app id="wrapper" app-data="true">
    <div class="navbar-custom">
      <!-- LOGO -->
      <div class="logo-box">
        <nuxt-link to="/dashboard" class="logo text-center">
          <span class="logo-lg">
            <img src="/assets/images/cash/cash_logo.svg" alt="Logo " height="32" />
          </span>
          <span class="logo-sm">
            <img
              src="https://res.cloudinary.com/beleza-na-web/image/upload/f_png,w_72,h_72,fl_progressive,q_auto:eco/v1/blz/assets-store/0.0.221/images/store/47/icon.svg"
              alt="Logo"
            />
          </span>
        </nuxt-link>
      </div>
      <!-- / LOGO -->

      <!-- Menu Sandwich -->
      <ul class="list-unstyled topnav-menu topnav-menu-left m-0">
        <li>
          <button class="button-menu-mobile waves-effect waves-light" @click="enlargeMenu">
            <i class="fe-menu"></i>
          </button>
        </li>
      </ul>
      <!-- / Menu Sandwich -->

      <!-- Menu Other Items -->
      <ul class="list-unstyled topnav-menu float-right mb-0">
        <li class="d-none d-sm-block">
          <div class="app-search">
            <div class="app-search-box">
              <div class="input-group"></div>
            </div>
          </div>
        </li>
        <!-- dropdown-user -->
        <li class="dropdown notification-list">
          <a
            class="nav-link dropdown-toggle nav-user mr-0 waves-effect waves-light"
            href="#"
            id="navbarDropdownUser"
            role="button"
            data-toggle="dropdown"
          >
            <img src="../static/assets/images/user.jpg" alt="user-image" class="rounded-circle" />
          </a>
          <div
            class="dropdown-menu dropdown-menu-right profile-dropdown"
            aria-labelledby="navbarDropdownUser"
          >
            <!-- item -->
            <div class="dropdown-header noti-title">
              <h6 class="text-overflow m-0">Meus Dados</h6>
            </div>

            <!-- <a @click="showChangePassword" class="dropdown-item notify-item">
              <i class="fe-shield"></i>
              <span>Alterar Senha</span>
            </a>-->

            <!-- item -->
            <nuxt-link
              v-if="listConfiguration"
              to="/configuracoes"
              class="dropdown-item notify-item"
            >
              <i class="fe-settings"></i>
              <span>Configurações</span>
            </nuxt-link>

            <div class="dropdown-divider"></div>

            <!-- item -->
            <a @click="logout()" class="dropdown-item notify-item">
              <i class="fe-log-out"></i>
              <span>Logout</span>
            </a>
          </div>
        </li>
        <!-- dropdown-settings -->
        <!-- 
        <li class="dropdown notification-list">
          <a href="javascript:void(0);" class="nav-link right-bar-toggle waves-effect waves-light">
            <i class="fe-settings noti-icon"></i>
          </a>
        </li> 
        -->
      </ul>
      <!-- / Menu Other Items -->
    </div>

    <div class="left-side-menu">
      <div class="slimscroll-menu">
        <!--- Sidemenu -->
        <div id="sidebar-menu">
          <ul class="metismenu" id="side-menu">
            <li class="menu-title">Menu Principal</li>
            <li>
              <nuxt-link to="/dashboard">
                <i class="fe-airplay"></i>
                <span>Dashboard</span>
              </nuxt-link>
            </li>
            <li>
              <nuxt-link to="/orders">
                <i class="fe-dollar-sign"></i>
                <span>Compras</span>
              </nuxt-link>
            </li>
            <li>
              <nuxt-link to="/resellers">
                <i class="fe-users"></i>
                <span>Revendedores</span>
              </nuxt-link>
            </li>
          </ul>
        </div>
        <!-- End Sidebar -->
        <div class="clearfix"></div>
      </div>
      <!-- Sidebar -left -->
    </div>
    <div class="content-page">
      <div class="container-fluid">
        <!-- start page title -->
        <div class="row">
          <div class="col-12">
            <div class="page-title-box">
              <div class="page-title-right">
                <ol class="breadcrumb m-0">
                  <li class="breadcrumb-item">
                    <n-link to="/dashboard">Eu Revendedor - Admin</n-link>
                  </li>
                  <li class="breadcrumb-item">
                    <a href="javascript: void(0);">{{ currentPage }}</a>
                  </li>
                </ol>
              </div>
              <h4 class="page-title">{{ currentPage }}</h4>
            </div>
          </div>
        </div>
        <!-- end page title -->
      </div>
      <nuxt />
      <footer class="footer">
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-6">
              Todos os direitos &copy; 2020 O Boticário
              <a
                href="https://www.boticario.com.br/"
              >Website</a>
            </div>
            <div class="col-md-6">
              <div class="text-md-right footer-links d-none d-sm-block">
                <a href="https://www.boticario.com.br/">About Us</a>
                <a href="mailto:develop@boti-cashback.com">Help</a>
                <a href="mailto:info@boti-cashback.com">Contact Us</a>
              </div>
            </div>
          </div>
        </div>
      </footer>
    </div>
  </v-app>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "default",
  components: {},

  computed: {
    ...mapGetters([
      "isAuthenticated",
      "loggedInUser",
      "currentTenant",
      "currentPage"
    ])
  },

  data() {
    return {
      title: this.$route.name,
      avatar: "",
      tenant: "",
      tenants: [],
      // Change Password Dialog
      showDialog: false,
      // Actions (Verificar se usuário pode Carregar itens do Menu)
      listAccount: false,
      listClients: false, // Clientes
      listConfiguration: false, // Configurações
      listCurrency: false,
      listDefinitions: false,
      listInsurance: false, // Seguro
      listIntention: false, // Intenções
      listPurchase: false, // Pedidos de Moeda, Pedidos de VTM
      listRegion: false,
      listRoles: false, // Roles
      listShopRules: false,
      listUser: false // Usuários
    };
  },

  mounted() {
    this.sideMenu();
    //this.checkActionOnMenu();
  },

  methods: {
    async logout() {
      try {
        await this.$auth.logout();
        this.$axios.setToken(false);
        this.$store.commit("clearUserData");
        this.$router.push("/");
        setTimeout(() => {
          this.$router.push("/");
          this.$router.push("/login");
        }, 1000);
      } catch (error) {
        console.log("logout", error);
      }
    },

    enlargeMenu() {
      if (window.mobilecheck()) {
        if (document.body.classList.contains("sidebar-enable")) {
          document.body.classList.remove("sidebar-enable");
        } else {
          document.body.classList.add("sidebar-enable");
        }
      } else {
        if (document.body.classList.contains("enlarged")) {
          document.body.classList.remove("enlarged");
        } else {
          document.body.classList.add("enlarged");
        }
      }
    },

    sideMenu() {
      const metismenu = document.getElementById("side-menu");
      metismenu.querySelectorAll(".menu-arrow").forEach((item, i) => {
        let link = item.parentElement;
        link.addEventListener("click", e => {
          this.openChildren(link);
        });
      });
    },

    openChildren(link) {
      const selectedLink = link; //a
      const selectedLi = link.parentElement; //li
      const unorderedList = selectedLi.querySelector("ul"); //ul

      selectedLi.classList.toggle("mm-active");
      let ariaExpanded =
        selectedLink.attributes["aria-expanded"].specified !== true;
      selectedLink.setAttribute("aria-expanded", ariaExpanded);
      unorderedList.setAttribute("aria-expanded", !ariaExpanded);
      unorderedList.classList.toggle("mm-show");
    },

    toggleDropdown(e) {
      if (process.env.NODE_ENV == "development") return;
      const listItem = document.getElementsByClassName("notification-list")[0];
      const link = document.getElementById("navbarDropdownUser");
      const rightMenu = document.querySelector(
        "div[aria-labelledby='navbarDropdownUser']"
      );
      let toggle = link.getAttribute("aria-expanded");
      if (toggle === "true") {
        link.setAttribute("aria-expanded", "false");
        listItem.classList.remove("show");
        rightMenu.classList.remove("show");
      } else {
        link.setAttribute("aria-expanded", "true");
        listItem.classList.add("show");
        rightMenu.classList.add("show");
      }
    },

    showChangePassword() {
      if (this.showDialog === false) {
        this.showDialog = true;
      }
    },

    closeModal(hideDialog) {
      if (hideDialog) {
        this.showDialog = false;
      }
    }
  }
};
</script>

<style>
body {
  background-color: #242c37 !important;
}
.tenant-select {
  background-color: #232a36;
}
.metismenu {
  cursor: pointer !important;
}
.navbar-custom {
  background: transparent linear-gradient(271deg, #ccff00 0%, #464646 100%) 0%
    0% no-repeat padding-box !important;
  color: #464646 !important;
}
.navbar-custom ul li,
.navbar-custom .waves-light,
.navbar-custom .logo-box .logo .logo-lg-text-light {
  color: #464646 !important;
}
/*
.enlarged .button-menu-mobile {
  margin-left: 170px;
}
*/
</style>
