const env = require('dotenv').config({ path: `.env.${process.env.NUXT_ENV_SCOPE}` });
import INLINE_ELEMENTS from "eslint-plugin-vue/lib/utils/inline-non-void-elements";
export default {
    /*
     ** Headers of the page
     */
    head: {
        title: 'Boticário Cashback Admin Tool',
        meta: [
            { charset: 'utf-8' },
            { name: "description", content: process.env.npm_package_description || "Boticário Cashback Admin Tool", hid: "description" },
            { name: 'viewport', content: 'width=device-width, initial-scale=1, shrink-to-fit=no, user-scalable=no, minimal-ui' },
            { name: 'apple-mobile-web-app-capable', content: 'yes' },
        ],
        link: [
            { rel: 'icon', href: 'https://res.cloudinary.com/beleza-na-web/image/upload/f_png,w_16,h_16,fl_progressive,q_auto:eco/v1/blz/assets-store/0.0.221/images/store/47/icon.svg' },
            { rel: 'stylesheet', href: '/assets/css/bootstrap.min.css' },
            { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Material+Icons' },
            { rel: 'apple-touch-icon', sizes: "180x180", href: 'https://res.cloudinary.com/beleza-na-web/image/upload/f_png,w_152,h_152,fl_progressive,q_auto:eco/v1/blz/assets-store/0.0.221/images/store/47/icon.svg' },
            { rel: 'stylesheet', href: '/assets/css/icons.min.css' },
            { rel: 'stylesheet', href: '/assets/css/app.min.css' }
        ],
        script: [
            { src: "https://code.jquery.com/jquery-3.3.1.slim.min.js", body: true },
            { src: "https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js", body: true },
            { src: "https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js", body: true },
            //{ src: "/assets/js/vendor.js", body: true },
            { src: "/assets/js/app.js", body: true },
        ]
    },
    /*
     ** Customize the progress-bar color
     */
    //loading: "~/components/loading.vue",
    loading: {
        color: '#1E4000',
        height: '5px'
    },
    /*
     ** Global CSS
     */
    css: [
        "~/assets/main.css"
    ],
    /*
     ** Build configuration
     */
    build: {
        // Add exception
        transpile: [
            // Doc: https://logaretm.github.io/vee-validate/api/rules.html
            "vee-validate/dist/rules"
        ],
        // // HTML
        // html: {
        //     minify: {
        //       collapseWhitespace: true, 
        //       removeComments: true, 
        //     },
        // },
        // Run ESLint on save
        extend(config, { isDev, isClient }) {
            if (isDev && isClient) {
                config.module.rules.push({
                    enforce: 'pre',
                    test: /\.(js|vue)$/,
                    loader: 'eslint-loader',
                    exclude: /(node_modules)/
                })
            }
        }

    },
    /*
     ** Nuxt.js dev-modules
     */
    buildModules: [
        // Doc: https://github.com/nuxt-community/eslint-module
        '@nuxtjs/eslint-module',
        // With options
        ['@nuxtjs/moment', {
            /* module options */
            defaultLocale: 'pt-br',
            locales: ['pt-br']
        }],
        // Doc: https://github.com/nuxt-community/dotenv-module
        ['@nuxtjs/dotenv', { filename: `.env.${process.env.NUXT_ENV_SCOPE}` }],

    ],
    /*
     ** Nuxt.js modules
     */
    modules: [
        '@nuxtjs/dotenv',
        '@nuxtjs/axios',
        '@nuxtjs/auth',
        '@nuxtjs/toast', [
            'nuxt-sweetalert2',
            {
                confirmButtonColor: '#2e7d32',
                cancelButtonColor: '#ff7674'
            }
        ],
        // See: https://www.npmjs.com/package/vue-currency-filter
        // See: https://dm4t2.github.io/vue-currency-input/playground/
        // ['vue-currency-filter/nuxt', {
        //     globalOptions: {
        //         symbol: 'R$',
        //         currency: 'BRL',
        //         precision: 4,
        //         allowNegative: false,
        //         thousandsSeparator: '.',
        //         fractionCount: 4,
        //         fractionSeparator: ',',
        //         symbolPosition: 'front',
        //         symbolSpacing: true
        //     },
        //     componentName:'Currency'
        // }],
    ],
    /*
     ** Axios module configuration
     ** See https://axios.nuxtjs.org/options
     */
    axios: {
        //-----: 
        baseURL: process.env.BASE_API_URL,
        redirectError: {
            401: '/login',
            //404: '/notfound'
        }
    },
    /*
     ** Plugins to load before mounting the App
     */
    plugins: [
        '~plugins/axios.js',
        '~plugins/core-helper.js',
        '~plugins/vuetify.js',
        '~plugins/vee-validate',
        '~plugins/vue-the-mask',
        // Services (Classe Genérica para a maioria dos métodos)
        '~services/api-services.js',
        // Services (Todo os Serviços injetados)
        '~plugins/globalServices',
        // Global Components
        '~plugins/globalComponents.js',
        // Filters
        '~plugins/globalFilters',
        // Directives
        '~plugins/globalDirectives'
    ],
    /*
     ** Auth module configuration
     ** See https://auth.nuxtjs.org/
     */
    auth: {
        token: {
            prefix: "_token."
        },
        localStorage: {
            prefix: "auth."
        },
        strategies: {
            local: {
                endpoints: {
                    login: {
                        url: "/api/login",
                        method: "post",
                        propertyName: "access_token"
                    },
                    user: false,
                    //user: { url: '/admin/me', method: 'get', propertyName: '' },
                    logout: false,
                },
            }
        },
        // redirect: {
        //     login: '/',
        //     logout: '/',
        //     home: '/'
        // }
    },
    /*
     ** Vai pegar as variáveis de Ambiente .env.{development} || .env.{staging} || .env.{production}
     */
    env: env.parsed,
    /*
     ** Display a Toast message!
     ** See https://github.com/nuxt-community/modules/tree/master/packages/toast
     ** See https://shakee93.github.io/vue-toasted/
     */
    toast: {
        position: 'bottom-right',
        theme: 'bubble',
        duration: 5000,
        register: [ // Register custom toasts
            {
                name: 'my-error',
                message: 'Ops... Aconteceu um erro inexperado!',
                options: {
                    type: 'error'
                }
            }
        ]
    },
    /*
     ** Router
     ** See https://nuxtjs.org/guide/routing
     */
    router: {
        // Run the middleware/page-agent.js on every page
        //middleware: "guest"
    },

    /*
     ** ESLINT configuration
     */
    eslint: {
        root: true,
        env: {
            browser: true,
            node: true
        },
        parserOptions: {
            parser: 'babel-eslint'
        },
        extends: [
            '@nuxtjs',
            'plugin:nuxt/recommended'
        ],
        // add your custom rules here
        rules: {
            "vue/singleline-html-element-content-newline": ["off", {
                "ignoreWhenNoAttributes": true,
                "ignoreWhenEmpty": true,
                "ignores": ["pre", "textarea", ...INLINE_ELEMENTS]
            }],
            "no-console": "off",
            "quotes": "off",
            "semi": "off",
            "space-before-function-paren": "off",
            "vue/mustache-interpolation-spacing": "off",
            "vue/html-self-closing": "off",
            "vue/html-closing-bracket-spacing": "off",
            "vue/multiline-html-element-content-newline": "off",
            "vue/no-template-shadow": "off",
            "vue/max-attributes-per-line": "off",
            "no-trailing-spaces": "off",
            "spaced-comment": "off",
            "eol-last": "off",
            "arrow-parens": "off",
            "import/namespace": "off",
            "indent": "off",

        }
    },
}