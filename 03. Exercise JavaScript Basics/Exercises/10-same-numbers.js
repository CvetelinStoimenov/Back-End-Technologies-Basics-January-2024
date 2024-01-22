function sameNumbers(number) {
    'use strict'

    const digits = String(number).split('').map(Number);

    // Check if all digits are the same
    const allSame = digits.every(digit => digit === digits[0]);

    console.log(allSame); // Print true or false

    // Calculate and print the sum of all digits
    const sum = digits.reduce((acc, digit) => acc + digit, 0);
    console.log(sum);
}

sameNumbers(2222222)

function lectorSameNumbers(number) {
    'use strict'
    let totalSum = 0;
    let allDigitsAreTheSame = true;
    const firstDigit = number % 10;

    while (number > 0) {
        const currentDigit = number % 10;

        if (firstDigit != currentDigit) {
            allDigitsAreTheSame = false;
        }

        totalSum += currentDigit;
        number = Math.floor(number / 10);
    }

    console.log(allDigitsAreTheSame);
    console.log(totalSum)
}