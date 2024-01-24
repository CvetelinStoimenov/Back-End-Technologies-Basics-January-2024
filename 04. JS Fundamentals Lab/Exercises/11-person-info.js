// Define a function named personInfo that takes three parameters: firstName, lastName, and age
function personInfo(firstName, lastName, age) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Create an empty object to store person information
    let person = {};

    // Assign values to properties of the person object
    person.firstName = firstName;
    person.lastName = lastName;
    person.age = age;

    // Return the populated person object
    return person;
}
