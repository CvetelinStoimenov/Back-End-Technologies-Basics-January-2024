// Define a function named largestNumber that takes three parameters: num1, num2, and num3
function largestNumber(num1, num2, num3) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Alternative method using if-else statements to find the largest number
    // Commented out for simplicity, as the function now uses a more concise approach

    /*
    if (num1 > num2 && num1 > num3) {
        console.log(`The largest number is ${num1}.`);
    } else if (num2 > num1 && num2 > num3) {
        console.log(`The largest number is ${num2}.`);
    } else {
        console.log(`The largest number is ${num3}.`);
    }
    */

    // Use Math.max to find the largest number among num1, num2, and num3
    const result = Math.max(num1, num2, num3);

    // Log the result to the console
    console.log(`The largest number is ${result}.`);
}

// Call the largestNumber function with specific numbers (9, 5, 20)
largestNumber(9, 5, 20);
