// Define a function named excellentGrade that takes a parameter 'grade'
function excellentGrade(grade) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Check if the provided grade is greater than or equal to 5.50
    if (grade >= 5.50) {
        // If true, log "Excellent" to the console
        console.log("Excellent");
    } else {
        // If false, log "Not excellent" to the console
        console.log("Not excellent");
    }
}

// Call the excellentGrade function with a specific grade (5.50)
excellentGrade(5.50);
