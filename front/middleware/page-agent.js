//
// Função para pegar o Nome de cada página
//
export default ({ route, store }) => {
    //console.info("Page-Agent Middleware");
    // Take the last value (latest route child)
    const constTitle = 'meta.title was not defined';
    const title = route.meta.reduce((title, meta) => meta.title || title, constTitle);
    store.commit('setTitle', title);
}