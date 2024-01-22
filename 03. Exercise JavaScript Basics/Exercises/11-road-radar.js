function roadRadar(speed, area) {
    'use strict'
    let message = '';
    if (area === "motorway") {
        let diff = speed - 130;

        if (diff <= 0) {
            message = `Driving ${speed} km/h in a 130 zone`;
        } else if (diff >= 0 && diff <= 20) {
            message = `The speed is ${diff} km/h faster than the allowed speed of 130 - speeding`;
        } else if (diff > 20 && diff <= 40) {
            message = `The speed is ${diff} km/h faster than the allowed speed of 130 - excessive speeding`;
        } else {
            message = `The speed is ${diff} km/h faster than the allowed speed of 130 - reckless driving`;
        }
    } else if (area === "interstate") {
        let diff = speed - 90;

        if (diff <= 0) {
            message = `Driving ${speed} km/h in a 90 zone`;
        } else if (diff >= 0 && diff <= 20) {
            message = `The speed is ${diff} km/h faster than the allowed speed of 90 - speeding`;
        } else if (diff > 20 && diff <= 40) {
            message = `The speed is ${diff} km/h faster than the allowed speed of 90 - excessive speeding`;
        } else {
            message = `The speed is ${diff} km/h faster than the allowed speed of 90 - reckless driving`;
        }
    } else if (area === "city") {
        let diff = speed - 50;

        if (diff <= 0) {
            message = `Driving ${speed} km/h in a 50 zone`;
        } else if (diff >= 0 && diff <= 20) {
            message = `The speed is ${diff} km/h faster than the allowed speed of 50 - speeding`;
        } else if (diff > 20 && diff <= 40) {
            message = `The speed is ${diff} km/h faster than the allowed speed of 50 - excessive speeding`;
        } else {
            message = `The speed is ${diff} km/h faster than the allowed speed of 50 - reckless driving`;
        }
    } else if (area === "residential") {
        let diff = speed - 20;

        if (diff <= 0) {
            message = `Driving ${speed} km/h in a 20 zone`;
        } else if (diff >= 0 && diff <= 20) {
            message = `The speed is ${diff} km/h faster than the allowed speed of 20 - speeding`;
        } else if (diff > 20 && diff <= 40) {
            message = `The speed is ${diff} km/h faster than the allowed speed of 20 - excessive speeding`;
        } else {
            message = `The speed is ${diff} km/h faster than the allowed speed of 20 - reckless driving`;
        }
    }
    console.log(message);
}

roadRadar(21, 'residential')