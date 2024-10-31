document.addEventListener('DOMContentLoaded', function () {
    const cartItemsContainer = document.querySelector('.cart-items');

    // Atualiza o subtotal, taxa e total geral
    function updateSummary() {
        let subtotal = 0;
        const cartItems = document.querySelectorAll('.cart-item'); // Seleciona todos os itens do carrinho
        cartItems.forEach(item => {
            const totalPriceElement = item.querySelector('.total-price');
            const totalPrice = parseFloat(totalPriceElement.textContent.replace('$', ''));
            subtotal += totalPrice;
        });

        const tax = subtotal * 0.10; // Exemplo de cálculo de taxa (10%)
        const total = subtotal + tax;

        document.querySelector('.subtotal').textContent = `Subtotal: $${subtotal.toFixed(2)}`;
        document.querySelector('.tax').textContent = `Tax: $${tax.toFixed(2)}`;
        document.querySelector('.total').textContent = `Total: $${total.toFixed(2)}`;
    }

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
            value = isNaN(value) ? 0 : value;
            input.value = value + 1;

            updateTotalPrice(input);
        });
    });

    // Diminuir a quantidade
    document.querySelectorAll('.decrease').forEach(button => {
        button.addEventListener('click', function () {
            const input = this.nextElementSibling;
            let value = parseInt(input.value, 10);
            if (value > 1) { // Evita que a quantidade fique menor que 1
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

    updateSummary(); // Atualiza o resumo ao carregar a página
});
