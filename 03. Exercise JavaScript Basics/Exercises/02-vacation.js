function vacation(people, groupType, day) {
    'use strict';
    let price;
    let totalPrice;

    if (groupType === "Students") {
        if (day === "Friday") {
            price = 8.45;
        } else if (day === "Saturday") {
            price = 9.80;
        } else {
            price = 10.46;
        }
    } else if (groupType === "Business") {
        if (day === "Friday") {
            price = 10.90;
        } else if (day === "Saturday") {
            price = 15.60;
        } else {
            price = 16;
        }
    } else {
        if (day === "Friday") {
            price = 15;
        } else if (day === "Saturday") {
            price = 20;
        } else {
            price = 22.50;
        }
    }

    totalPrice = people * price;

    if (groupType === "Students" && people >= 30) {
        totalPrice = totalPrice * 0.85;
    } else if (groupType === "Business" && people >= 100) {
        totalPrice = totalPrice - (10 * price);
    } else if (groupType === "Regular" && people >= 10 && people <= 20) {
        totalPrice = totalPrice * 0.95;
    }

    console.log(`Total price: ${totalPrice.toFixed(2)}`);

}

vacation(15, "Regular", "Saturday")