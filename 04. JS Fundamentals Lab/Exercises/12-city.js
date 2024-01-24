function printCityInfo(cityObject) {
    // Iterate through all properties of the cityObject
    for (const key in cityObject) {
      // Check if the property is directly owned by the object- optional
      // if (cityObject.hasOwnProperty(key)) {
        // Print the property and its corresponding value
        console.log(`${key} -> ${cityObject[key]}`);
      // }
    }
  }
  
  // Example usage:
  const city = {
    name: "New York",
    area: 468.9,
    population: 8398748,
    country: "United States",
    postcode: "10001"
  };
  
  // Use the function to print information about the city
  printCityInfo(city);
  