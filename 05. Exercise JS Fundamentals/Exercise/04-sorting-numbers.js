// Define a function named lectorSortingNumbers that takes one parameter: inputArray
function lectorSortingNumbers(inputArray) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Sort the inputArray in ascending order using a comparator function
    inputArray.sort((a, b) => a - b);

    // Initialize an empty array to store the sorted numbers in the required order
    const result = [];

    // Use a while loop to continuously process the inputArray until it's empty
    while (inputArray.length > 0) {
        // Remove the first element from the inputArray and store it in a variable
        const firstElement = inputArray.shift();

        // Remove the last element from the inputArray and store it in a variable
        const lastElement = inputArray.pop();

        // Push the first element to the result array
        result.push(firstElement);

        // Check if lastElement exists (inputArray was not empty)
        if (lastElement) {
            // Push the last element to the result array
            result.push(lastElement);
        }
    }

    // Return the sorted numbers array in the required order
    return result;
}

// Call the lectorSortingNumbers function with a specific array
console.log(lectorSortingNumbers([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]));




// Define a function named sortingNumbers that sorts an array and returns a new array with alternating elements
function sortingNumbers(inputArray) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Step 1: Sort the inputArray in ascending order
    inputArray.sort((a, b) => a - b);

    // Initialize an empty array to store the result
    const result = [];

    // Initialize left pointer at the beginning of the array and right pointer at the end of the array
    let left = 0;
    let right = inputArray.length - 1;

    // Use a while loop to iterate until left pointer exceeds right pointer
    while (left <= right) {
        // Add the element at the left pointer to the result array
        result.push(inputArray[left]);

        // Check if left pointer is not equal to right pointer to avoid duplicate addition
        if (left !== right) {
            // Add the element at the right pointer to the result array
            result.push(inputArray[right]);
        }

        // Move the left pointer to the right and the right pointer to the left
        left++;
        right--;
    }

    // Return the result array
    return result;
}

// Call the sortingNumbers function with a specific array and log the result
console.log(sortingNumbers([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]));



function customSort1(array) {
    'use strict';

    // Step 1: Sort the array in ascending order
    array.sort((a, b) => a - b);

    // Step 2: Create a new array with alternating elements
    let result = [];
    for (let i = 0; i < array.length / 2; i++) {
        result.push(array[i]);
        result.push(array[array.length - 1 - i]);
    }

    // If the array length is odd, add the middle element
    if (array.length % 2 !== 0) {
        result.push(array[Math.floor(array.length / 2)]);
    }

    return result;
}

// Call the customSort1 function with a specific array and log the result
console.log(customSort1([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]));