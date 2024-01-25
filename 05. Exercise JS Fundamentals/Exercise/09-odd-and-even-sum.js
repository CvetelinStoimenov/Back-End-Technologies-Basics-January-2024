// Define a function named OddAndEvenSum to calculate the sum of odd and even digits in a number
function OddAndEvenSum(inputNumber) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';
    
    // Initialize variables to store the sum of odd and even digits
    let oddSum = 0;
    let evenSum = 0;

    // Loop until inputNumber becomes 0
    while (inputNumber > 0) {
        // Extract the last digit of the inputNumber
        const currentDigit = inputNumber % 10;

        // Check if the current digit is even
        if (currentDigit % 2 === 0) {
            evenSum += currentDigit; // Add the current digit to the even sum
        } else {
            oddSum += currentDigit; // Add the current digit to the odd sum
        }

        // Update inputNumber to remove the last digit
        inputNumber = Math.floor(inputNumber / 10);
    }

    // Output the sum of odd and even digits to the console
    console.log(`Odd sum = ${oddSum}, Even
