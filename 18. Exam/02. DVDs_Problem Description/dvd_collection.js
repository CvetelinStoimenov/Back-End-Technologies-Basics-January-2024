function dvd_collection(dvds){
    'use strict'
    const numberOfDvds = parseInt(dvds[0]);
    const listOfDvdsNames = dvds.slice(1, numberOfDvds + 1);
    const commands = dvds.slice(numberOfDvds + 1);

    let allDvds = [...listOfDvdsNames];

    for (const command of commands) {
        
        if(command.startsWith("Watch")){

            if (allDvds.length > 0) {
                const [watchDVD, ...remainingDvd] = allDvds;
                allDvds = remainingDvd;
                console.log(`${watchDVD} DVD watched!`);
            }

        }else if(command.startsWith("Buy")){

            const dvdToAdd = command.split("Buy ")[1];
            allDvds.push(dvdToAdd);

        }else if(command.startsWith("Swap")){

            const [, startIndex, endIndex] = command.split(" ").map(Number);
            if (
                startIndex >= 0 &&
                endIndex >= 0 &&
                startIndex < allDvds.length &&
                endIndex < allDvds.length
            ) {
                // Swap the movie tickets at the given indices
                [allDvds[startIndex], allDvds[endIndex]] = [
                    allDvds[endIndex],
                    allDvds[startIndex],
                ];
                console.log("Swapped!");
            }

        }else if(command.startsWith("Done")){
            
            if (allDvds.length > 0) {
                console.log("DVDs left: " + allDvds.join(", "));
            } else {
                console.log("The collection is empty");
            }
            break;
        } else {
            break;
        }

    }
}

dvd_collection (['3', 'The Matrix', 'The Godfather', 'The Shawshank Redemption', 'Watch', 'Done', 'Swap 0 1'])
//dvd_collection (['5', 'The Lion King', 'Frozen', 'Moana', 'Toy Story', 'Shrek', 'Buy Coco', 'Swap 2 4', 'Done'])
//dvd_collection (['5', 'The Avengers', 'Iron Man', 'Thor', 'Captain America', 'Black Panther', 'Watch', 'Watch', 'Watch', 'Watch', 'Watch', 'Done']) 