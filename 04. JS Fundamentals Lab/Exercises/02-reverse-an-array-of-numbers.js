function reverseAnArrayOfNumbers(number, array) {
    'use strict';
    let arr = [];
    
    for (let i = 0; i < number; i++) {
        arr.push(array[i]);
    }

    let output = '';

    for (let i = arr.length - 1; i >= 0; i--) {
        output += arr[i] + ' ';
    }

    console.log(output.trim());
}

reverseAnArrayOfNumbers(3, [10, 20, 30, 40, 50])
reverseAnArrayOfNumbers(4, [-1, 20, 99, 5])
reverseAnArrayOfNumbers(2, [66, 43, 75, 89, 47])