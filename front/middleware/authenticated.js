export default function({ route, store, redirect }) {
    console.info("Authenticaded Middleware");
    // If the user is not authenticated
    if (!store.state.auth.loggedIn) {
        return redirect('/login');
    }
}