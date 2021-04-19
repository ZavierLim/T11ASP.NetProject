window.onload = function () {
    let uname = document.getElementById("username");
    let password = document.getElementById("password");
    let unameOutput = document.getElementById("unameOutput");

    uname.onblur = function () {
        checkUsername(uname.value);
    }
    uname.addEventListener("focus", () => unameOutput.textContent = "");

}

function checkUsername(username) {
    let elem = event.currentTarget;
    
   
    let xhr = new XMLHttpRequest();

    xhr.open("POST", "/Account/IsUserOkay");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
    
    xhr.onreadystatechange = function () {
        
        if (this.readyState === XMLHttpRequest.DONE) {
            if (this.status !== 200)
                
                return;

            let data = JSON.parse(this.responseText);

            if (data.isOkay === false) {

                unameOutput.textContent = "NO SUCH USER!";
                elem.setAttribute("class", "form-control is-invalid");
            } else {
                elem.setAttribute("class", "form-control is-valid");
            }

        }
    }

    let data = {
        "username": username
    };
    console.log(JSON.stringify(data));
    xhr.send(JSON.stringify(data));
}