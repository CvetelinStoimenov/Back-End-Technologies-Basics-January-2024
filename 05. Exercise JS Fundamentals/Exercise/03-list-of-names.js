// Define a function named listOfNames that takes one parameter: listOfNames
function listOfNames(listOfNames) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Sort the elements of the listOfNames array alphabetically
    listOfNames.sort((a, b) => a.localeCompare(b));

    // Use a for loop to iterate over the sorted array and log each element with an index
    for (let index = 1; index <= listOfNames.length; index += 1) {
        // Log the current index and the corresponding name to the console
        console.log(`${index}.${listOfNames[index - 1]}`);
    }
}


listOfNames(["John", "Bob", "Christina", "Ema"])