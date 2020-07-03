export default function({
    $auth,
    store,
    redirect,
    next
}) {
    console.info("Guest Middleware");
    if (store.state.auth.loggedIn) {
        return redirect('/')
    } else {
        return next("/");
    }
}