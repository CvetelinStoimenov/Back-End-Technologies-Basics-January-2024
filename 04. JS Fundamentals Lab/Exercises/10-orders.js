function order(product, quantity) {
    'use strict'

    let price = 0;

    switch (product) {
        case 'coffee':
            price = 1.50;
            break;
        case 'coke':
            price = 1.40;
            break;
        case 'water':
            price = 1.00;
            break;
        case 'snacks':
            price = 2.00;
            break;
        default:
            console.log('Invalid product');
            return;
    }

    let totalPrice = price * quantity;
    console.log(totalPrice.toFixed(2));
}

order("snacks", 5)
order("coffee", 2)
order("snacks", 5)
order("coke", 2)