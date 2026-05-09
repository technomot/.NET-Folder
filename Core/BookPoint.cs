namespace Core
{
    public struct BookPoint
    {
        public int ShelfNumber;
        public int PositionNumber;

        public BookPoint(int shelf, int position)
        {
            ShelfNumber = shelf;
            PositionNumber = position;
        }

        public override string ToString()
        {
            return $"Shelf: {ShelfNumber}, Position: {PositionNumber}";
        }
    }
}