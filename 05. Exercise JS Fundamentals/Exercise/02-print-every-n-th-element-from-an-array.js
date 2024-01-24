// Define a function named printEveryNThElementFromAnArray that takes two parameters: inputArray and step
function printEveryNThElementFromAnArray(inputArray, step) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Initialize an empty array to store the result
    const result = [];

    // Use a for loop to iterate over the inputArray with a step size of 'step'
    for (let index = 0; index < inputArray.length; index += step) {
        // Push the current element to the result array
        result.push(inputArray[index]);
    }

    // Return the result array containing every Nth element
    return result;
}

// Call the printEveryNThElementFromAnArray function with specific values (['5', '20', '31', '4', '20'], 2)
console.log(printEveryNThElementFromAnArray(['5', '20', '31', '4', '20'], 2));

// Call the printEveryNThElementFromAnArray function with specific values (['dsa','asd', 'test', 'tset'], 2)
console.log(printEveryNThElementFromAnArray(['dsa','asd', 'test', 'tset'], 2));

// Call the printEveryNThElementFromAnArray function with specific values (['1', '2','3', '4', '5'], 6)
console.log(printEveryNThElementFromAnArray(['1', '2','3', '4', '5'], 6));
