function cookingByNumbers(rawnumber, op1, op2, op3, op4, op5) {
    'use stict'

    let number = parseInt(rawnumber, 10);

    if (op1 === 'chop') {
        number = number / 2;
    } else if (op1 === 'dice') {
        number = Math.sqrt(number);
    } else if (op1 === 'spice') {
        number += 1;
    } else if (op1 === 'bake') {
        number = number * 3;
    } else if (op1 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number);

    if (op2 === 'chop') {
        number = number / 2;
    } else if (op2 === 'dice') {
        number = Math.sqrt(number);
    } else if (op2 === 'spice') {
        number += 1;
    } else if (op2 === 'bake') {
        number = number * 3;
    } else if (op2 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number);

    if (op3 === 'chop') {
        number = number / 2;
    } else if (op3 === 'dice') {
        number = Math.sqrt(number);
    } else if (op3 === 'spice') {
        number += 1;
    } else if (op3 === 'bake') {
        number = number * 3;
    } else if (op3 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number);

    if (op4 === 'chop') {
        number = number / 2;
    } else if (op4 === 'dice') {
        number = Math.sqrt(number);
    } else if (op4 === 'spice') {
        number += 1;
    } else if (op4 === 'bake') {
        number = number * 3;
    } else if (op4 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number);

    if (op5 === 'chop') {
        number = number / 2;
    } else if (op5 === 'dice') {
        number = Math.sqrt(number);
    } else if (op5 === 'spice') {
        number += 1;
    } else if (op5 === 'bake') {
        number = number * 3;
    } else if (op5 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number);
}

cookingByNumbers('32', 'chop', 'chop', 'chop', 'chop', 'chop')
cookingByNumbers('9', 'dice', 'spice', 'chop', 'bake', 'fillet')

function lectorCookingByNumbers(rawnumber, firstOperation, secondOperation, thirdOperation, forthOperation, fifthOperation) {
    'use stict'

    function executeOperation(currentNumber, currentOperation) {
        if (currentOperation === 'chop') {
            return currentNumber / 2;
        } else if (currentOperation === 'dice') {
            return Math.sqrt(currentNumber);
        } else if (currentOperation === 'spice') {
            return currentNumber + 1;
        } else if (currentOperation === 'bake') {
            return currentNumber * 3;
        } else if (currentOperation === 'fillet') {
            return currentNumber * 0.8;
        } else {
            return number;
        }
    }

    let number = parseInt(rawnumber, 10);

    number = executeOperation(number, firstOperation);
    console.log(number);

    number = executeOperation(number, secondOperation);
    console.log(number);

    number = executeOperation(number, thirdOperation);
    console.log(number);

    number = executeOperation(number, forthOperation);
    console.log(number);

    number = executeOperation(number, fifthOperation);
    console.log(number);
}

lectorCookingByNumbers('32', 'chop', 'chop', 'chop', 'chop', 'chop')
lectorCookingByNumbers('9', 'dice', 'spice', 'chop', 'bake', 'fillet')

function lector1CookingByNumbers(rawnumber, firstOperation, secondOperation, thirdOperation, forthOperation, fifthOperation) {
    'use stict'

    const operations = [firstOperation, secondOperation, thirdOperation, forthOperation, fifthOperation];

    function executeOperation(currentNumber, currentOperation) {
        if (currentOperation === 'chop') {
            return currentNumber / 2;
        } else if (currentOperation === 'dice') {
            return Math.sqrt(currentNumber);
        } else if (currentOperation === 'spice') {
            return currentNumber + 1;
        } else if (currentOperation === 'bake') {
            return currentNumber * 3;
        } else if (currentOperation === 'fillet') {
            return currentNumber * 0.8;
        } else {
            return number;
        }
    }

    let number = parseInt(rawnumber, 10);

    for (const operation of operations) {
        number = executeOperation(number, operation);
        console.log(number);
    }

}

lector1CookingByNumbers('32', 'chop', 'chop', 'chop', 'chop', 'chop')
lector1CookingByNumbers('9', 'dice', 'spice', 'chop', 'bake', 'fillet')