// Define a function named formatGrade that takes one parameter: grade
function formatGrade(grade) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Check different grade ranges and print the corresponding message
    if (grade < 3.00) {
        console.log(`Fail (2)`);
    } else if (grade >= 3.00 && grade < 3.50) {
        console.log(`Poor (${grade.toFixed(2)})`);
    } else if (grade >= 3.50 && grade < 4.50) {
        console.log(`Good (${grade.toFixed(2)})`);
    } else if (grade >= 4.50 && grade < 5.50) {
        console.log(`Very good (${grade.toFixed(2)})`);
    } else if (grade >= 5.50) {
        console.log(`Excellent (${grade.toFixed(2)})`);
    }
}

// Call the formatGrade function with specific values (2.99)
formatGrade(2.99);

// Call the formatGrade function with specific values (3.00)
formatGrade(3.00);
