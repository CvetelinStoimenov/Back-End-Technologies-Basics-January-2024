import { analyzeArray} from "./04-array-analyzer.js";
import { expect } from "chai";

describe('Test the Array analyzer function.', () => {
    it('With not an array input should return undefined.', () => {
        //Arrange
        //Act
        const nanResult = analyzeArray(NaN);
        const undefinedResult = analyzeArray(undefined);
        const objectResult = analyzeArray({});
        const nullResult = analyzeArray(null);
        const floatNumberResult = analyzeArray(18.02);
        const numberResult = analyzeArray(123);
        const emptyResult = analyzeArray([]);
        const stringResult = analyzeArray('123');

        //Assert
        expect(nanResult).to.be.undefined;
        expect(undefinedResult).to.be.undefined;
        expect(objectResult).to.be.undefined;
        expect(nullResult).to.be.undefined;
        expect(floatNumberResult).to.be.undefined;
        expect(numberResult).to.be.undefined;    
        expect(emptyResult).to.be.undefined;
        expect(stringResult).to.be.undefined;   
    })

    it('Should return same number if the array is with one element.', () => {
        // Arrange
        const inputArray = [8];

        // Act
        const result = analyzeArray(inputArray);

        // Assert
        expect(result).to.deep.equal({min: 8, max: 8, length: 1})
    });

    it('Should return correct output if the array is correct.', () => {
        // Arrange
        const inputArray = [1, 2, 3, 4, 5, 6, 7, 8, 9, 0];

        // Act
        const result = analyzeArray(inputArray);

        // Assert
        expect(result).to.deep.equal({min: 0, max: 9, length: 10})
    });

    it('Should return correct output if the array is with equal elements.', () => {
        // Arrange
        const inputArray = [1, 2, 9, 4, 9, 6, 1, 8, 9, 1];

        // Act
        const result = analyzeArray(inputArray);

        // Assert
        expect(result).to.deep.equal({min: 1, max: 9, length: 10})
    });

    it('Should return correct output if the array is with negative elements.', () => {
        // Arrange
        const inputArray = [-1, -2, -3, -4, -5, -6, -7, -8, -9, 0];

        // Act
        const result = analyzeArray(inputArray);

        // Assert
        expect(result).to.deep.equal({min: -9, max: 0, length: 10})
    });
})