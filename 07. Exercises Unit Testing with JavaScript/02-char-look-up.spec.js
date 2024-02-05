import { lookupChar } from "./02-char-look-up.js";
import { expect } from "chai";
describe('Test the Lookup char function.', () => {
    it('With not string input and number index should return undefined.', () => {
        //Arrange
        //Act
        const nanResult = lookupChar(NaN, '1');
        const undefinedResult = lookupChar(undefined, 1);
        const objectResult = lookupChar({}, 1);
        const nullResult = lookupChar(null, 1);
        const floatNumberResult = lookupChar(18.02, 1);
        const numberResult = lookupChar(123, 1);

        //Assert
        expect(nanResult).to.be.undefined;
        expect(undefinedResult).to.be.undefined;
        expect(objectResult).to.be.undefined;
        expect(nullResult).to.be.undefined;
        expect(floatNumberResult).to.be.undefined;
        expect(numberResult).to.be.undefined
    })

    it('With not string input and string index should return undefined.', () => {
        //Arrange
        //Act
        const nanResult = lookupChar(NaN, '1');
        const undefinedResult = lookupChar(undefined, '1');
        const objectResult = lookupChar({}, '1');
        const nullResult = lookupChar(null, '1');
        const floatNumberResult = lookupChar(18.02, '1');
        const numberResult = lookupChar(123, '1');

        //Assert
        expect(nanResult).to.be.undefined;
        expect(undefinedResult).to.be.undefined;
        expect(objectResult).to.be.undefined;
        expect(nullResult).to.be.undefined;
        expect(floatNumberResult).to.be.undefined;
        expect(numberResult).to.be.undefined
    })

    it('With correct string input and string index should return lookup char.', () => {
        //Arrange
        const stringInput = 'Test input';
        const index = 1;

        //Act
        const result = lookupChar(stringInput, index);

        //Assert
        expect(result).to.be.equals('e')
    })

    it('With correct string input and white space index should return undefined.', () => {
                //Arrange
                const stringInput = 'Test input';
                const index = ' 1 ';
        
                //Act
                const result = lookupChar(stringInput, index);
        
                //Assert
                expect(result).to.be.undefined;
    })

    it('With correct string input and negative index should return "Incorrect index".', () => {
        //Arrange
        const stringInput = 'Test input';
        const index = -5;

        //Act
        const result = lookupChar(stringInput, index);

        //Assert
        expect(result).to.be.equals('Incorrect index')
    })

    it('With short string input and larger index should return "Incorrect index".', () => {
        //Arrange
        const stringInput = 'Test input';
        const index = 10;

        //Act
        const result = lookupChar(stringInput, index);

        //Assert
        expect(result).to.be.equals('Incorrect index')
    })

    it('With correct string input and index 0 should return first char.', () => {
        //Arrange
        const stringInput = 'Test input';
        const index = 0;

        //Act
        const result = lookupChar(stringInput, index);

        //Assert
        expect(result).to.be.equals('T')
    })

    it('With correct string input and index equal to string length should return last char.', () => {
        //Arrange
        const stringInput = 'Test input';
        const index = stringInput.length - 1;

        //Act
        const result = lookupChar(stringInput, index);

        //Assert
        expect(result).to.be.equals('t')
    })

    it('With correct string input and string index should return undefined.', () => {
        //Arrange
        const stringInput = 'Test input';
        const index = '1';

        //Act
        const result = lookupChar(stringInput, index);

        //Assert
        expect(result).to.be.undefined;
    })

    it('With correct string input and undefined index should return undefined.', () => {
        //Arrange
        const stringInput = 'Test input';
        const index = undefined;

        //Act
        const result = lookupChar(stringInput, index);

        //Assert
        expect(result).to.be.undefined;
    })

    it('With correct string input and not number index should return undefined.', () => {
        //Arrange
        //Act
        const nanResult = lookupChar('Test input', NaN);
        const undefinedResult = lookupChar('Test input', undefined);
        const objectResult = lookupChar('Test input', {});
        const nullResult = lookupChar('Test input', null);
        const floatNumberResult = lookupChar('Test input', 18.02);

        //Assert
        expect(nanResult).to.be.undefined;
        expect(undefinedResult).to.be.undefined;
        expect(objectResult).to.be.undefined;
        expect(nullResult).to.be.undefined;
        expect(floatNumberResult).to.be.undefined;
    })
})