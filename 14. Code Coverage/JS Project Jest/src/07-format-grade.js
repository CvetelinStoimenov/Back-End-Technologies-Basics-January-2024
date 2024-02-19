function formatGrade(grade) {
    'use strict';
    if (grade >= 0 && grade < 3.00) {
        return `Fail (2)`;
    } else if (grade >= 3.00 && grade < 3.50) {
        return `Poor (${grade.toFixed(2)})`;
    } else if (grade >= 3.50 && grade < 4.50) {
        return `Good (${grade.toFixed(2)})`;
    } else if (grade >= 4.50 && grade < 5.50) {
        return `Very good (${grade.toFixed(2)})`;
    } else if (grade >= 5.50 && grade <= 6) {
        return `Excellent (${grade.toFixed(2)})`;
    } else{
        return 'Enter valid grade!'
    }
}

module.exports = {
    formatGrade
}
