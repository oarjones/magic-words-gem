using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a player in the game.
/// </summary>
public class Player
{
    public string PlayerId { get; private set; }
    public int Score { get; set; }
    public List<string> PowerUps { get; private set; } // Lista de power-ups como strings
    // Podríamos añadir más propiedades como un color, un nombre, etc.

    public Player(string playerId)
    {
        PlayerId = playerId;
        Score = 0;
        PowerUps = new List<string>();
    }

    /// <summary>
    /// Adds a power-up to the player's inventory.
    /// </summary>
    /// <param name="powerUpId">The ID of the power-up to add.</param>
    public void AddPowerUp(string powerUpId)
    {
        PowerUps.Add(powerUpId);
    }

    /// <summary>
    /// Removes a power-up from the player's inventory.
    /// </summary>
    /// <param name="powerUpId">The ID of the power-up to remove.</param>
    public void RemovePowerUp(string powerUpId)
    {
        PowerUps.Remove(powerUpId);
    }

    /// <summary>
    /// Checks if the player has a specific power-up.
    /// </summary>
    /// <param name="powerUpId">The ID of the power-up to check for.</param>
    /// <returns>True if the player has the power-up, false otherwise.</returns>
    public bool HasPowerUp(string powerUpId)
    {
        return PowerUps.Contains(powerUpId);
    }
}