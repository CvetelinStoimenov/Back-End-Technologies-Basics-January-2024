import { isOddOrEven } from "./01-is-odd-or-even.js";
import { expect } from "chai";

describe('Test the Is Odd Or Even function.', () => {
    
    it('With not string input should return undefined.', () => {
        //Arrange
        //Act
        const nanResult = isOddOrEven(NaN);
        const undefinedResult = isOddOrEven(undefined);
        const objectResult = isOddOrEven({});
        const nullResult = isOddOrEven(null);
        const floatNumberResult = isOddOrEven(18.02);
        const numberResult = isOddOrEven(123);

        //Assert
        expect(nanResult).to.be.undefined;
        expect(undefinedResult).to.be.undefined;
        expect(objectResult).to.be.undefined;
        expect(nullResult).to.be.undefined;
        expect(floatNumberResult).to.be.undefined;
        expect(numberResult).to.be.undefined;        
    })

    it('With odd input should return odd result.', () => {
        //Arrange
        const input = 'Odd input';

        //Act
        const result = isOddOrEven(input);

        //Assert
        expect(result).to.be.equal('odd')
    })

    it('With Even input should return even result.', () => {
        //Arrange
        const input = 'Even input';

        //Act
        const result = isOddOrEven(input);

        //Assert
        expect(result).to.be.equal('even')      
    })

    it('With empty input should return even result.', () => {
        //Arrange
        const input = '';

        //Act
        const result = isOddOrEven(input);

        //Assert
        expect(result).to.be.equal('even')           
    })
})