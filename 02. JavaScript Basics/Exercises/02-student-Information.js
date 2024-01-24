// Define a function named studentInformation that takes three parameters: name, age, and grade
function studentInformation(name, age, grade) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Log student information using template literals
    // Displaying the name, age, and formatting the grade with two decimal places
    console.log(`Name: ${name}, Age: ${age}, Grade: ${grade.toFixed(2)}`);
}

// Call the studentInformation function with specific arguments
studentInformation('Ivan', 25, 6);
