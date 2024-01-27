function lectorTowns(arrayOfRawData) {
    'use strict';
    
    // Initialize an empty array to store town data
    const townData = [];

    // Iterate over each raw data string in the input array
    for (const rawData of arrayOfRawData) {
        // Split the raw data string into individual pieces using the separator ' | '
        const splitData = rawData.split(' | ');

        // Create a town object and push it into the townData array
        townData.push({
            town: splitData[0],                                // Extract the town name
            latitude: parseFloat(splitData[1]).toFixed(2),     // Convert latitude to float and round to 2 decimal places
            longitude: parseFloat(splitData[2]).toFixed(2)     // Convert longitude to float and round to 2 decimal places
        });
    }

    // Output the town data to the console
    townData.forEach((town) => console.log(town));
}

lectorTowns(['Sofia | 42.696552 | 23.32601',
'Beijing | 39.913818 | 116.363625']
)
lectorTowns(['Plovdiv | 136.45 | 812.575'])


function towns(arrayOfTowns) {
    // Use an empty array to store the result
    const townsData = [];

    // Iterate over each row
    for (const row of arrayOfTowns) {
        // Split the row string into values using the pipe and space separator
        const [town, latitude, longitude] = row.split(" | ").map(value => value.trim());

        // Parse latitude and longitude to numbers and format them to the second decimal point
        const formattedLatitude = parseFloat(latitude).toFixed(2);
        const formattedLongitude = parseFloat(longitude).toFixed(2);

        // Create an object representing the town data and push it to the result array
        const townObj = {
            town,
            latitude: formattedLatitude,
            longitude: formattedLongitude
        };

        // Log the object directly without enclosing it in an array
        console.log(townObj);
    }
}

// Example usage:
const input = [
    'Sofia | 42.696552 | 23.32601',
    'Beijing | 39.913818 | 116.363625'
];

towns(input);
