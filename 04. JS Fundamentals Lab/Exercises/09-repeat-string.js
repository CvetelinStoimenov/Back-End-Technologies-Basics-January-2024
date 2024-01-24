// Define a function named repeatString that takes two parameters: string and repeatCount
function repeatString(string, repeatCount) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Initialize an empty string to store the repeated string
    let result = '';

    // Use a for loop to concatenate 'string' to 'result' 'repeatCount' times
    for (let i = 0; i < repeatCount; i++) {
        result += string;
    }

    // Return the final result
    return result;
}

// Call the repeatString function with specific values ("abc", 3)
repeatString("abc", 3);

// Call the repeatString function with specific values ("String", 2)
repeatString("String", 2);
