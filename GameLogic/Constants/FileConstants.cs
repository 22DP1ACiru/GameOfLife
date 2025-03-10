﻿namespace GameOfLife
{
    public static class FileConstants
    {
        /// <summary>
        /// The default folder where game saves are stored.
        /// </summary>
        public const string SaveFolder = "saves";

        /// <summary>
        /// The format for save file names.
        /// </summary>
        public const string SaveFileNameFormat = "{0}_{1:yyyyMMdd_HHmmss}.json";

        /// <summary>
        /// The format for parallel game save file names.
        /// </summary>
        public const string ParallelSaveFileNameFormat = "parallel_save_{0:yyyyMMdd_HHmmss}.json";
    }
}
