document.addEventListener('DOMContentLoaded', function () {
    const cartItemsContainer = document.querySelector('.cart-items');
    const selectAllCheckbox = document.querySelector('.select-all');
    const itemCheckboxes = document.querySelectorAll('.select-item');

    // Atualiza o subtotal, taxa e total geral com base nos itens selecionados
    function updateSummary() {
        let subtotal = 0;
        const cartItems = document.querySelectorAll('.cart-item');

        cartItems.forEach(item => {
            const checkbox = item.querySelector('.select-item');
            if (checkbox.checked) { // Calcula apenas se o item estiver selecionado
                const totalPriceElement = item.querySelector('.total-price');
                const priceElement = item.querySelector('.price');
                const quantityInput = item.querySelector('.input-cart');

                const price = parseFloat(priceElement.getAttribute('data-price'));
                const quantity = parseInt(quantityInput.value, 10);

                const totalPrice = price * quantity;
                totalPriceElement.textContent = `$${totalPrice.toFixed(2)}`;
                subtotal += totalPrice;
                
            }
        });

        const tax = subtotal * 0.10; // Exemplo de cálculo de taxa (10%)
        const total = subtotal + tax;

        // Atualiza os valores gerais no HTML
        document.querySelector('.subtotal').textContent = `Subtotal: $${subtotal.toFixed(2)}`;
        document.querySelector('.tax').textContent = `Tax: $${tax.toFixed(2)}`;
        document.querySelector('.total').textContent = `Total: $${total.toFixed(2)}`;

        // Atualiza os campos hidden do formulário
        document.querySelector('#Subtotal').value = subtotal.toFixed(2);
        document.querySelector('[name="Tax"]').value = tax.toFixed(2);
        document.querySelector('[name="Total"]').value = total.toFixed(2);
        
    }

    // Marcar/desmarcar todos os itens com o checkbox principal
    selectAllCheckbox.addEventListener('change', function () {
        const isChecked = this.checked;
        itemCheckboxes.forEach(checkbox => {
            checkbox.checked = isChecked;
        });
        updateSummary(); // Atualiza o resumo do carrinho
    });

    // Atualiza o estado do checkbox principal ao alterar qualquer item
    itemCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function () {
            const allChecked = Array.from(itemCheckboxes).every(cb => cb.checked);
            selectAllCheckbox.checked = allChecked; // Sincroniza com o estado geral
            updateSummary(); // Atualiza o resumo do carrinho
        });
    });

    // Função para atualizar o preço total do item
    function updateTotalPrice(quantityInput) {
        const cartItem = quantityInput.closest('.cart-item');
        const priceElement = cartItem.querySelector('.price');
        const totalPriceElement = cartItem.querySelector('.total-price');

        const price = parseFloat(priceElement.getAttribute('data-price'));
        const quantity = parseInt(quantityInput.value, 10);

        const totalPrice = price * quantity;
        totalPriceElement.textContent = `$${totalPrice.toFixed(2)}`;

        updateSummary(); // Atualiza o resumo do carrinho
    }

    // Aumentar a quantidade
    document.querySelectorAll('.increase').forEach(button => {
        button.addEventListener('click', function () {
            const input = this.previousElementSibling;
            let value = parseInt(input.value, 10);
            if (value < 5) {
                value = isNaN(value) ? 0 : value;
                input.value = value + 1;
            } else {
                showAlert('Limit reached', 'You have reached the maximum quantity for this item!');
            }
            updateTotalPrice(input);
        });
    });

    // Diminuir a quantidade
    document.querySelectorAll('.decrease').forEach(button => {
        button.addEventListener('click', function () {
            const input = this.nextElementSibling;
            let value = parseInt(input.value, 10);
            if (value > 1) {
                value = isNaN(value) ? 0 : value;
                input.value = value - 1;
                updateTotalPrice(input);
            }
        });
    });

    // Remover item
    document.querySelectorAll('.remove-item').forEach(button => {
        button.addEventListener('click', function () {
            const cartItem = this.closest('.cart-item');
            cartItem.remove();
            updateSummary(); // Atualiza o resumo do carrinho após a remoção
        });
    });

    updateSummary(); // Atualiza o resumo e os totais dos itens ao carregar a página
});

function CartToCheckout() {
    const subtotal = parseFloat(document.getElementById('Subtotal').value);
    const tax = parseFloat(document.getElementById('Tax').value);
    const total = parseFloat(document.getElementById('Total').value);
    const button = document.getElementById("checkoutBtn");
    if (subtotal === 0 && tax === 0) {
        if (button.type === "submit") {
            button.type = "button";
        }
        showAlertNegative('Your cart is empty.');
    } else {
        button.type = "submit";
    }
}