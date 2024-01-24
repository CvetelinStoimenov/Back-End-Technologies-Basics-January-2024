// Define a function named arrayRotation that takes two parameters: inputArray and numberRotation
function arrayRotation(inputArray, numberRotation) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Use a for loop to perform array rotation 'numberRotation' times
    for (let i = 0; i < numberRotation; i++) {
        // Remove the first element from the array and store it in a variable
        const firstElement = inputArray.shift();
        
        // Add the removed element to the end of the array
        inputArray.push(firstElement);
    }

    // Log the rotated array elements joined by a space to the console
    console.log(inputArray.join(' '));
}

// Call the arrayRotation function with specific values ([51, 47, 32, 61, 21], 2)
arrayRotation([51, 47, 32, 61, 21], 2);

// Call the arrayRotation function with specific values ([32, 21, 61, 1], 4)
arrayRotation([32, 21, 61, 1], 4);

// Call the arrayRotation function with specific values ([2, 4, 15, 31], 5)
arrayRotation([2, 4, 15, 31], 5);
