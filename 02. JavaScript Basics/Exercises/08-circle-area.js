// Define a function named circleArea that takes one parameter: input
function circleArea(input) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Declare variables for the result and the type of the input
    let result;
    let inputType = typeof (input);

    // Check if the type of the input is a number
    if (inputType === "number") {
        // Calculate the area of the circle using the input as the radius
        result = Math.pow(input, 2) * Math.PI;

        // Log the result to the console with two decimal places
        console.log(result.toFixed(2));
    } else {
        // If the input is not a number, log an error message
        console.log(`We cannot calculate the circle area because we received a ${inputType}.`);
    }
}

// Call the circleArea function with a numeric input (5)
circleArea(5);

// Call the circleArea function with a non-numeric input ('name')
circleArea('name');
