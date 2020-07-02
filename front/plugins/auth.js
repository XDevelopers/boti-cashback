export default async function({ $auth, next, store }) {
  console.log("auth middleware");
  //console.log(store);
  let user = $auth.state.user;
  if(user){
    // let the user in
  } else {
    next("/login")
  }

  // $auth.onError((error, name, endpoint) => {
  //   console.log("auth/onError");
  //   console.error(name, error)
  // }),

  // $auth.onRedirect((to, from) => {
  //   console.log("auth/onRedirect");
  //   console.error(to)
  //   // you can optionally change `to` by returning a new value
  // })
}