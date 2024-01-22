function largestNumber(num1, num2, num3) {
    'use strict'
    // if (num1 > num2 && num1 > num3) {
    //     console.log(`The largest number is ${num1}.`);
    // } else if (num2 > num1 && num2 > num3) {
    //     console.log(`The largest number is ${num2}.`);
    // } else {
    //     console.log(`The largest number is ${num3}.`);
    // }

    const result = Math.max(num1, num2, num3);
    console.log(`The largest number is ${result}.`);
}

largestNumber(9, 5, 20);