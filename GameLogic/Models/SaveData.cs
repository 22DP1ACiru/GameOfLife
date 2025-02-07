namespace GameOfLife
{
    public class SaveData
    {
        public int Size { get; set; }
        public bool[,] Field { get; set; }
        public int IterationCount { get; set; }
    }
}