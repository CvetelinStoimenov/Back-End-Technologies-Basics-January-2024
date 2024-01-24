// Define a function named numbersFromMToN that takes two parameters: m and n
function numbersFromMToN(m, n) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Use a for loop to iterate from 'm' to 'n' (inclusive) in descending order
    for (let i = m; i >= n; i--) {
        // Log the current value of 'i' to the console
        console.log(i);
    }
}

// Call the numbersFromMToN function with specific values (6 and 2)
numbersFromMToN(6, 2);
