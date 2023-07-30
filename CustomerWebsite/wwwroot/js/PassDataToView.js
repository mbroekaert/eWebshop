function SendDataToView() {
    var dataToSend = localStorage.getItem('basket');
    fetch('/Cart/Index', {
        method:'POST',
        headers: {
            'Content-Type':'application/json'
        },
        body: dataToSend
    })
        .then(response => response.json())
        .then(data => {
            console.log('Data sent successfully!');
        })
        .catch(error => {
            console.error('Error sending data to server:', error);
        });
}
