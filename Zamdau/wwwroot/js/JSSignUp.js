
let currentStep = 0;

function showNextInput() {
    const fields = document.querySelectorAll('.input-field');
    const currentField = fields[currentStep];
    const nextStep = currentStep + 1;

    // Valida o campo atual
    const currentInput = currentField.querySelector('input');
    if (!currentInput.checkValidity()) {
        currentInput.reportValidity();
        return;
    }

    // Avança para o próximo campo
    if (nextStep < fields.length) {
        // Mostrar o próximo campo
        const nextField = fields[nextStep];
        nextField.classList.remove('hidden');

        // Verifica se é o último campo
        if (nextStep === fields.length - 1) {
            // Se for o último campo, oculta o botão "Continuar" e mostra o botão "Enviar"
            document.getElementById('continue-button').style.display = 'none';
            document.getElementById('submit-button').style.display = 'block';
        }

        currentStep = nextStep;
    }
}

function validateForm() {
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirm-password').value;

    if (password !== confirmPassword) {
        showAlert('Passwords and password confirmation do not match.');
        return false;
    }

    return true;
}

function togglePassword() {
    const passwordField = document.getElementById('password');
    const togglePassword = document.getElementById('toggle-password');
    passwordField.type = togglePassword.checked ? 'text' : 'password';
}

function toggleConfirmPassword() {
    const confirmPasswordField = document.getElementById('confirm-password');
    const toggleConfirmPassword = document.getElementById('toggle-confirm-password');
    confirmPasswordField.type = toggleConfirmPassword.checked ? 'text' : 'password';
}
//------------------------------------------------------------------------------------

function showAlert(message) {
    const alertMessage = document.getElementById('alert-message');
    const modal = document.getElementById('custom-alert-modal');
    alertMessage.textContent = message;
    modal.style.display = "block";
}

function closeAlert() {
    const modal = document.getElementById('custom-alert-modal');
    modal.style.display = "none";
}

window.onclick = function (event) {
    const modal = document.getElementById('custom-alert-modal');
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

