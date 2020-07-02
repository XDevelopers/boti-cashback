export default function ({ route, store, redirect }) {
  console.info("NotAuthenticated Middleware");
  // If the user is not authenticated
  if (store.state.auth.loggedIn) {
    return redirect('/');
  }
}
