function theatrePromotions(day, age) {
    'use strict';
    let result;

    if (day === 'Weekday') {
        if (age >= 0 && age <= 18) {
            result = '12$';
        } else if (age > 18 && age <= 64) {
            result = '18$';
        } else if (age > 64 && age <= 122) {
            result = '12$';
        } else if (age < 0 || age > 122) {
            result = 'Error!';
        }
    } else if (day === 'Weekend') {
        if (age >= 0 && age <= 18) {
            result = '15$';
        } else if (age > 18 && age <= 64) {
            result = '20$';
        } else if (age > 64 && age <= 122) {
            result = '15$';
        } else if (age < 0 || age > 12 ) {
            result = 'Error!';
        }
    } else if (day === 'Holiday') {
        if (age >= 0 && age <= 18) {
            result = '5$';
        } else if (age > 18 && age <= 64) {
            result = '12$';
        } else if (age > 64 && age <= 122) {
            result = '10$';
        } else if (age < 0 || age > 122) {
            result = 'Error!';
        }
    }

    console.log(result);
}

theatrePromotions('Holiday', -1);


function oneTheatrePromotions(day, age) {
    'use strict'
    let result;
    
    if (age >= 0 && age <= 18) {
        if (day === 'Weekday') {
            result = '12$';
        } else if (day === 'Weekend') {
            result = '15$';
        } else if (day === 'Holiday') {
            result = '5$';
        }
    } else if (age > 18 && age <= 64) {
        if (day === 'Weekday') {
            result = '18$';
        } else if (day === 'Weekend') {
            result = '20$';
        } else if (day === 'Holiday') {
            result = '12$';
        }
    } else if (age > 64 && age <= 122) {
        if (day === 'Weekday') {
            result = '12$';
        } else if (day === 'Weekend') {
            result = '15$';
        } else if (day === 'Holiday') {
            result = '10$';
        }
    } else {
        result = 'Error!'
    }

    console.log(result);
}

oneTheatrePromotions('Holiday', 15);


