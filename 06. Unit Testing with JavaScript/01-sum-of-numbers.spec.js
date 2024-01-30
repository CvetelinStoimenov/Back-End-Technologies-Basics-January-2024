import {sum} from './01-sum-of-numbers.js';
import { expect } from 'chai';

describe('Test the Sum numbers function', () => {
    it('Should return 0 if an empty array is given.', () => {
        // Arrange
        const inputArray = [];

        // Act
        const result = sum(inputArray);

        // Assert
        expect(result).to.be.equal(0);

    });
    
    it('Should return same number if the array is with one number.', () => {
        // Arrange
        const inputArray = [8];

        // Act
        const result = sum(inputArray);

        // Assert
        expect(result).to.be.equal(8);
    });
    
    it('Should return correct sum if all numbers are positive', () => {
        // Arrange
        const inputArray = [1, 2, 3, 4, 5, 6]

        // Act
        const result = sum(inputArray);

        // Assert
        expect(result).to.be.equal(21);
    });

    it('Should return correct sum if all numbers are negative', () => {
        // Arrange
        const inputArray = [-1, -2, -3, -4, -5, -6]

        // Act
        const result = sum(inputArray);

        // Assert
        expect(result).to.be.equal(-21);
    });

    it('Should return correct sum if numbers are mixed', () => {
        // Arrange
        const inputArray = [-1, -2, -3, -4, 5, 6]

        // Act
        const result = sum(inputArray);

        // Assert
        expect(result).to.be.equal(1);
    });

    it('Should return correct sum if numbers are floating', () => {
        // Arrange
        const inputArray = [2.5, 1.5, 3.6]

        // Act
        const result = sum(inputArray);

        // Assert
        expect(result).to.be.equal(7.6);
    });
});