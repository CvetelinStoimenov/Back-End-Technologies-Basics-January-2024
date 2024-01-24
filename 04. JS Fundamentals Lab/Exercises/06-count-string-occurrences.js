// Define a function named countStringOccurrences that takes two parameters: string and searchWord
function countStringOccurrences(string, searchWord) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Split the input string into an array of words using ' ' (space) as a separator
    let words = string.split(' ');

    // Initialize a counter to keep track of the number of occurrences of searchWord
    let counter = 0;

    // Use a for...of loop to iterate through each word in the array
    for (let word of words) {
        // Check if the current word is equal to the searchWord
        if (word === searchWord) {
            // Increment the counter if a match is found
            counter++;
        }
    }

    // Log the final count to the console
    console.log(counter);
}

// Call the countStringOccurrences function with specific values ('This is a word and it also is a sentence', 'is')
countStringOccurrences('This is a word and it also is a sentence', 'is');

// Call the countStringOccurrences function with specific values ('softuni is great place for learning new programming languages', 'softuni')
countStringOccurrences('softuni is a great place for learning new programming languages', 'softuni');
