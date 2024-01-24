function sumFirstAndLast(array) {
    'use strict';

        const first = array[0];
        const last = array[array.length - 1];
        console.log(first + last);

}

// Example usage:
sumFirstAndLast([1, 2, 3, 4, 5]);
// Output: 6

sumFirstAndLast([10]);
// Output: Array should have at least two elements.

function sumFirstAndLastArrayElements(array) {
    'use strict';

    let first = array[0];
    let last = array[array.length - 1];
    console.log(first + last);
}

sumFirstAndLastArrayElements([1, 2, 3, 4, 5]);