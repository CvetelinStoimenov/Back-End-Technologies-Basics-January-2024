import { isSymmetric } from './02-check-for-symmetry.js';
import { expect } from 'chai';

describe('Test the Is Symmetric function', () => {
    it('Should return true if the array is empty', () => {
        // Arrange
        const inputArray = [];

        // Act
        const result = isSymmetric(inputArray);

        // Assert
        expect(result).to.be.true;
    });

    it('Should return false if a non-array is given', () => {
        // Arrange
        // Act
        const nanResult = isSymmetric(NaN);
        const undefinedResult = isSymmetric(undefined);
        const objectResult = isSymmetric({});
        const nullResult = isSymmetric(null);
        const stringResult = isSymmetric('string value');
        const numberResult = isSymmetric(123);

        // Assert
        expect(nanResult).to.be.equal(false);
        expect(undefinedResult).to.be.equal(false);
        expect(objectResult).to.be.equal(false);
        expect(nullResult).to.be.equal(false);
        expect(stringResult).to.be.equal(false);
        expect(numberResult).to.be.equal(false);

    });

    it('Should return true if a symmetric array is given', () => {
        // Arrange
        const inputArray = [3, 2, 1, 2, 3];

        // Act
        const result = isSymmetric(inputArray);

        // Assert
        expect(result).to.be.true;
    });

    it('Should return false if a non-symmetric array is given', () => {
        // Arrange
        const inputArray = [1, 2, 3, 4];

        // Act
        const result = isSymmetric(inputArray);

        // Assert
        expect(result).to.be.false;
    });

    it('Should return false for symmetric lookalike values', () => {
        // Arrange
        const inputArray = ['1', '2', '3', 2, 1];

        // Act
        const result = isSymmetric(inputArray);

        // Assert
        expect(result).to.be.false;
    });
});