// Import the Math object to use its min method
function findSmallest(firstNumber, secondNumber, thirdNumber) {
    'use strict';

    // Correct the method name to Math.min
    const smallestNumber = Math.min(firstNumber, secondNumber, thirdNumber);
    
    console.log(smallestNumber);
}

// Example usage:
findSmallest(5, 3, 8); // Output: 3
