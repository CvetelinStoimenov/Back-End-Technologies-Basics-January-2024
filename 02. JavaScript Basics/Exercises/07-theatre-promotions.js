// Define a function named theatrePromotions that takes two parameters: day and age
function theatrePromotions(day, age) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Declare a variable 'result' to store the ticket price or error message
    let result;

    // Check the value of 'day'
    if (day === 'Weekday') {
        // Check the age range and set the ticket price accordingly
        if (age >= 0 && age <= 18) {
            result = '12$';
        } else if (age > 18 && age <= 64) {
            result = '18$';
        } else if (age > 64 && age <= 122) {
            result = '12$';
        } else if (age < 0 || age > 122) {
            result = 'Error!';
        }
    } else if (day === 'Weekend') {
        // Similar checks for the 'Weekend' day
        if (age >= 0 && age <= 18) {
            result = '15$';
        } else if (age > 18 && age <= 64) {
            result = '20$';
        } else if (age > 64 && age <= 122) {
            result = '15$';
        } else if (age < 0 || age > 12) {
            result = 'Error!';
        }
    } else if (day === 'Holiday') {
        // Similar checks for the 'Holiday' day
        if (age >= 0 && age <= 18) {
            result = '5$';
        } else if (age > 18 && age <= 64) {
            result = '12$';
        } else if (age > 64 && age <= 122) {
            result = '10$';
        } else if (age < 0 || age > 122) {
            result = 'Error!';
        }
    }

    // Log the result to the console
    console.log(result);
}

// Call the theatrePromotions function with specific day and age values
theatrePromotions('Holiday', -1);


// Define a function named oneTheatrePromotions that takes two parameters: day and age
function oneTheatrePromotions(day, age) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Declare a variable 'result' to store the ticket price or error message
    let result;

    // Check the age range and set the ticket price or error message accordingly
    if (age >= 0 && age <= 18) {
        if (day === 'Weekday') {
            result = '12$';
        } else if (day === 'Weekend') {
            result = '15$';
        } else if (day === 'Holiday') {
            result = '5$';
        }
    } else if (age > 18 && age <= 64) {
        if (day === 'Weekday') {
            result = '18$';
        } else if (day === 'Weekend') {
            result = '20$';
        } else if (day === 'Holiday') {
            result = '12$';
        }
    } else if (age > 64 && age <= 122) {
        if (day === 'Weekday') {
            result = '12$';
        } else if (day === 'Weekend') {
            result = '15$';
        } else if (day === 'Holiday') {
            result = '10$';
        }
    } else {
        result = 'Error!';
    }

    // Log the result to the console
    console.log(result);
}

// Call the oneTheatrePromotions function with specific day and age values
oneTheatrePromotions('Holiday', 15);
