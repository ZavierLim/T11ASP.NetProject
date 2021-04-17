//HG Changes here
//var addedProduct = [];

window.onload = function () {
    let button = document.getElementsByClassName("btn btn-primary img_description");
    for (var i = 0; i < button.length; i++) {
        button[i].onclick = function () {
            
            updateCounter();
        }
        
    }
}

function updateCounter() {
    //let isDuplicate = false;
    let elem = event.currentTarget;
    let productId = elem.getAttribute("id");
    let counterElem = document.getElementById("lblCartCount");
    
    counterElem.textContent++;
/*        for (var i = 0; i < addedProduct.length; i++) {
            window.console.log(addedProduct[i]);
            if (productId === addedProduct[i]) {    
                isDuplicate = true;
                break;
            }
    }

    if (!isDuplicate) {
        counterElem.textContent++;
        addedProduct.push(productId);
    }*/

    //window.console.log(productId);
    

    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/Cart/UpdateCartFromList");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    xhr.onreadystatechange = function () {

        if (this.readyState === XMLHttpRequest.DONE) {
            if (this.status !== 200)

                return;

            let data = JSON.parse(this.responseText);

            if (data.isOkay === true) {
                //unameOutput.textContent = "NO SUCH USER!";
                //elem.setAttribute("class", "form-control is-invalid");
                
            } 

        }
    }

    let data = {
        "productID": productId,
        "cartCount": parseInt(counterElem.textContent) || 0
    }

    xhr.send(JSON.stringify(data));
}