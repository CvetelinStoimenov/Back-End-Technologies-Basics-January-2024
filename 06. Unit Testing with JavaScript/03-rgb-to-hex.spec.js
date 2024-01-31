import {rgbToHexColor} from './03-rgb-to-hex.js'
import { expect } from 'chai';

describe('Test the RGB to HEX function.', () => {
    it('Should return undefine if the parameters are not in the expected range.', ()=> {
        // Arrange
        const redBelowLowerBoundary = -1;
        const greenBelowLowerBoundary = -1;
        const  blueBelowLowerBoundary = -1;
        const redAboveUpperBoundary = 256;
        const greenAboveUpperBoundary = 256;
        const  blueAboveUpperBoundary = 256;

        // Act
        const resultBelowLowerBoundary = rgbToHexColor(redBelowLowerBoundary, greenBelowLowerBoundary, blueBelowLowerBoundary);
        const resultAboveUpperBoundary = rgbToHexColor(redAboveUpperBoundary, greenAboveUpperBoundary, blueAboveUpperBoundary);

        // Assert
        expect(resultBelowLowerBoundary).to.be.undefined;
        expect(resultAboveUpperBoundary).to.be.undefined;
    });

    it('Should return undefine if the parameters are strings.', ()=> {
                // Arrange
        const red = '1';
        const green = '1';
        const  blue = '1';
        
        // Act
        const result = rgbToHexColor(red, blue, green);

        // Assert
        expect(result).to.be.undefined;
        
    });

    it('Should return undefine if the parameters are null.', ()=> {
        // Arrange
        const red = null;
        const green = null;
        const  blue = null;
        
        // Act
        const result = rgbToHexColor(red, blue, green);

        // Assert
        expect(result).to.be.undefined;

    });

    it('Should return undefine if the parameters are undefined.', ()=> {
        // Arrange
        const red = undefined;
        const green = undefined;
        const  blue = undefined;
        
        // Act
        const result = rgbToHexColor(red, blue, green);

        // Assert
        expect(result).to.be.undefined;

    });

    it('Should return correct Hex if the parameters are in the range.', ()=> {
        // Arrange
        const red = 1 ;
        const green = 2 ;
        const  blue = 5 ;
        
        // Act
        const result = rgbToHexColor(red, blue, green);

        // Assert
        expect(result).to.be.equal('#010502');

    });

    it('Should return correct Hex if the parameters are in the lower boundary.', ()=> {
        // Arrange
        const red = 0;
        const green = 0;
        const  blue = 0;
        
        // Act
        const result = rgbToHexColor(red, blue, green);

        // Assert
        expect(result).to.be.equal('#000000');

    });

    it('Should return correct Hex if the parameters are in the upper boundary.', ()=> {
        // Arrange
        const red = 255;
        const green = 255;
        const  blue = 255;
        
        // Act
        const result = rgbToHexColor(red, blue, green);

        // Assert
        expect(result).to.be.equal('#FFFFFF');

    });

    it('Should return undefine if green value is invalid.', ()=> {
        // Arrange
        // Act
        const nonNumericGreenValueResult = rgbToHexColor(0, '123', 0);
        const negativeGreenValueResult = rgbToHexColor(0, -123, 0);
        const toBigGreenValueResult = rgbToHexColor(0, 999, 0);

        // Assert
        expect(nonNumericGreenValueResult).to.be.undefined;
        expect(negativeGreenValueResult).to.be.undefined;
        expect(toBigGreenValueResult).to.be.undefined;
    });

    it('Should return undefine if blue value is invalid.', ()=> {
        // Arrange
        // Act
        const nonNumericBlueValueResult = rgbToHexColor(0, 0, '123');
        const negativeBlueValueResult = rgbToHexColor(0, 0, -123);
        const toBigBlueValueResult = rgbToHexColor(0, 0, 999);

        // Assert
        expect(nonNumericBlueValueResult).to.be.undefined;
        expect(negativeBlueValueResult).to.be.undefined;
        expect(toBigBlueValueResult).to.be.undefined;
    });

    it('Should return undefine if red value is invalid.', ()=> {
        // Arrange
        // Act
        const nonNumericRedValueResult = rgbToHexColor('123', 0, 0);
        const negativeRedValueResult = rgbToHexColor(-123, 0, 0);
        const toBigRedValueResult = rgbToHexColor(999, 0, 0);

        // Assert
        expect(nonNumericRedValueResult).to.be.undefined;
        expect(negativeRedValueResult).to.be.undefined;
        expect(toBigRedValueResult).to.be.undefined;
    });
});
