function caicleArea(input) {
    'use strict';
    let result;
    let inputType = typeof (input);
    if (inputType === "number") {
        result = Math.pow(input, 2) * Math.PI;
        console.log(result.toFixed(2));
    } else {
        console.log(`We can not calculate the circle area, because we received a ${inputType}.`)
    }
}

caicleArea(5);
caicleArea('name');