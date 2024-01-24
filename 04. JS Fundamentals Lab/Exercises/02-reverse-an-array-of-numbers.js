// Define a function named reverseAnArrayOfNumbers that takes two parameters: number and array
function reverseAnArrayOfNumbers(number, array) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Create a new array 'arr' to store the first 'number' elements from 'array'
    let arr = [];
    
    // Use a for loop to populate 'arr' with the first 'number' elements from 'array'
    for (let i = 0; i < number; i++) {
        arr.push(array[i]);
    }

    // Initialize an empty string 'output' to store the reversed elements
    let output = '';

    // Use a for loop to iterate through 'arr' in reverse order and concatenate elements to 'output'
    for (let i = arr.length - 1; i >= 0; i--) {
        output += arr[i] + ' ';
    }

    // Log the trimmed 'output' to the console
    console.log(output.trim());
}

// Call the reverseAnArrayOfNumbers function with specific values (3 and [10, 20, 30, 40, 50])
reverseAnArrayOfNumbers(3, [10, 20, 30, 40, 50]);

// Call the reverseAnArrayOfNumbers function with specific values (4 and [-1, 20, 99, 5])
reverseAnArrayOfNumbers(4, [-1, 20, 99, 5]);

// Call the reverseAnArrayOfNumbers function with specific values (2 and [66, 43, 75, 89, 47])
reverseAnArrayOfNumbers(2, [66, 43, 75, 89, 47]);
