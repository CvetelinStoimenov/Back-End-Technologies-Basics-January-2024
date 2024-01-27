function piccolo(log) {
    const parkingLot = new Set(); // Create a new Set to store car numbers in the parking lot
    
    // Iterate over each entry in the log
    log.forEach(entry => {
        const [direction, carNumber] = entry.split(', '); // Split each log entry into direction and car number

        // If the direction is 'IN', add the car number to the parking lot
        if (direction === 'IN') {
            parkingLot.add(carNumber);
        } 
        // If the direction is 'OUT', remove the car number from the parking lot
        else if (direction === 'OUT') {
            parkingLot.delete(carNumber);
        }
    });

    // Check if the parking lot is empty
    if (parkingLot.size === 0) {
        console.log('Parking Lot is Empty');
    } else {
        // If the parking lot is not empty, sort the car numbers and print them
        const sortedCarNumbers = [...parkingLot].sort();
        sortedCarNumbers.forEach(carNumber => console.log(carNumber));
    }
}

// Example usage:
const input = [
    'IN, CA2844AA',
    'IN, CA1234TA',
    'OUT, CA2844AA',
    'IN, CA9999TT',
    'IN, CA2866HI',
    'OUT, CA1234TA',
    'IN, CA2844AA',
    'OUT, CA2866HI',
    'IN, CA9876HH',
    'IN, CA2822UU'
];
piccolo(input);
