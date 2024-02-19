const { add, subtract } = require('../src/index.js')
describe('Add function', () => {
  it('should add two numbers', () => {
    expect(add(2, 3)).toBe(5);
  });
}); 

describe('subtract function', () => {
  it('should subtract two numbers', () => {
    expect(subtract(10, 5)).toBe(5);
  });
}); 
