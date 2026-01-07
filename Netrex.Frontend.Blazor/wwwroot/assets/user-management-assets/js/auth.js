function togglePassword() {
    const pass = document.getElementById("password");
    const eye = document.getElementById("eyeIcon");

    if (pass.type === "password") {
        pass.type = "text";
        eye.src = "assets/user-management-assets/icons/monkey-password-show.png";
    } else {
        pass.type = "password";
        eye.src = "assets/user-management-assets/icons/monkey-password-hide.png";
    }
}