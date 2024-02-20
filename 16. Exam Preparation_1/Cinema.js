function cinema(input) {
  // Extract the number of movies from the input
  const numberOfMovies = parseInt(input[0]);
  
  // Extract the movie names from the input
  const moviesNames = input.slice(1, numberOfMovies + 1);
  
  // Extract the commands from the input
  const commands = input.slice(numberOfMovies + 1);

  // Initialize an array to hold the movie tickets
  let tickets = [...moviesNames];

  // Iterate through each command
  for (const command of commands) {
    // Check if the command is "End"
    if (command === "End") {
      // If the command is "End", check if there are any tickets left
      if (tickets.length > 0) {
        // If there are tickets left, print them
        console.log("Tickets left: " + tickets.join(", "));
      } else {
        // If there are no tickets left, print that the box office is empty
        console.log("The box office is empty");
      }
      // Exit the loop as the sorting is complete
      break;
    } else if (command.startsWith("Sell")) {
      // If the command is "Sell", sell the first ticket
      if (tickets.length > 0) {
        // If there are tickets available, sell the first one
        // and remove it from the tickets array
        const [soldMovie, ...remainingTickets] = tickets;
        tickets = remainingTickets;
        console.log(`${soldMovie} ticket sold!`);
      }
    } else if (command.startsWith("Add")) {
      // If the command is "Add", add a new movie ticket
      // to the tickets array
      // Extract the movie title from the command
      const movieTitle = command.split("Add ")[1];
      // Add the movie title to the tickets array
      tickets.push(movieTitle);
    } else if (command.startsWith("Swap")) {
      // If the command is "Swap", swap two movie tickets
      // Extract the start and end indices from the command
      const [, startIndex, endIndex] = command.split(" ").map(Number);
      // Check if the provided indices are valid
      if (
        startIndex >= 0 &&
        endIndex >= 0 &&
        startIndex < tickets.length &&
        endIndex < tickets.length
      ) {
        // Swap the movie tickets at the given indices
        [tickets[startIndex], tickets[endIndex]] = [
          tickets[endIndex],
          tickets[startIndex],
        ];
        console.log("Swapped!");
      }
    }
  }
}

// Example usage:
cinema([
  "5",
  "The Matrix",
  "The Godfather",
  "The Shawshank Redemption",
  "The Dark Knight",
  "Inception",
  "Add The Lord of the Rings",
  "Swap 1 4",
  "End"
]);
