function createHeroRegister(data) {
    const heroes = [];

    // Loop through each item of the data array
    for (const item of data) {
        // Split each item into its components: heroName, heroLevel, and itemsString
        const [heroName, heroLevel, itemsString] = item.split(' / ');
        
        // Split itemsString into an array of items
        const items = itemsString.split(', ');

        // Create a hero object with name, level, and items
        const hero = {
            name: heroName,
            level: Number(heroLevel),
            items: items
        };

        // Push the hero object into the heroes array
        heroes.push(hero);
    }

    // Sort the heroes array by level in ascending order
    heroes.sort((a, b) => a.level - b.level);

    // Print information for each hero
    heroes.forEach(hero => {
        console.log(`Hero: ${hero.name}`);
        console.log(`level => ${hero.level}`);
        console.log(`items => ${hero.items.join(', ')}`);
    });
}

// Example usage:
const input = [
    'Isacc / 25 / Apple, GravityGun',
    'Derek / 12 / BarrelVest, DestructionSword',
    'Hes / 1 / Desolator, Sentinel, Antara'
];

createHeroRegister(input);