import {mathEnforcer} from './03-math-enforcer.js';
import { expect } from 'chai';

describe('Test the Math enforce function.', () => {
    describe('Test the Add Five function', () => {
        it('With not number input should return undefined.', () => {
        //Arrange
        //Act
        // const nanResult = mathEnforcer.addFive(NaN);
        const undefinedResult = mathEnforcer.addFive(undefined);
        const objectResult = mathEnforcer.addFive({});
        const nullResult = mathEnforcer.addFive(null);
        const emptyResult = mathEnforcer.addFive([]);
        const stringResult = mathEnforcer.addFive('123');

        //Assert
        // expect(nanResult).to.be.undefined;
        expect(undefinedResult).to.be.undefined;
        expect(objectResult).to.be.undefined;
        expect(nullResult).to.be.undefined;   
        expect(emptyResult).to.be.undefined;
        expect(stringResult).to.be.undefined;   
        })

        it('With valid number input should return valid result', () => {
            //Arrange
            const inputNumber = 5;

            //Act
            const result = mathEnforcer.addFive(inputNumber);

            //Assert
            expect(result).to.be.equal(10);   
            })

            it('With floating-point number input should return valid result', () => {
                //Arrange
                const inputNumber = 5.5;
    
                //Act
                const result = mathEnforcer.addFive(inputNumber);
    
                //Assert
                expect(result).to.be.equal(10.50);   
                })

                it('With negative number input should return valid result', () => {
                    //Arrange
                    const inputNumber = -5;
        
                    //Act
                    const result = mathEnforcer.addFive(inputNumber);
        
                    //Assert
                    expect(result).to.be.equal(0);   
                    })

                    it('With zero input should return valid result', () => {
                        //Arrange
                        const inputNumber = 0;
            
                        //Act
                        const result = mathEnforcer.addFive(inputNumber);
            
                        //Assert
                        expect(result).to.be.equal(5);   
                        })

                        it('With 0.01 input should return valid result', () => {
                            //Arrange
                            const inputNumber = 0.01;
                
                            //Act
                            const result = mathEnforcer.addFive(inputNumber);
                
                            //Assert
                            expect(result).to.be.equal(5.01);   
                            })

                            
                        it('With 0.00001 input should return valid result', () => {
                            //Arrange
                            const inputNumber = 0.00001;
                
                            //Act
                            const result = mathEnforcer.addFive(inputNumber);
                
                            //Assert
                            expect(result).to.be.equal(5.00001);   
                            })
    })

    describe('Test the subtractTen function', () => {
        it('With not number input should return undefined.', () => {
        //Arrange
        //Act
        // const nanResult = mathEnforcer.addFive(NaN);
        const undefinedResult = mathEnforcer.subtractTen(undefined);
        const objectResult = mathEnforcer.subtractTen({});
        const nullResult = mathEnforcer.subtractTen(null);
        const emptyResult = mathEnforcer.subtractTen([]);
        const stringResult = mathEnforcer.subtractTen('123');

        //Assert
        // expect(nanResult).to.be.undefined;
        expect(undefinedResult).to.be.undefined;
        expect(objectResult).to.be.undefined;
        expect(nullResult).to.be.undefined;   
        expect(emptyResult).to.be.undefined;
        expect(stringResult).to.be.undefined;   
        })

        it('With valid number input should return valid result', () => {
            //Arrange
            const inputNumber = 15;

            //Act
            const result = mathEnforcer.subtractTen(inputNumber);

            //Assert
            expect(result).to.be.equal(5);   
            })

            it('With floating-point number input should return valid result', () => {
                //Arrange
                const inputNumber = 15.5;
    
                //Act
                const result = mathEnforcer.subtractTen(inputNumber);
    
                //Assert
                expect(result).to.be.equal(5.50);   
                })

                it('With negative number input should return valid result', () => {
                    //Arrange
                    const inputNumber = -10;
        
                    //Act
                    const result = mathEnforcer.subtractTen(inputNumber);
        
                    //Assert
                    expect(result).to.be.equal(-20);   
                    })

                    it('With zero input should return valid result', () => {
                        //Arrange
                        const inputNumber = 0;
            
                        //Act
                        const result = mathEnforcer.subtractTen(inputNumber);
            
                        //Assert
                        expect(result).to.be.equal(-10);   
                        })

                        it('With 0.01 input should return valid result', () => {
                            //Arrange
                            const inputNumber = 0.01;
                
                            //Act
                            const result = mathEnforcer.subtractTen(inputNumber);
                
                            //Assert
                            expect(result).to.be.equal(-9.99);   
                            })

                            it('With 0.00001 input should return valid result', () => {
                                //Arrange
                                const inputNumber = 0.00001;
                    
                                //Act
                                const result = mathEnforcer.subtractTen(inputNumber);
                    
                                //Assert
                                expect(result).to.be.equal(-9.99999);   
                                })
    })

    describe('Test the Sum function', () => {
        it('With not numbers input should return undefined.', () => {
        //Arrange
        //Act
        // const nanResult = mathEnforcer.addFive(NaN);
        const undefinedResult = mathEnforcer.sum(undefined, undefined);
        const objectResult = mathEnforcer.sum({}, {});
        const nullResult = mathEnforcer.sum(null, null);
        const emptyResult = mathEnforcer.sum([], []);
        const stringResult = mathEnforcer.sum('123', '123');

        //Assert
        // expect(nanResult).to.be.undefined;
        expect(undefinedResult).to.be.undefined;
        expect(objectResult).to.be.undefined;
        expect(nullResult).to.be.undefined;   
        expect(emptyResult).to.be.undefined;
        expect(stringResult).to.be.undefined;   
        })

        it('With one not number input should return undefined.', () => {
            //Arrange
            //Act
            // const nanResult = mathEnforcer.addFive(NaN);
            const undefinedResult = mathEnforcer.sum(5, undefined);
            const objectResult = mathEnforcer.sum(8, {});
            const nullResult = mathEnforcer.sum(null, 9);
            const emptyResult = mathEnforcer.sum([], 15);
            const stringResult = mathEnforcer.sum(123, '123');
    
            //Assert
            // expect(nanResult).to.be.undefined;
            expect(undefinedResult).to.be.undefined;
            expect(objectResult).to.be.undefined;
            expect(nullResult).to.be.undefined;   
            expect(emptyResult).to.be.undefined;
            expect(stringResult).to.be.undefined;   
            })

        it('With valid numbers input should return valid result', () => {
            //Arrange
            const inputNumberA = 15;
            const inputNumberB = 5;

            //Act
            const result = mathEnforcer.sum(inputNumberA, inputNumberB);

            //Assert
            expect(result).to.be.equal(20);   
            })

            it('With floating-point number input should return valid result', () => {
                //Arrange
                const inputNumberA = 15.5;
                const inputNumberB = 5;
    
                //Act
                const result = mathEnforcer.sum(inputNumberA, inputNumberB);
    
                //Assert
                expect(result).to.be.equal(20.50);   
                })

                it('With negative number input should return valid result', () => {
                    //Arrange
                    const inputNumberA = -5;
                    const inputNumberB = 20;
        
                    //Act
                    const result = mathEnforcer.sum(inputNumberA, inputNumberB);
        
                    //Assert
                    expect(result).to.be.equal(15);   
                    })

                    it('With zeros input should return valid result', () => {
                        //Arrange
                        const inputNumberA = 0;
                        const inputNumberB = 0;
            
                        //Act
                        const result = mathEnforcer.sum(inputNumberA, inputNumberB);
            
                        //Assert
                        expect(result).to.be.equal(0);   
                        })

                        it('With 0.01 input should return valid result', () => {
                            //Arrange
                            const inputNumberA = 0.01;
                            const inputNumberB = 0.01;
                
                            //Act
                            const result = mathEnforcer.sum(inputNumberA, inputNumberB);
                
                            //Assert
                            expect(result).to.be.equal(0.02);   
                            })


                            it('With 0.0000001 input should return valid result', () => {
                                //Arrange
                                const inputNumberA = 0.0000001;
                                const inputNumberB = 0.0000001;
                    
                                //Act
                                const result = mathEnforcer.sum(inputNumberA, inputNumberB);
                    
                                //Assert
                                expect(result).to.be.equal(0.0000002);   
                                })
    })
})