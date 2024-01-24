function phoneBook(input) {
    'use strict'
    let uniqueName = {};
    input.forEach(element => {
        let keyValuePair = element.split(' ');
        let name = keyValuePair[0];
        let phoneNumber = keyValuePair[1];
        uniqueName[name] = phoneNumber;
    });

    for (let key in uniqueName) {
        console.log(`${key} -> ${uniqueName[key]}`);
    }
}

phoneBook(['Tim 0834212554',
'Peter 0877547887',
'Bill 0896543112',
'Tim 0876566344']
)