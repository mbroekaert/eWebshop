function SendDataToView() {
    const dataToSend = { CartItems: JSON.parse(localStorage.getItem('basket')) };
    const url = '/Cart/GetBasketData';

    // Post action
    fetch(url, {
        method:'POST',
        headers: {
            'Content-Type':'application/json'
        },
        body: JSON.stringify(dataToSend)
    })
        .then(response => response.json())
        .then(data => {
            console.log('Data sent successfully!');
            window.location.href='https://localhost:7276/Cart/Index'
           
        })
        .catch(error => {
            console.error('Error sending data to server:', error);
        });
}
