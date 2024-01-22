function fruit (fruit, weightGr, priceKg) {
    'use strict'
    
    console.log(`I need $${((weightGr/1000) * priceKg).toFixed(2)} to buy ${(weightGr / 1000).toFixed(2)} kilograms ${fruit}.`);
}

fruit('orange', 2500, 1.80)