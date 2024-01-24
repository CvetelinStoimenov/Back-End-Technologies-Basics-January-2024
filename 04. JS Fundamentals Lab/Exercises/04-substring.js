// Define a function named substring that takes three parameters: string, startIndex, and count
function substring(string, startIndex, count) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Use the substring method to extract a substring from 'string'
    let result = string.substring(startIndex, startIndex + count);

    // Log the resulting substring to the console
    console.log(result);
}

// Call the substring function with specific values ('ASentence', 1, 8)
substring('ASentence', 1, 8);

// Call the substring function with specific values ('SkipWord', 4, 7)
substring('SkipWord', 4, 7);
