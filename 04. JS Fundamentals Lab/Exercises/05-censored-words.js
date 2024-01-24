// Define a function named censoredWords that takes two parameters: text and word
function censoredWords(text, word) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Replace occurrences of 'word' in 'text' with a censored version
    let censored = text.replace(word, repeat(word));

    // Use a while loop to continue replacing occurrences until none are left
    while (censored.includes(word)) {
        // Replace the next occurrence of 'word' with a censored version
        censored = censored.replace(word, repeat(word));
    }

    // Define a nested function named repeat to generate a censored version of 'word'
    function repeat(word) {
        return '*'.repeat(word.length);
    }

    // Log the censored text to the console
    console.log(censored);
}

// Call the censoredWords function with specific values ('A small sentence with some words', 'small')
censoredWords('A small sentence with some words', 'small');

// Call the censoredWords function with specific values ('Find the hidden word', 'hidden')
censoredWords('Find the hidden word', 'hidden');
