// Define a function named sumFirstAndLast that takes one parameter: array
function sumFirstAndLast(array) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Retrieve the first and last elements of the array
    const first = array[0];
    const last = array[array.length - 1];

    // Log the sum of the first and last elements to the console
    console.log(first + last);
}

// Example usage:
sumFirstAndLast([1, 2, 3, 4, 5]);
// Output: 6

sumFirstAndLast([10]);
// Output: Array should have at least two elements.


// Define a function named sumFirstAndLastArrayElements that takes one parameter: array
function sumFirstAndLastArrayElements(array) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Retrieve the first and last elements of the array
    let first = array[0];
    let last = array[array.length - 1];

    // Log the sum of the first and last elements to the console
    console.log(first + last);
}

// Call the sumFirstAndLastArrayElements function with an array [1, 2, 3, 4, 5]
sumFirstAndLastArrayElements([1, 2, 3, 4, 5]);
