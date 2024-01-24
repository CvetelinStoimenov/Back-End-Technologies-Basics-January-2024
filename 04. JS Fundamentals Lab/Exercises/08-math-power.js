// Define a function named mathPowers that takes two parameters: number and power
function mathPowers(number, power) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Calculate the result using the Math.pow method
    let result = Math.pow(number, power);

    // Log the result to the console
    console.log(result);
}

// Call the mathPowers function with specific values (2, 8)
mathPowers(2, 8);

// Call the mathPowers function with specific values (3, 4)
mathPowers(3, 4);
