const shoppingBasket = {
    addItem(productId, productPrice, quantity) {
        const currentBasket = JSON.parse(localStorage.getItem('basket')) || {};

        // Update the quantity or add the new item to the basket
        currentBasket[productId] = Math.max((currentBasket[productId] || 0) + quantity, 0);

        // Save the updated basket data back to localStorage
        localStorage.setItem('basket', JSON.stringify(currentBasket));

        // Update the basket total price
        this.updateTotalPrice(productPrice, quantity);
    },

    removeItem(productId, productPrice, quantity) {
        const currentBasket = JSON.parse(localStorage.getItem('basket')) || {};

        // Calculate the new quantity for the item (never less than 0)
        const newQuantity = Math.max((currentBasket[productId] || 0) - quantity, 0);

        // Only update the basket if the new quantity is different from the old quantity
        if (newQuantity !== currentBasket[productId]) {
            // Update the quantity in the basket
            currentBasket[productId] = newQuantity;

            // Save the updated basket data back to localStorage
            localStorage.setItem('basket', JSON.stringify(currentBasket));

            // Update the basket total price if the new quantity is greater than 0
            if (newQuantity > -1) {
                this.updateTotalPrice(productPrice, -1);
            }
        }
    },

    getBasketItemCount() {
        currentBasket = JSON.parse(localStorage.getItem('basket')) || {};
        return Object.values(currentBasket).reduce((total, quantity) => total + quantity, 0);
    },

    getBasketTotalPrice() {
        const totalBasketPrice = parseFloat(localStorage.getItem('totalBasketPrice')) || 0;
        return totalBasketPrice;
    },

    updateTotalPrice(productPrice, quantity) {
        const totalBasketPrice = (this.getBasketTotalPrice() + productPrice * quantity).toFixed(2);
        localStorage.setItem('totalBasketPrice', totalBasketPrice);
    },

    getProductQuantity(productId) {
        const productQuantity = currentBasket[productId];
        return productQuantity;
    },

    emptyBasket() {
        localStorage.removeItem('basket');
        localStorage.removeItem('totalBasketPrice');
    }

    
};
//export default shoppingBasket;

async function addToCart(productId, productPrice, quantity) {
    const priceAsNumber = convertPriceToNumber(productPrice);

    // Check available stock
    const response = await fetch(`/api/product/stock/${productId}`);
    const availableStock = await response.json();
    // Get quantity from basket
    const quantityInBasket = shoppingBasket.getProductQuantity(productId);
    // Calculate total quantity 
    const totalQuantity = quantityInBasket + quantity;

    if (availableStock >= totalQuantity) {
        shoppingBasket.addItem(productId, priceAsNumber, quantity);
        console.log('product added to cart');
        updateBasketItemCount();
        updateBasketTotalPrice();
    }
    else {
        window.alert('Sorry! We dont have enough stock of this product to answer your demand');
    }
};

function removeFromCart(productId, productPrice, quantity) {
    const priceAsNumber = convertPriceToNumber(productPrice);
    shoppingBasket.removeItem(productId, priceAsNumber, quantity);
    console.log('product removed from cart');
    updateBasketItemCount();
    updateBasketTotalPrice();
};

function updateBasketItemCount() {
    const itemCountElement = document.getElementById('basketItemCount');
    itemCountElement.textContent = shoppingBasket.getBasketItemCount();
};

function updateBasketTotalPrice() {
    const itemPriceElement = document.getElementById('basketItemPrice');
    const itemPriceElementBody = document.getElementById('basketItemPriceBody');
    itemPriceElement.textContent = shoppingBasket.getBasketTotalPrice();
    itemPriceElementBody.textContent = shoppingBasket.getBasketTotalPrice();
};



function updateItemQuantities() {
    const quantities = JSON.parse(localStorage.getItem('basket')) || {};

    for (const productId in quantities) {
        const quantityElement = document.getElementById(`itemQuantity_${productId}`);
        if (quantityElement) {
            quantityElement.textContent = quantities[productId];
        }
    }
}


function convertPriceToNumber(formattedPrice) {
    // Remove any existing commas and replace them with dots
    const priceWithDots = formattedPrice.replace(',', '.');
    // Parse the result to a floating-point number
    return parseFloat(priceWithDots);
};

function populateHiddenQuantityFields() {
    // Select all hidden input fields
    const quantityInputs = document.querySelectorAll('input[name="quantity[]"]');
    // loop and populate
    quantityInputs.forEach(populateUniqueHiddenQuantityField);
}

function populateUniqueHiddenQuantityField(input) {
    const productId = input.getAttribute('id').split('_')[1];
    const quantity = shoppingBasket.getProductQuantity(productId);
    input.value = quantity;
}

function UpdateBasketOnPageLoad() {
    updateBasketItemCount();
    updateItemQuantities();
    updateBasketTotalPrice();
    populateHiddenQuantityFields();
    
};
function EmptyBasket() {
    shoppingBasket.emptyBasket();
}

document.addEventListener('DOMContentLoaded', UpdateBasketOnPageLoad);

