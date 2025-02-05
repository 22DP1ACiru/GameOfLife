namespace GameOfLife
{
    public interface IEngine
    {
        /// <summary>
        /// Starts the game loop with the specified update speed.
        /// </summary>
        /// <param name="updateSpeed">The speed at which the game updates, in milliseconds.</param>
        void Start(int updateSpeed);
    }
}
