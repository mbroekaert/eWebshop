function checkRadioButtons() {
    const shippingRadio = document.querySelector('input[name="selectShipping"]');
    const billingRadio = document.querySelector('input[name="selectBilling"]');
    const proceedButton = document.getElementById('proceedButton');
    const billingAddressInput = document.querySelector('input[name="billingAddressId"]');
    const shippingAddressInput = document.querySelector('input[name="shippingAddressId"]');

    // Populate hidden fields

    billingAddressInput.value = billingRadio.value;
    shippingAddressInput.value = shippingRadio.value;

    // Enable the "Proceed with payment" button if either radio button is selected
    if (shippingRadio.checked && billingRadio.checked) {
        proceedButton.disabled = false;
    } else {
        proceedButton.disabled = true;
    }
}