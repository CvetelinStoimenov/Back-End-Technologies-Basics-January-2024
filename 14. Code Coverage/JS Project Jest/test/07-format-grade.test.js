const { formatGrade } = require('../src/07-format-grade.js')

describe('formatGrade', () => {
    it('should show failed grade', () => {
        expect(formatGrade(2)).toBe("Fail (2)");
    });

    it('should show Poor grade', () => {
        expect(formatGrade(3.1)).toBe("Poor (3.10)");
    });

    it('should show Good grade', () => {
        expect(formatGrade(3.55)).toBe("Good (3.55)");
    });

    it('should show Very good grade', () => {
        expect(formatGrade(4.55)).toBe("Very good (4.55)");
    });

    it('should show Excellent grade', () => {
        expect(formatGrade(5.55)).toBe("Excellent (5.55)");
    });


    it('should show Excellent grade', () => {
        expect(formatGrade(80.55)).toBe("Enter valid grade!");
    });
  }); 