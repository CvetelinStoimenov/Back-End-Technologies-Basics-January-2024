// Define a function named order that takes two parameters: product and quantity
function order(product, quantity) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Initialize a variable to store the price of the selected product
    let price = 0;

    // Use a switch statement to set the price based on the selected product
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
        // Handle the case where an invalid product is selected
        default:
            console.log('Invalid product');
            // Exit the function early using 'return'
            return;
    }

    // Calculate the total price based on the selected product and quantity
    let totalPrice = price * quantity;

    // Log the total price to the console with two decimal places
    console.log(totalPrice.toFixed(2));
}

// Call the order function with specific values ("snacks", 5)
order("snacks", 5);

// Call the order function with specific values ("coffee", 2)
order("coffee", 2);

// Call the order function with specific values ("snacks", 5)
order("snacks", 5);

// Call the order function with specific values ("coke", 2)
order("coke", 2);
