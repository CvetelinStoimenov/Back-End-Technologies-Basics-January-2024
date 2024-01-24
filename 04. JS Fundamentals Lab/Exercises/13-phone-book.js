// Define a function named phoneBook that takes an array 'input'
function phoneBook(input) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Create an empty object to store unique names and phone numbers
    let uniqueName = {};

    // Iterate through each element in the input array using forEach
    input.forEach(element => {
        // Split each element into a key-value pair using space as a separator
        let keyValuePair = element.split(' ');

        // Extract the name and phone number from the key-value pair
        let name = keyValuePair[0];
        let phoneNumber = keyValuePair[1];

        // Add the name and phone number to the uniqueName object
        uniqueName[name] = phoneNumber;
    });

    // Iterate through each key-value pair in the uniqueName object
    for (let key in uniqueName) {
        // Log the name and corresponding phone number to the console
        console.log(`${key} -> ${uniqueName[key]}`);
    }
}

// Call the phoneBook function with an array of phone book entries
phoneBook([
    'Tim 0834212554',
    'Peter 0877547887',
    'Bill 0896543112',
    'Tim 0876566344'
]);
