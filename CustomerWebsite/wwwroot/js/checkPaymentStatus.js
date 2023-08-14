function checkPaymentStatus(paymentPayid) {
    fetch(`https://localhost:7060/api/billing/confirm/getpaymentbypayidfromquery?paymentPayid=${paymentPayid}`)
        .then(response => response.json())
        .then(data => {
            if (data.paymentStatus === 9) {
                document.getElementById('ContentToBeUpdateOrderConfirmed').innerHTML =
                    '<h2 class="text-primary" id="MainTextToUpdate">Thank you for your order !</h2>';
                localStorage.removeItem('basket');
                localStorage.removeItem('totalBasketPrice');
            }
        })
        .catch(error => {
            console.error('Error fetching payment status:', error);
        });
}

const urlParams = new URLSearchParams(window.location.search);
const paymentPayid = urlParams.get('hostedCheckoutId');

if (paymentPayid) {
    
    checkPaymentStatus(paymentPayid);
    setInterval(() => {
        checkPaymentStatus(paymentPayid);
    }, 3000); 
}