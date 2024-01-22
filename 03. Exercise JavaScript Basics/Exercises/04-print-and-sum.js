function printAndSum(start, end ){
    'use strict'
    let message = '';
    let sum = 0;
    for (let i = start; i <= end; i++) {
        sum += i;
        message += `${i} `;
    }
    console.log(message.trimEnd())
    console.log(`Sum: ${sum}`)
}

printAndSum(0, 26)