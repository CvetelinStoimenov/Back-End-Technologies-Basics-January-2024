function employees(arrayOfEmployees) {
    'use strict';

    // Initialize an empty array to store employee data
    const employeeData = [];

    // Iterate over each employee in the input array
    for (const employeeRaw of arrayOfEmployees) {
        // Push a new employee data object into the employeeData array
        // This object contains the employee name and the length of the name as personalNumber
        employeeData.push({
            name: employeeRaw,
            personalNumber: employeeRaw.length
        });
    }

    // Output the employee data to the console
    employeeData.forEach((employee) => console.log(`Name: ${employee.name} -- Personal Number: ${employee.personalNumber}`));
}

employees(['Silas Butler', 'Adnaan Buckley', 'Juan Peterson', 'Brendan Villarreal'])
employees([
    'Samuel Jackson',
    'Will Smith',
    'Bruce Willis',
    'Tom Holland'
    ]
    )




function employees1(arrayOfEmployees) {
    'use strict';
    
    // Use the map method to transform each employee name into an employee data object
    const employeeData = arrayOfEmployees.map((employeeRaw) => ({
        name: employeeRaw,
        personalNumber: employeeRaw.length
    }));

    // Output the employee data to the console
    employeeData.forEach((employee) => console.log(`Name: ${employee.name} -- Personal Number: ${employee.personalNumber}`));
}

employees1(['Silas Butler', 'Adnaan Buckley', 'Juan Peterson', 'Brendan Villarreal'])
employees1([
    'Samuel Jackson',
    'Will Smith',
    'Bruce Willis',
    'Tom Holland'
    ]
    )