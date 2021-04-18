
var oldValue = "";
var totalPrice = "";
window.onload = function () {
    var elements = document.getElementsByClassName("quantityInput");

    for (var i = 0; i < elements.length; i++) {

        //elements[i].addEventListener("input", updateCart(elemValue, elements[i]));
        elements[i].onfocus = function () {
            getOldValue();
        }
        elements[i].onchange = function () {
            updateCart();
        }
    }
}

function updateCart() {
    let elem = event.currentTarget;
    //current textfield value is elem.value
    let prodId = elem.getAttribute("id");
    let totalPrice = document.getElementById("totalPrice");
    let counter = document.getElementById("lblCartCount");
    //window.console.log(totalPrice.textContent);
    //price calculation update on each item
    let itemSubtotal = document.getElementById("itemSubtotal" + prodId);
    totalPrice.textContent = parseFloat(totalPrice.textContent) - parseFloat(itemSubtotal.textContent);
    //window.console.log(totalPrice.textContent);

    let unitPrice = parseFloat(itemSubtotal.textContent) / parseFloat(oldValue);
    itemSubtotal.textContent = unitPrice * parseFloat(elem.value);
    totalPrice.textContent = parseFloat(totalPrice.textContent) + parseFloat(itemSubtotal.textContent);

    //Update Cart Counter
    counter.textContent = parseInt(counter.textContent) - parseInt(oldValue) + parseInt(elem.value);

    //AJAX request to Controller
    let xhr = new XMLHttpRequest();

    xhr.open("POST", "/Cart/UpdateCartFromCart");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    xhr.onreadystatechange = function () {

        if (this.readyState === XMLHttpRequest.DONE) {
            if (this.status !== 200)
                return;

            let data = JSON.parse(this.responseText);

            if (data.isOkay === true) {
                //unameOutput.textContent = "NO SUCH USER!";
                //elem.setAttribute("class", "form-control is-invalid");
                console.log("success");
            }

        }
    }

    let data = {
        "productId": prodId,
        "cartCount": parseInt(counter.textContent),
        "quantity": parseInt(elem.value) || 0
    }

    xhr.send(JSON.stringify(data));

}

function getOldValue() {
    let elem = event.currentTarget;
    oldValue = elem.value;
}
