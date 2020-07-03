<template>
  <div class="account-pages my-5 pt-4">
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-md-8 col-lg-6 col-xl-4">
          <div>
            <div class="text-center mb-4 login-header">
              <span>
                <img src="/assets/images/cash/cash_logo.svg" alt height="60" />&nbsp;&nbsp;
                <h2>Eu Revendedor - O Boticário</h2>
              </span>
            </div>
            <!-- Consider refactoring this to its own component -->
            <!-- The 'passes' method allows us to only run our submit hander if the form is valid -->
            <ValidationObserver v-slot="{ invalid }">
              <form id="formLogin" @submint.prevent="login()" @keyup.enter="login()">
                <div class="form-group mb-3">
                  <label for="email">E-mail</label>
                  <ValidationProvider v-slot="{ errors }" name="Email" rules="required|email">
                    <input
                      id="email"
                      type="text"
                      class="form-control"
                      placeholder="Informe seu e-mail"
                      autocomplete="on"
                      v-model="email"
                    />
                    <span class="invalid-feedback d-block">{{ errors[0] }}</span>
                  </ValidationProvider>
                </div>

                <div class="form-group mb-2">
                  <label for="password">Senha</label>
                  <ValidationProvider v-slot="{ errors }" name="Senha" rules="required|min:4">
                    <input
                      id="password"
                      type="password"
                      class="form-control"
                      placeholder="Informe sua Senha"
                      autocomplete="on"
                      v-model="password"
                    />
                    <span class="invalid-feedback d-block">{{ errors[0] }}</span>
                  </ValidationProvider>
                </div>

                <!-- <a
                    href="#"
                    @click="modalResetPassword()"
                    class="text-muted float-right"
                    style="color: #1e4000 !important; font-weight: 500; font-size: 14px;"
                >Esqueceu sua senha?</a>-->
                <v-row>
                  <v-col class="text-start" cols="12" md="6">
                    <div class="form-group mb-3">
                      <div class="custom-control custom-checkbox">
                        <input
                          type="checkbox"
                          class="custom-control-input"
                          id="checkbox-signin"
                          checked
                        />
                        <label class="custom-control-label" for="checkbox-signin">Manter logado</label>
                      </div>
                    </div>
                  </v-col>
                  <v-col class="text-end d-none" cols="12" md="6">
                    <v-btn
                      color="green darken-3"
                      text
                      link
                      @click="modalResetPassword()"
                      class="text-muted float-right forgot-password"
                    >Esqueceu sua senha?</v-btn>
                  </v-col>
                </v-row>

                <div class="form-group text-center mb-3">
                  <button
                    id="btnEntrar"
                    :disabled="invalid || showLoading"
                    @click="login()"
                    type="button"
                    class="btn btn-primary btn-lg width-lg btn-rounded"
                    style="background-color: #1e4000;"
                  >Entrar</button>
                </div>
              </form>
            </ValidationObserver>
          </div>
          <!-- end card -->

          <!-- 
          <div class="row">
            <div class="col-sm-12 text-center">
              <p class="text-muted">
                Don't have an account?
                <a href="/page-register" class="text-dark ml-1">Sign Up</a>
              </p>
            </div>
          </div>
          -->
          <!-- end row -->
        </div>
        <!-- end col -->
      </div>
      <!-- end row -->
    </div>
    <!-- end container -->
    <v-app>
      <v-dialog v-model="dialog" persistent max-width="400px">
        <ValidationObserver ref="observer" v-slot="{ invalid }">
          <v-card>
            <v-card-title>
              <span class="headline">Resetar sua senha!</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-row v-if="messageError">
                  <v-col cols="12" sm="12" md="12">
                    <label>{{ messageError }}</label>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="12" md="12">
                    <VInputWithValidation
                      v-model="cpfToResetPassword"
                      v-mask="['###########']"
                      label="CPF *"
                      placeholder="Informe o CPF"
                      rules="required|max:11|min:3"
                      counter="11"
                    />
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <div class="form-action-buttons text-center justify-center align-center">
                <v-btn color="primary" @click="close" style="text-transform: capitalize;">Fechar</v-btn>
                <v-btn
                  color="primary"
                  @click="resetPassword"
                  :disabled="invalid"
                  class="ml-4"
                  style="text-transform: capitalize;"
                >Enviar nova senha</v-btn>
              </div>
            </v-card-actions>
          </v-card>
        </ValidationObserver>
      </v-dialog>
    </v-app>
    <loading :showLoading="showLoading" message="Autenticando..." />
  </div>
</template>

<script>
import { ValidationObserver, ValidationProvider } from "vee-validate";
import VInputWithValidation from "~/components/inputs/VInputWithValidation";

export default {
  transition: "bounce",

  layout: "empty",

  //middleware: "auth",

  components: {
    ValidationObserver,
    ValidationProvider,
    VInputWithValidation
  },

  data() {
    return {
      email: "",
      password: "",
      //  "Email": "admin@boticario-cashback.com.br",
      //  "Password": "boti-cash2020",
      //  "GrantType": "password"
      showLoading: false,
      errors: [],
      // Modal Reset Password
      cpfToResetPassword: "",
      messageError: "",
      dialog: false
    };
  },

  mounted() {
    this.initialize();
  },

  methods: {
    initialize() {},

    login() {
      try {
        this.showLoading = true;
        this.$auth
          .loginWith("local", {
            data: {
              Email: this.email,
              Password: this.password,
              GrantType: "password"
            }
          })
          .then(response => {
            if (response && response.data && response.data.id) {
              this.$store.commit("setUser", {
                id: response.data.id,
                name: response.data.name,
                role: response.data.role,
                cpf: response.data.cpf
              });
              this.$auth.setUserToken(
                localStorage.getItem("auth._token.local").replace("Bearer ", "")
              );
              this.$router.push("/dashboard");
            } else {
              console.log("ELSE: ??? ", response);
            }
            this.showLoading = false;
          })
          .catch(error => {
            //console.log("Login Promise:", error.response);
            this.displayErrors(error);
            this.showLoading = false;
          });

        //.then(() => this.$toast.success('User set!'));
        //this.$store.commit('tenant/setcpf', this.cpf);
        //this.$router.push("/dashboard");
      } catch (error) {
        console.log(error);
        if (
          error &&
          error.response &&
          error.response.status &&
          error.response.status === 401
        ) {
          this.$core.messageError("Credenciais informadas, não são válidas!");
        }
      } finally {
        this.showLoading = false;
      }
    },

    displayErrors(error) {
      if (
        error &&
        error.response &&
        error.response.data &&
        error.response.status &&
        error.response.status === 400
      ) {
        let errorMessage = error.response.data[0].title;
        this.$core.messageError(errorMessage);
      }
    },

    loginSubmit(e) {
      if (e.keyCode === 13 && !document.getElementById("btnEntrar").disabled) {
        this.login();
      }
    },

    modalResetPassword() {
      this.dialog = true;
      if (this.cpf) {
        this.cpfToResetPassword = this.cpf;
      }
    },

    async resetPassword() {
      try {
        this.showLoading = true;
        //resetPassword?cpf=05617258966
        const response = await this.$adminUserServices.get(
          `resetPassword?cpf=${this.cpfToResetPassword}`
        );
        console.log(response);
        if (response && response.success) {
          this.$toast.success(
            "Dentro de instantes você receberá sua senha temporaria no e-mail cadastrado!"
          );
          this.close();
        } else if (response && response.success == false) {
          this.$toast.error(`${response.message}`);
        }
      } catch (error) {
        console.error("resetPassword", error);
      } finally {
        this.showLoading = false;
      }
    },

    close() {
      this.dialog = false;
      this.cpfToResetPassword = "";
    }
  }
};
</script>

<style>
.bg-empty {
  background: transparent linear-gradient(271deg, #9aff8a 0%, #ccff00 100%) 0%
    0% no-repeat padding-box;
}
.login-header {
  padding: 5px 5px;
  border-radius: 10px;
}
.login-header h2 {
  color: #1e4000 !important;
}
#formLogin label {
  color: #1e4000 !important;
}

.custom-control-input:checked ~ .custom-control-label::before {
  color: #1e4000 !important;
  border-color: #1e4000 !important;
  background-color: #1e4000 !important;
}
.custom-checkbox
  .custom-control-input:indeterminate
  ~ .custom-control-label::before {
  border-color: #1e4000 !important;
  background-color: #1e4000 !important;
}
.custom-control-input:focus:not(:checked) ~ .custom-control-label::before {
  border-color: #3c7f00 !important;
}

.btn-primary {
  color: #fff !important;
  background-color: #3c7f00 !important;
  border-color: #3c7f00 !important;
}
.btn-primary:hover {
  color: #fff !important;
  background-color: #1e4000 !important;
  border-color: #1e4000 !important;
}
.btn-primary:not(:disabled):not(.disabled).active,
.btn-primary:not(:disabled):not(.disabled):active,
.show > .btn-primary.dropdown-toggle {
  color: #fff;
  background-color: #1e4000;
  border-color: #1e4000;
}

.invalid-feedback {
  color: #f7531f;
  font-weight: bold;
  font-size: small;
}

.login-header img {
  height: 44px !important;
}
@media (min-width: 576px) {
  .login-header img {
    height: 44px !important;
  }
}
@media (min-width: 992px) {
  .login-header img {
    height: 52px !important;
  }
}
@media (min-width: 1200px) {
  .login-header img {
    height: 60px !important;
  }
}

.forgot-password {
  margin-top: -8px;
}
.forgot-password span {
  text-transform: capitalize;
  font-size: 14px !important;
  font-weight: 500;
  color: #1e4000 !important;
  letter-spacing: unset !important;
}
</style>
