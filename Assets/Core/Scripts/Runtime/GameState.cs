using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents the current state of the game.
/// </summary>
public class GameState : MonoBehaviour
{
    public enum GameStatus
    {
        Waiting,
        Playing,
        Finished
    }

    public GameStatus Status { get; private set; }
    public Board GameBoard { get; private set; } // Referencia al tablero
    public Dictionary<string, Player> Players { get; private set; } // Información de los jugadores
    public string CurrentPlayerId { get; private set; } // ID del jugador actual
    public string CurrentWord { get; private set; }
    public List<string> WordHistory { get; private set; }

    private void Awake()
    {
        // Inicializar valores por defecto
        Status = GameStatus.Waiting;
        Players = new Dictionary<string, Player>();
        WordHistory = new List<string>();
        CurrentWord = "";
    }

    /// <summary>
    /// Initializes the game state.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="playerIds">List of player IDs participating in the game.</param>
    public void Initialize(Board board, List<string> playerIds)
    {
        GameBoard = board;
        InitializePlayers(playerIds);
        // Aquí puedes añadir más lógica de inicialización, como elegir el jugador inicial aleatoriamente
        CurrentPlayerId = playerIds.First(); // Por simplicidad, el primer jugador de la lista empieza
        Status = GameStatus.Playing;
    }

    private void InitializePlayers(List<string> playerIds)
    {
        Players.Clear();
        foreach (string playerId in playerIds)
        {
            // Inicializar con valores por defecto. Luego estos valores se actualizarán desde Firebase
            Players.Add(playerId, new Player(playerId));
        }
    }

    /// <summary>
    /// Adds a letter to the current word.
    /// </summary>
    /// <param name="letter">The letter to add.</param>
    public void AddLetterToCurrentWord(string letter)
    {
        CurrentWord += letter;
    }

    /// <summary>
    /// Clears the current word.
    /// </summary>
    public void ClearCurrentWord()
    {
        CurrentWord = "";
    }

    /// <summary>
    /// Updates the score of the given player.
    /// </summary>
    /// <param name="playerId">The ID of the player whose score to update.</param>
    /// <param name="points">The points to add to the player's score.</param>
    public void UpdatePlayerScore(string playerId, int points)
    {
        if (Players.ContainsKey(playerId))
        {
            Players[playerId].Score += points;
        }
    }

    /// <summary>
    /// Submits the current word formed by the player
    /// </summary>
    public void SubmitCurrentWord()
    {
        WordHistory.Add(CurrentWord);
        ClearCurrentWord();
    }

    /// <summary>
    /// Sets the current player.
    /// </summary>
    /// <param name="playerId">The ID of the player to set as current.</param>
    public void SetCurrentPlayer(string playerId)
    {
        if (Players.ContainsKey(playerId))
        {
            CurrentPlayerId = playerId;
        }
    }

    /// <summary>
    /// Changes the game status.
    /// </summary>
    /// <param name="newStatus">The new game status.</param>
    public void ChangeGameStatus(GameStatus newStatus)
    {
        Status = newStatus;
    }

    /// <summary>
    /// Adds a new player to the game.
    /// </summary>
    /// <param name="playerId">The ID of the new player.</param>
    /// <param name="player">The Player object representing the new player.</param>
    public void AddPlayer(string playerId, Player player)
    {
        if (!Players.ContainsKey(playerId))
        {
            Players.Add(playerId, player);
        }
    }

    /// <summary>
    /// Gets the Player object for the given player ID.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>The Player object, or null if no player with the given ID is found.</returns>
    public Player GetPlayer(string playerId)
    {
        if (Players.ContainsKey(playerId))
        {
            return Players[playerId];
        }
        return null;
    }

    /// <summary>
    /// Checks if the given player ID is the current player.
    /// </summary>
    /// <param name="playerId">The ID of the player to check.</param>
    /// <returns>True if the player is the current player, false otherwise.</returns>
    public bool IsCurrentPlayer(string playerId)
    {
        return CurrentPlayerId == playerId;
    }
}