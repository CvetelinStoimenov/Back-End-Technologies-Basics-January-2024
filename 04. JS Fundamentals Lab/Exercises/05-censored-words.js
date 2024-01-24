function censoredWords(text, word) {
    'use strict';
    let censored = text.replace(word, repeat(word));

    while (censored.includes(word)) {
        censored = censored.replace(word, repeat(word));
    }

    
    function repeat(word) {
        return '*'.repeat(word.length);
    }

    console.log(censored);
}



censoredWords('A small sentence with some words', 'small')
censoredWords('Find the hidden word', 'hidden')