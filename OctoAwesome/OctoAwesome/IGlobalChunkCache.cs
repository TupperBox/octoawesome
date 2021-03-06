﻿namespace OctoAwesome
{
    /// <summary>
    /// Basisinterface für einen Globalen Chunkcache
    /// </summary>
    public interface IGlobalChunkCache
    {
        /// <summary>
        /// Abonniert einen Chunk.
        /// </summary>
        /// <param name="planet">Die Id des Planeten</param>
        /// <param name="position">Position des Chunks</param>
        /// <returns>Den neu abonnierten Chunk</returns>
        IChunkColumn Subscribe(int planet,Index2 position);

        /// <summary>
        /// Liefert den Chunk, sofern geladen.
        /// </summary>
        /// <param name="planet">Die Id des Planeten</param>
        /// <param name="position">Die Position des zurückzugebenden Chunks</param>
        /// <returns>Chunk Instanz oder null, falls nicht geladen</returns>
        IChunkColumn Peek(int planet, Index2 position);

        /// <summary>
        /// Die Zahl der geladenen Chunks zurück
        /// </summary>
        int LoadedChunkColumns { get; }

        /// <summary>
        /// Anzahl der noch nicht gespeicherten ChunkColumns.
        /// </summary>
        int DirtyChunkColumn { get; }

        /// <summary>
        /// Gibt einen abonnierten Chunk wieder frei.
        /// </summary>
        /// <param name="planet">Die Id des Planeten</param>
        /// <param name="position">Die Position des freizugebenden Chunks</param>
        void Release(int planet,Index2 position);

        /// <summary>
        /// Löscht den gesamten Inhalt des Caches.
        /// </summary>
        void Clear();
    }
}
