function extractOddOccurrences(sentence) {
    const words = sentence.toLowerCase().split(' '); // Split the sentence into lowercase words
    const wordCounts = {}; // Create an empty object to store word counts

    // Count occurrences of each word
    words.forEach(word => {
        if (wordCounts[word]) {
            wordCounts[word]++; // Increment the count if the word exists
        } else {
            wordCounts[word] = 1; // Initialize the count to 1 if the word doesn't exist
        }
    });

    // Filter out words with odd occurrences
    const oddOccurrences = Object.keys(wordCounts).filter(word => wordCounts[word] % 2 !== 0);

    // Print the result
    console.log(oddOccurrences.join(' ')); // Join the words with odd occurrences and print
}

// Example usage:
const input = 'Java C# Php PHP Java PhP 3 C# 3 1 5 C#';
extractOddOccurrences(input);

