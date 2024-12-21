using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;

/// <summary>
/// GameManager class orchestrates the overall game flow.
/// It should be implemented as a Singleton.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Instance of the GameManager for the Singleton pattern
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }
    public Board GameBoard { get; private set; }

    private WordValidator _wordValidator;

    // Evento que se lanzar� cuando GameManager est� listo
    public event Action OnGameManagerReady;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Inicializa el estado del juego (GameState, Board, etc.)
        InitializeGame();        
    }

    private void Start()
    {
        // Inicializa la UI
        gameObject.AddComponent<UIManager>();

        // Lanza el evento OnGameManagerReady
        OnGameManagerReady?.Invoke();
    }

    /// <summary>
    /// Initializes the game.
    /// </summary>
    private void InitializeGame()
    {
        // Crea una instancia del tablero
        GameBoard = gameObject.AddComponent<Board>();
        // Por ahora, el tama�o del tablero ser� fijo, pero se podr�a parametrizar m�s adelante.
        GameBoard.Initialize(7, 7);

        // Lista de jugadores de ejemplo (m�s adelante se obtendr� de Firebase)
        List<string> playerIds = new List<string> { "player1", "player2" };

        // Crea una instancia del estado del juego. Se inicializa dentro de GameState,
        // pero tambi�n se podr�a hacer aqu�
        GameState = gameObject.AddComponent<GameState>();
        GameState.Initialize(GameBoard, playerIds);

        // Crea una instancia del validador de palabras, inyectando la dependencia de IWordProvider
        // En este caso, estamos usando una implementaci�n local (LocalWordProvider)
        IWordProvider localWordProvider = gameObject.AddComponent<LocalWordProvider>();
        _wordValidator = new WordValidator(localWordProvider);
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        // Comienza la partida, se podr�a mostrar una cuenta atr�s, por ejemplo.
        // Por ahora, simplemente cambiaremos el estado a "jugando".
        GameState.ChangeGameStatus(GameState.GameStatus.Playing);
    }

    /// <summary>
    /// Processes a player's move (when a cell is selected).
    /// </summary>
    /// <param name="cell">The cell that was selected.</param>
    public void ProcessPlayerMove(HexCell cell)
    {
        // Si la partida no est� en juego, no se procesan movimientos
        if (GameState.Status != GameState.GameStatus.Playing) return;

        // Comprueba que la celda seleccionada sea adyacente a la �ltima celda seleccionada,
        // a no ser que sea la primera letra de la palabra, que se permite que sea cualquier celda
        // (esto se implementa en IsLegalMove de Board.cs, en funci�n de si la palabra actual es vac�a)
        var lastCell = GameState.WordHistory.Count == 0
            ? null
            : GameBoard.GetCell(
                GameState.WordHistory.Last().X,
                GameState.WordHistory.Last().Y
            );

        if (lastCell != null && !GameBoard.IsLegalMove(lastCell, cell))
        {
            Debug.Log("Movimiento no v�lido: la celda seleccionada no es adyacente a la �ltima celda seleccionada.");
            return; // Movimiento no v�lido
        }

        // A�ade la celda a la lista de celdas seleccionadas de la palabra actual
        GameState.WordHistory.Add(cell);

        // A�ade la letra de la celda seleccionada a la palabra actual en GameState
        GameState.AddLetterToCurrentWord(cell.Letter.ToString());

        // Aqu� podr�as a�adir l�gica adicional, como actualizar la UI, etc.
    }

    /// <summary>
    /// Validates and submits the current word.
    /// </summary>
    public void SubmitWord()
    {
        // Si la partida no est� en juego, no se procesan las palabras
        if (GameState.Status != GameState.GameStatus.Playing) return;

        // Valida la palabra actual
        if (_wordValidator.ValidateWord(GameState.CurrentWord))
        {
            // Palabra v�lida: actualiza la puntuaci�n, limpia la palabra actual, etc.
            Debug.Log("Palabra v�lida: " + GameState.CurrentWord);
            int points = CalculatePoints(GameState.CurrentWord);
            GameState.UpdatePlayerScore(GameState.CurrentPlayerId, points);
            GameState.SubmitCurrentWord();
        }
        else
        {
            // Palabra inv�lida: limpia la palabra actual, penaliza al jugador, etc.
            Debug.Log("Palabra no v�lida: " + GameState.CurrentWord);
            GameState.ClearCurrentWord();
            // Aqu� se podr�a a�adir una penalizaci�n, como restarle puntos al jugador
        }
    }

    /// <summary>
    /// Calculates the points for a given word (basic example).
    /// </summary>
    /// <param name="word">The word to calculate points for.</param>
    /// <returns>The points for the word.</returns>
    private int CalculatePoints(string word)
    {
        // Implementar la l�gica para calcular la puntuaci�n de la palabra.
        // Por ahora, simplemente devolvemos la longitud de la palabra como puntuaci�n.
        return word.Length;
    }
}