function countStringOccurrences(string, searchWord) {
    'use strict'
    let words = string.split(' ');
    let counter = 0;

    for (let word of words) {
        if (word === searchWord) {
            counter ++;
        }
    }

    console.log(counter);
}

countStringOccurrences('This is a word and it also is a sentence', 'is')
countStringOccurrences('softuni is great place for learning new programming languages', 'softuni')