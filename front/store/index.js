/*
 **
 ** Vuex -> See https://nuxtjs.org/guide/vuex-store
 ** Vuex Examples -> https://nuxtjs.org/examples/vuex-store
 **
 */
export const state = () => ({
    errors: [],
    title: "",
    user: null
});

/**
 * To call the mutations use -> this.$store.commit('MUTATIONS', payload)
 */
export const mutations = {

    // changeSomething (state, something) {
    //   state.something = something
    // }
    setError(state, error) {
        state.errors.push(error);
    },

    setTitle(state, title) {
        //console.log(`setTitle: ${title}`);
        state.title = title;
        state.pages.title = title;
    },

    setUser(state, userData) {
        console.log("Store setUser", userData);
        //state.auth.user = userData;
        state.user = userData;
    },

    clearUserData(state) {
        state.auth.user = null;
    }
};

/**
 * To call the getters use -> this.$store.state.isAuthenticated
 */
export const getters = {
    isAuthenticated(state) {
        return state.auth.loggedIn;
    },

    loggedInUser(state) {
        return state.auth.user;
    },

    getUser(state) {
        return state.user;
    },

    // Current Page
    currentPage(state) {
        return state.pages.title;
    },

    enumOrderStatus(state) {
        return state.order.enumOrderStatus;
    }
};