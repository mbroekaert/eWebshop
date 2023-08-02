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
    }

    
};
//export default shoppingBasket;

function addToCart(productId, productPrice, quantity) {
    const priceAsNumber = convertPriceToNumber(productPrice);
    shoppingBasket.addItem(productId, priceAsNumber, quantity);
    console.log('product added to cart');
    updateBasketItemCount();
    updateBasketTotalPrice();
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
function UpdateBasketOnPageLoad() {
    updateBasketItemCount();
    updateItemQuantities();
    updateBasketTotalPrice();
    
}
document.addEventListener('DOMContentLoaded', UpdateBasketOnPageLoad);