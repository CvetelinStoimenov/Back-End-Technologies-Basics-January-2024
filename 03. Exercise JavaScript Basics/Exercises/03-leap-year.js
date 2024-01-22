function isLeapYear(year) {
    'use strict';

    if ((year % 4 === 0 && year % 100 !== 0) || year % 400 === 0) {
        console.log('yes');
    } else {
        console.log('no');
    }
}

// Example usage:
isLeapYear(2024); // Output: yes
isLeapYear(2100); // Output: no

function lectorIsLeapYear(year) {
    'use strict';
    const isLeapYear = ((year % 4 === 0 && year % 100 !== 0) || year % 400 === 0)
    const message = isLeapYear ? "yes" : "no";
    console.log(message);
}

lectorIsLeapYear(1984)
lectorIsLeapYear(2003)
lectorIsLeapYear(4)