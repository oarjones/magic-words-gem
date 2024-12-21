using UnityEngine;

/// <summary>
/// Represents a single hexagonal cell on the game board.
/// </summary>
public class HexCell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public char Letter { get; set; }
    public string OwnerPlayerId { get; set; } // ID del jugador propietario de la celda, si corresponde

    // Referencia al prefab de la celda (botón)
    public GameObject CellPrefab { get; set; }

    public HexCell(int x, int y)
    {
        X = x;
        Y = y;
        Letter = ' '; // Inicialmente, la celda no tiene letra
        OwnerPlayerId = null; // Inicialmente, la celda no tiene propietario
    }
}