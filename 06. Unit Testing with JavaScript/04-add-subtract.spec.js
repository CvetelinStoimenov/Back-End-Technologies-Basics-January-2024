import {createCalculator} from './04-add-subtract.js';
import { expect } from 'chai';

describe('Test Add subtract function.', () => {
    it('Should return 0 if no operation are executed on the calculator.', () => {
        // Arrange
        const calculator = createCalculator();
    
        // Act
        const result = calculator.get();
    
        // Assert
        expect(result).to.equals(0);
    });

    it('Should return a negative number if only subtract operations are executed with positive numbers on the calculator.', () => {
        // Arrange
        const calculator = createCalculator();
    
        // Act
        calculator.subtract(3);
        calculator.subtract(3);
        calculator.subtract(10);
        const result = calculator.get();
    
        // Assert
        expect(result).to.equals(-16);
    });

    it('Should return a positive number if only subtract operations are executed with negative numbers on the calculator.', () => {
        // Arrange
        const calculator = createCalculator();
    
        // Act
        calculator.subtract(-3);
        calculator.subtract(-3);
        calculator.subtract(-3);
        const result = calculator.get();
    
        // Assert
        expect(result).to.equals(9);
    });
    it('Should return positive number if only add operations are executed with positive numbers on the calculator.', () => {
        // Arrange
        const calculator = createCalculator();
    
        // Act
        calculator.add(2);
        calculator.add(2);
        calculator.add(100);
        const result = calculator.get();
    
        // Assert
        expect(result).to.equals(104);
    });

    it('Should return negative number if only add operations are executed with positive numbers on the calculator.', () => {
        // Arrange
        const calculator = createCalculator();
    
        // Act
        calculator.add(-5);
        calculator.add(-5);
        calculator.add(-5);
        const result = calculator.get();
    
        // Assert
        expect(result).to.equals(-15);
    });

    it('Should return correct number if mixed operations are executed on the calculator.', () => {
        // Arrange
        const calculator = createCalculator();
    
        // Act
        calculator.add(2);
        calculator.add(2);
        calculator.add(100);
        calculator.add(-4);
        calculator.subtract(-100);
        calculator.subtract(200);
        const result = calculator.get();
    
        // Assert
        expect(result).to.equals(0);
    });

    it('Should return correct number if string are given on the calculator.', () => {
        // Arrange
        const calculator = createCalculator();
    
        // Act
        calculator.add('2')
        calculator.add('2')
        calculator.add('100')
        calculator.subtract('104')
        const result = calculator.get();
    
        // Assert
        expect(result).to.equals(0);
    });

    it('Should return the same value if zero is subtracted from a non-zero value.', () => {
        // Arrange
        const calculator = createCalculator();
        calculator.add(10);

        // Act
        calculator.subtract(0);
        const result = calculator.get();

        // Assert
        expect(result).to.equals(10);
    });

    it('Should return the same value if zero is added to a non-zero value.', () => {
        // Arrange
        const calculator = createCalculator();
        calculator.add(5);

        // Act
        calculator.add(0);
        const result = calculator.get();

        // Assert
        expect(result).to.equals(5);
    });

    it('Should return the correct result when subtracting a floating-point number from an integer.', () => {
        // Arrange
        const calculator = createCalculator();

        // Act
        calculator.subtract(3);
        calculator.subtract(2.5);
        const result = calculator.get();

        // Assert
        expect(result).to.equals(-5.5);
    });
});