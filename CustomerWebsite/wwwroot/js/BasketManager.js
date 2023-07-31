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

        // Remove the item from the basket or set the quantity to 0 if not found
        currentBasket[productId] = currentBasket[productId]- quantity;

        // Save the updated basket data back to localStorage
        localStorage.setItem('basket', JSON.stringify(currentBasket));

        // Update the basket total price
        this.updateTotalPrice(productPrice, -1);
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
    itemPriceElement.textContent = shoppingBasket.getBasketTotalPrice();
};

//function updateItemQuantity(productId) {
//    const itemQuantity = document.getElementById('itemQuantity');
//    itemQuantity.textContent = shoppingBasket.getProductQuantity(productId)
//};

function convertPriceToNumber(formattedPrice) {
    // Remove any existing commas and replace them with dots
    const priceWithDots = formattedPrice.replace(',', '.');
    // Parse the result to a floating-point number
    return parseFloat(priceWithDots);
};
function UpdateBasketOnPageLoad() {
    updateBasketItemCount();
    updateBasketTotalPrice();
}
document.addEventListener('DOMContentLoaded', UpdateBasketOnPageLoad);