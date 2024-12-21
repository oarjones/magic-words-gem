using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    private HexCell[,] _cells;

    /// <summary>
    /// Initializes the game board with the given dimensions.
    /// </summary>
    /// <param name="width">The width of the board (number of cells).</param>
    /// <param name="height">The height of the board (number of cells).</param>
    public void Initialize(int width, int height)
    {
        Width = width;
        Height = height;
        _cells = new HexCell[width, height];

        GenerateBoard();
    }

    /// <summary>
    /// Generates the game board, filling it with HexCells.
    /// </summary>
    private void GenerateBoard()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                // Aquí es dond eadaptaremos las coordenadas para simular un tablero hexagonal
                // Consideramos que las filas pares están desplazadas a la derecha

                _cells[x, y] = new HexCell(x, y);

                // Asignar letras aleatorias a las celdas (puedes modificar esto para usar una distribución de letras específica)
                _cells[x, y].Letter = GenerateRandomLetter();
            }
        }
    }

    /// <summary>
    /// Gets the HexCell at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns>The HexCell at the given coordinates, or null if out of bounds.</returns>
    public HexCell GetCell(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return _cells[x, y];
        }
        return null;
    }

    /// <summary>
    /// Gets the neighboring cells of the cell at the given coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the cell.</param>
    /// <param name="y">The y-coordinate of the cell.</param>
    /// <returns>A list of neighboring HexCells.</returns>
    public List<HexCell> GetNeighbors(int x, int y)
    {
        List<HexCell> neighbors = new List<HexCell>();

        // Adaptación para obtener vecinos en un tablero hexagonal
        // Las filas pares están desplazadas a la derecha

        int offset = y % 2 == 0 ? 0 : 1;

        AddNeighbor(neighbors, x - 1 + offset, y - 1); // Arriba a la izquierda
        AddNeighbor(neighbors, x + offset, y - 1);     // Arriba a la derecha
        AddNeighbor(neighbors, x - 1, y);           // Izquierda
        AddNeighbor(neighbors, x + 1, y);           // Derecha
        AddNeighbor(neighbors, x - 1 + offset, y + 1); // Abajo a la izquierda
        AddNeighbor(neighbors, x + offset, y + 1);     // Abajo a la derecha

        return neighbors;
    }

    private void AddNeighbor(List<HexCell> neighbors, int x, int y)
    {
        HexCell cell = GetCell(x, y);
        if (cell != null)
        {
            neighbors.Add(cell);
        }
    }

    /// <summary>
    /// Validates if a move from one cell to another is legal.
    /// </summary>
    /// <param name="from">The starting cell.</param>
    /// <param name="to">The destination cell.</param>
    /// <returns>True if the move is legal, false otherwise.</returns>
    public bool IsLegalMove(HexCell from, HexCell to)
    {
        // Si no se ha seleccionado ninguna celda previamente, cualquier movimiento es válido.
        if (from == null) return true;

        // Comprueba si la celda de destino está entre las adyacentes a la celda de origen
        List<HexCell> neighbors = GetNeighbors(from.X, from.Y);
        return neighbors.Contains(to);
    }

    /// <summary>
    /// Generates a random letter (you can adjust the distribution as needed).
    /// </summary>
    /// <returns>A random uppercase letter.</returns>
    private char GenerateRandomLetter()
    {
        // Ejemplo simple: distribución uniforme de letras
        return (char)('A' + Random.Range(0, 26));
    }
}