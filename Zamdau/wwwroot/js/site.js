function showAlertPositive(message) {
    const alertBox = document.getElementById('alert-box');

    // Atualiza a mensagem do alerta, se necessário
    alertBox.textContent = message;

    // Adiciona a classe para exibir o alerta
    alertBox.classList.add('show');
    alertBox.classList.add('success');

    // Remove o alerta após 3 segundos
    setTimeout(() => {
        alertBox.classList.remove('show');

    }, 3000);
}
function showAlertNegative(message) {
    const alertBox = document.getElementById('alert-box');

    // Atualiza a mensagem do alerta, se necessário
    alertBox.textContent = message;

    // Adiciona a classe para exibir o alerta
    alertBox.classList.add('show');
    alertBox.classList.add('error');

    // Remove o alerta após 3 segundos
    setTimeout(() => {
        alertBox.classList.remove('show');
    }, 3000);
}

function showAlert(title, text) {
    
    Swal.fire({
        icon: 'warning', //  'success', 'error', 'warning', 'info', 'question'
        title: title,
        text: text,
        confirmButtonText: 'Ok',
        background: 'black', // Gradiente do fundo
        color: '#fff' // Cor do texto

    });
}


    const editButton = document.getElementById('editButton');
    const saveButton = document.getElementById('saveButton');
    const cancelButton = document.getElementById('cancelButton');

    // Alterna entre os modos de visualização e edição
    editButton.addEventListener('click', () => toggleEditMode(true));
    cancelButton.addEventListener('click', () => toggleEditMode(false));

    function toggleEditMode(isEditing) {
        const inputs = document.querySelectorAll('#profileForm input, #profileForm select');
    const displays = document.querySelectorAll('#profileForm p');

        inputs.forEach(input => input.classList.toggle('d-none', !isEditing));
        displays.forEach(display => display.classList.toggle('d-none', isEditing));

    editButton.classList.toggle('d-none', isEditing);
    saveButton.classList.toggle('d-none', !isEditing);
    cancelButton.classList.toggle('d-none', !isEditing);
    }

