// Define a function named evenAndOddSubtraction that takes one parameter: arr
function evenAndOddSubtraction(arr) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Initialize variables to store the sum of even and odd numbers
    let evenSum = 0;
    let oddSum = 0;

    // Use a for loop to iterate through each element in the array 'arr'
    for (let i = 0; i < arr.length; i++) {
        // Convert the current element to a number
        let currentNumber = Number(arr[i]);

        // Check if the current number is even or odd and update the corresponding sum
        if (currentNumber % 2 == 0) {
            evenSum += currentNumber;
        } else {
            oddSum += currentNumber;
        }
    }

    // Log the result of subtracting the sum of odd numbers from the sum of even numbers
    console.log(evenSum - oddSum);
}

// Call the evenAndOddSubtraction function with specific arrays
evenAndOddSubtraction([1,2,3,4,5,6]);
evenAndOddSubtraction([3,5,7,9]);
evenAndOddSubtraction([2,4,6,8,10]);
