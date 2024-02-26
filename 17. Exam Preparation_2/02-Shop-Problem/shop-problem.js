function shop (products){
    'use strict'

    const numberOfProducts = parseInt(products[0]);

    const listOfProducts = products.slice(1, numberOfProducts + 1);
    const commands = products.slice(numberOfProducts + 1);

    let allProducts = [...listOfProducts];

    for (const command of commands) {

        if(command.startsWith("Sell")){

            if (allProducts.length > 0) {
                const [soldProduct, ...remainingProduct] = allProducts;
                allProducts = remainingProduct;
                console.log(`${soldProduct} product sold!`);
            }

        } else if(command.startsWith("Add")){

            const productToAdd = command.split("Add ")[1];
            allProducts.push(productToAdd);

        } else if(command .startsWith("Swap")){

            const [, startIndex, endIndex] = command.split(" ").map(Number);
            if (
                startIndex >= 0 &&
                endIndex >= 0 &&
                startIndex < allProducts.length &&
                endIndex < allProducts.length
            ) {
                // Swap the movie tickets at the given indices
                [allProducts[startIndex], allProducts[endIndex]] = [
                    allProducts[endIndex],
                    allProducts[startIndex],
                ];
                console.log("Swapped!");
            }
        } else if(command === "End"){

            if (allProducts.length > 0) {
                console.log("Products left: " + allProducts.join(", "));
            } else {
                console.log("The shop is empty");
            }
            break;
        }  

    }
}

shop(['3', 'Apple', 'Banana', 'Orange', 'Sell', 'End', 'Swap 0 1'])
// Apple product sold! 
// Products left: Banana, Orange

shop(['5', 'Milk', 'Eggs', 'Bread', 'Cheese', 'Butter', 'Add Yogurt', 'Swap 1 4', 'End']) 
// Swapped!
// Products left: Milk, Butter, Bread, Cheese, Eggs, Yogurt

shop(['3', 'Shampoo', 'Soap', 'Toothpaste', 'Sell', 'Sell', 'Sell', 'End'])
// Shampoo product sold! 
// Soap product sold! 
// Toothpaste product sold! 
// The shop is empty
