function printCityInfo(cityObject) {
    for (const key in cityObject) {
      if (cityObject.hasOwnProperty(key)) {
        console.log(`${key} -> ${cityObject[key]}`);
      }
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
  
  printCityInfo(city);
  