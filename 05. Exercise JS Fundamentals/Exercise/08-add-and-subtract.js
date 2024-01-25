// Define a function named addAndSubtract with three parameters: firstNumber, secondNumber, and thirdNumber
function addAndSubtract(firstNumber, secondNumber, thirdNumber) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';
    
    // Define an arrow function named sum that takes two parameters and returns their sum
    const sum = (first, second) => first + second;
    
    // Define an arrow function named subtract that takes two parameters and returns their difference
    const subtract = (first, second) => first - second;

    // Calculate the sum of firstNumber and secondNumber, then subtract thirdNumber from the result
    const result = subtract(sum(firstNumber, secondNumber), thirdNumber);

    // Log the result to the console
    console.log(result);
}

// Example usage:
addAndSubtract(5, 3, 2); // Output: 6
