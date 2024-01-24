// Define a function named mathOperation that takes three parameters: num1, num2, and operator
function mathOperation(num1, num2, operator) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Declare a variable 'result' to store the result of the mathematical operation
    let result;

    // Use a switch statement to check the value of 'operator'
    switch (operator) {
        case '+':
            // If 'operator' is '+', perform addition and assign the result to 'result'
            result = num1 + num2;
            break;
        case '-':
            // If 'operator' is '-', perform subtraction and assign the result to 'result'
            result = num1 - num2;
            break;
        case '*':
            // If 'operator' is '*', perform multiplication and assign the result to 'result'
            result = num1 * num2;
            break;
        case '/':
            // If 'operator' is '/', perform division and assign the result to 'result'
            result = num1 / num2;
            break;
        case '%':
            // If 'operator' is '%', perform modulo operation and assign the result to 'result'
            result = num1 % num2;
            break;
        case '**':
            // If 'operator' is '**', perform exponentiation and assign the result to 'result'
            result = num1 ** num2;
            break;
    }

    // Log the final result to the console
    console.log(result);
}

// Call the mathOperation function with specific numbers and an operator ('+')
mathOperation(2, 2, '+');
