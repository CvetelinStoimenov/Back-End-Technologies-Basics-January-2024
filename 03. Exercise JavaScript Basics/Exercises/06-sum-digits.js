function sumDigits(number) {
    'use strict'
    let sum = 0;
    const numberAsString = number.toString();
    for (const char of numberAsString) {
        const charAsNum = parseInt(char, 10);
        sum += charAsNum;
    }
    console.log(sum);
}

sumDigits(123)
sumDigits(245678)
sumDigits(97561)
sumDigits(543)