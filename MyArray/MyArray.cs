namespace SimpleArray
{
    public class MyArray
    {
        private readonly int[] _myArr;
        public int[] Array => _myArr;

        public MyArray(int size)
        {
            _myArr = Enumerable.Range(0, size).ToArray();
        }

        public bool Replace(int position, int newValue)
        {
            if (position < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Position must not be less than zero");
            }

            if (position >  _myArr.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Position must be valid");
            }

            _myArr[position] = newValue;
            return true;
        }
    }
}
