// Define a function named monthPrinter that takes a parameter 'numberOfMonth'
function monthPrinter(numberOfMonth) {
    // Enable strict mode to catch common coding errors and improve performance
    'use strict';

    // Use a switch statement to check the value of 'numberOfMonth'
    switch (numberOfMonth) {
        case 1:
            console.log('January');
            break;
        case 2:
            console.log('February');
            break;
        case 3:
            console.log('March');
            break;
        case 4:
            console.log('April');
            break;
        case 5:
            console.log('May');
            break;
        case 6:
            console.log('June');
            break;
        case 7:
            console.log('July');
            break;
        case 8:
            console.log('August');
            break;
        case 9:
            console.log('September');
            break;
        case 10:
            console.log('October');
            break;
        case 11:
            console.log('November');
            break;
        case 12:
            console.log('December');
            break;
        default:
            console.log('Error!'); // If 'numberOfMonth' doesn't match any case, log an error
            break;
    }
}

// Call the monthPrinter function with a specific number of the month (16)
monthPrinter(16);
