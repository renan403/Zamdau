function focusZip() {
    document.getElementById('zip').focus();
}

function lookupAddress() {
    var zipCode = document.getElementById('zip').value;
    var country = document.getElementById('country').value;

    // Verifique se o CEP foi inserido
    if (!zipCode) {
        showAlertNegative("Zip is empty.");
        document.getElementById('zip').focus();
        return;
    }

    // Montar a URL para a API com base no país
    var apiUrl = '';
    if (country === 'Brazil') {
        apiUrl = `https://viacep.com.br/ws/${zipCode}/json/`;
    } else if (country === 'United States') {
        apiUrl = `https://api.zippopotam.us/us/${zipCode}`;
    } else {
        alert("Select a country.");
        document.getElementById('country').focus();
        return;
    }

    // Chamada à API
    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (country === 'Brazil') {
                document.getElementById('street').value = data.logradouro || '';
                document.getElementById('state').value = data.estado || '';
            } else if (country === 'United States') {
                document.getElementById('street').value = data.places[0]['place name'] || '';
                document.getElementById('state').value = data.places[0]['state'] || '';
            }
        })
        .catch(error => {
            document.getElementById('street').value = "";
            document.getElementById('state').value = "";
            showAlertNegative("Error fetching address data, please check the zipcode.");

            console.error('There was a problem with the fetch operation:', error);
        });
}
