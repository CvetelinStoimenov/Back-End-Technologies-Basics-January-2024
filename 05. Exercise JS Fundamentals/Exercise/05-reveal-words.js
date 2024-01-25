// Define a function named revealWords that replaces placeholders with corresponding words
function revealWords(words, template) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Split the words and template strings into arrays
    const separatedWords = words.split(', ');
    const templateWords = template.split(' ');

    // Initialize an empty string to store the result
    let result = '';

    // Iterate through each word in the template
    for (const templateWord of templateWords) {
        // Check if the first character of the word is '*'
        if (templateWord[0] === '*') {
            // Find the corresponding word from separatedWords with the same length
            const correspondingWord = separatedWords.find(x => x.length === templateWord.length);
            // Append the corresponding word to the result
            result += `${correspondingWord} `;
        } else {
            // If the word is not a placeholder, append it to the result
            result += `${templateWord} `;
        }
    }

    // Output the result to the console
    console.log(result);
}

// Test cases
revealWords('great', 'softuni is ***** place for learning new programming languages');
revealWords('great, learning', 'softuni is ***** place for ******** new programming languages');
revealWords('great, learning, programming', 'softuni is ***** place for ******** new *********** languages');


// Define a function named revealWords1 that replaces placeholders with corresponding words
function revealWords1(words, template) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Split the words and template strings into arrays
    const separatedWords = words.split(', ');
    
    // Split the template string into an array of words, then map each word
    // to either the corresponding word from separatedWords or keep the word as is
    const result = template.split(' ').map(word =>
        word[0] === '*' ? // Check if the first character of the word is '*'
            separatedWords.find(x => x.length === word.length) : // If yes, find the corresponding word
            word // If no, keep the original word
    ).join(' '); // Join the array of words into a single string separated by spaces

    // Output the result to the console
    console.log(result);
}

// Call the revealWords1 function with specific words and template, and log the result
revealWords1('great, learning, programming', 'softuni is ***** place for ******** new *********** languages');


// Define a function named lectorRevealWords to replace placeholders in a template with corresponding words
function lectorRevealWords(words, template) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Split the words string into an array of individual words, separated by ', '
    const separatedWords = words.split(', ');

    // Iterate over each separated word
    for (const separatedWord of separatedWords) {
        // Replace each occurrence of '*' repeated for the length of the separated word with the separated word itself in the template
        template = template.replace('*'.repeat(separatedWord.length), separatedWord);
    }

    // Log the updated template with revealed words to the console
    console.log(template);
}

// Call the lectorRevealWords function with specific words and a template, and log the result
lectorRevealWords('great', 'softuni is ***** place for learning new programming languages');
lectorRevealWords('great, learning', 'softuni is ***** place for ******** new programming languages');
lectorRevealWords('great, learning, programming', 'softuni is ***** place for ******** new *********** languages');
