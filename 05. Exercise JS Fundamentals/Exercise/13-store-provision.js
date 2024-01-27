function storeProvisions(currentStock, orderStock) {
    'use strict';

    // Initialize an empty object to store the store's stock
    const storeStock = {};

    // Loop through the current stock array
    for (let index = 0; index < currentStock.length; index += 2) {
        // Extract the stock name and amount from the current stock array
        const stockName = currentStock[index];
        const stockAmount = parseInt(currentStock[index + 1], 10);

        // Update the storeStock object with the current stock information
        storeStock[stockName] = stockAmount;
    }

    // Loop through the ordered stock array
    for (let index = 0; index < orderStock.length; index += 2) {
        // Extract the stock name and amount from the ordered stock array
        const stockName = orderStock[index];
        const stockAmount = parseInt(orderStock[index + 1], 10);

        // Check if the stock item already exists in the storeStock
        if (storeStock[stockName] !== undefined) {
            // If it exists, update the stock amount by adding the ordered amount
            storeStock[stockName] += stockAmount;
        } else {
            // If it doesn't exist, add a new stock item to the storeStock
            storeStock[stockName] = stockAmount;
        }
    }

    // Output the storeStock information to the console
    Object.keys(storeStock).forEach((currentItemName) => console.log(`${currentItemName} -> ${storeStock[currentItemName]}`));
}

// Example usage:
storeProvisions (['Chips', '5', 'CocaCola', '9', 'Bananas', '14', 'Pasta', '4', 'Beer', '2'],
    ['Flour', '44', 'Oil', '12', 'Pasta', '7', 'Tomatoes', '70', 'Bananas', '30'])
storeProvisions (['Salt', '2', 'Fanta', '4', 'Apple', '14', 'Water', '4', 'Juice', '5'],
    ['Sugar', '44', 'Oil', '12', 'Apple', '7', 'Tomatoes', '7', 'Bananas', '30'])    