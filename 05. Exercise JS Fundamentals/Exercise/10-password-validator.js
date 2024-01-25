function lectorPasswordValidator(password) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Initialize an array to store validation errors
    const errors = [];

    // Regular expression to match only letters and digits
    const regexOnlyLettersAndDigit = /^[a-zA-Z0-9]+$/;

    // Function to count the number of digits in a string
    const countDigitAndString = (word) => {
        let counter = 0;
        for (const char of word) {
            // Check if the character is a digit
            if (!isNaN(parseInt(char))) {
                counter += 1; // Increment the counter if it's a digit
            }
        }
        return counter; // Return the count of digits in the string
    };

    // Count the number of digits in the password
    const numberOfDigitsInString = countDigitAndString(password);

    // Check if the password length is between 6 and 10 characters
    if (password.length < 6 || password.length > 10) {
        errors.push('Password must be between 6 and 10 characters');
    }

    // Check if the password consists only of letters and digits
    if (!regexOnlyLettersAndDigit.test(password)) {
        errors.push('Password must consist only of letters and digits');
    }

    // Check if the password has at least 2 digits
    if (numberOfDigitsInString < 2) {
        errors.push('Password must have at least 2 digits');
    }

    // Output the validation result
    if (errors.length === 0) {
        console.log('Password is valid');
    } else {
        errors.forEach((error) => console.log(error));
    }
}

// Example usage:
lectorPasswordValidator("abc123"); // Example of a valid password
lectorPasswordValidator("password123"); // Example of an invalid password


function validatePassword(password) {
    'use strict';

    let isValid = true;
    let messages = [];

    // Validation 1: Check length
    if (password.length < 6 || password.length > 10) {
        messages.push("Password must be between 6 and 10 characters");
        isValid = false;
    }

    // Validation 2: Check if it consists only of letters and digits
    if (!/^[a-zA-Z0-9]+$/.test(password)) {
        messages.push("Password must consist only of letters and digits");
        isValid = false;
    }

    // Validation 3: Check if it has at least 2 digits
    if ((password.match(/[0-9]/g) || []).length < 2) {
        messages.push("Password must have at least 2 digits");
        isValid = false;
    }

    // Print validation result
    if (isValid) {
        console.log("Password is valid");
    } else {
        messages.forEach(message => console.log(message));
    }
}

// Example usage:
validatePassword("abc123"); // Example of a valid password
validatePassword("password123"); // Example of an invalid password
