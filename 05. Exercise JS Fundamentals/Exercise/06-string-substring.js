// Define a function named stringSubstring to check if a word exists in a given text
function stringSubstring(word, text) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Split the text into an array of words
    const templateWords = text.split(' ');

    // Convert the word to lowercase for case-insensitive comparison
    const wordToLower = word.toLowerCase();

    // Initialize a flag to track if the word is found
    let isFound = false;

    // Iterate through each word in the text
    for (const templateWord of templateWords) {
        // Convert the current word to lowercase for case-insensitive comparison
        const compeerWord = templateWord.toLowerCase();
        
        // Check if the current word matches the target word
        if (compeerWord === wordToLower) {
            // If a match is found, set the flag to true
            isFound = true;
        } 
    }

    // Check if the word is found
    if (isFound === true) {
        // If found, log the word to the console
        console.log(wordToLower);
    } else {
        // If not found, log a message indicating that the word was not found
        console.log(`${wordToLower} not found!`);
    }
}

// Call the stringSubstring function with specific word and text inputs, and log the result
stringSubstring('Javascript', 'JavaScript is the best programming language');
stringSubstring('python', 'JavaScript is the best programming language');
