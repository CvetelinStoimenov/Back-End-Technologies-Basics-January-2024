function movies(input) {
    let movies = []; // Initialize an empty array to store movie objects

    // Iterate through each command in the input
    for (let line of input) {
        // If the command is to add a movie
        if (line.includes("addMovie ")) {
            let name = line.split("addMovie ")[1]; // Extract the movie name
            movies.push({ name }); // Add an object representing the movie to the movies array
        } 
        // If the command is to specify the director of a movie
        else if (line.includes("directedBy")) {
            let [name, director] = line.split(" directedBy "); // Extract movie name and director
            let movie = movies.find((m) => m.name === name); // Find the corresponding movie object
            if (movie) {
                movie.director = director; // Update the director property of the movie
            }
        } 
        // If the command is to specify the release date of a movie
        else if(line.includes("onDate")){
            let [name, date] = line.split(" onDate "); // Extract movie name and release date
            let movie = movies.find((m) => m.name === name); // Find the corresponding movie object
            if(movie){
                movie.date = date; // Update the date property of the movie
            }
        }
    }

    // Print the details of movies that have all three properties (name, director, and date)
    for(let movie of movies){
        if(movie.name && movie.director && movie.date){
            console.log(JSON.stringify(movie)); // Convert movie object to JSON string and print
        }
    }
}

// Example usage:
movies([
    "addMovie Fast and Furious",
    "addMovie Godfather",
    "Inception directedBy Christopher Nolan",
    "Godfather directedBy Francis Ford Coppola",
    "Godfather onDate 29.07.2018",
    "Fast and Furious onDate 30.07.2018",
    "Batman onDate 01.08.2018",
    "Fast and Furious directedBy Rob Cohen",
]);
