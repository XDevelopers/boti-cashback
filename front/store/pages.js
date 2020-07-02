export const state = () => ({
    title: 'Correparti Admin Tool'
  });

  export const mutations = {
    
    setTitle(state, title) {
      console.log("pages/setTitle");
      state.title = title;
    },
    
  };
  