// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function userSignIn() {
  console.log("JEEEESUS");
  var signInEmail = document.getElementById("email").value;
  var singInPassword = document.getElementById("password").value;

  console.log(signInEmail);
  console.log(singInPassword);

  var userToSignIn = {
    UserEmail: signInEmail,
    Password: singInPassword,
  };

  localStorage.setItem("signedAttempt", JSON.stringify(userToSignIn));
}

function registerNewUser() {
  var newEmail = document.getElementById("newEmail").value;
  var newPass = document.getElementById("newPassword").value;
  var newPassVeri = document.getElementById("newPasswordVeri").value;
  console.log(newPass);
  console.log(newPassVeri);

  if (newPass === newPassVeri) {
    var newUser = {
      UserEmail: newEmail,
      Password: newPass,
    };

    localStorage.setItem("newRegisteredUser", JSON.stringify(newUser));
  } else {
    console.log("Password Dont Match!");
  }
}
