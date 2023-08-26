$(document).ready(function () {
    $("#LoginBtn").click(function () {

        if (!ValidateLoginForm())
            return false;
            
        var token = $("input[name='__RequestVerificationToken']").val();
        $('#LoginBtn').attr('disabled', 'disabled');
        const login = {
            "Id": 0,
            "UserName": $("input[name='UserName']").val(),
            "Password": $("input[name='Password']").val(),
            RequestVerificationToken: token
        };
        fetch("/Login/Login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token
            },
            body: JSON.stringify(login)
        })
            .then(
                response => response.json()
            )
            .then(data => {
                $('#LoginBtn').removeAttr('disabled');
                if (data.response.returnCode == 200) {
                    window.location.href = '/Dashboard'
                }
                else
                    sweetalert("Error", "Please enter valid credentials !");
            })
            .catch(error => {
                $('#LoginBtn').removeAttr('disabled');
                sweetalert("Error", "Something went wrong !")
            });
    });
})

function ValidateLoginForm() {
    var uname = document.getElementById("UserName").value;
    var pass = document.getElementById("Password").value;
    document.getElementById("UserNameError").innerHTML = "";
    document.getElementById("PasswordError").innerHTML = "";
    var isValid = true;
    if (uname.trim() === "") {
        document.getElementById("UserNameError").innerHTML = "Please enter username.";
        isValid = false;
    }
    if (pass.trim() === "") {
        document.getElementById("PasswordError").innerHTML = "Please enter password.";
        isValid = false;
    }
    return isValid;
}