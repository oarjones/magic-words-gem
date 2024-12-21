using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton instance

    [Header("Board")]
    [SerializeField] private GameObject _boardPanel;
    [SerializeField] private Button _hexCellPrefab;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _currentWordText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Header("Buttons")]
    [SerializeField] private Button _validateWordButton;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Inicializa la UI
        InitializeUI();

        // Suscribe los métodos a los eventos de los botones
        _validateWordButton.onClick.AddListener(OnValidateWordButtonClicked);
    }

    /// <summary>
    /// Initializes the UI elements.
    /// </summary>
    private void InitializeUI()
    {
        // Crea los botones del tablero
        CreateBoardUI();

        // Actualiza los textos iniciales
        UpdateCurrentWordText();
        UpdateScoreText();
    }

    /// <summary>
    /// Creates the UI representation of the game board.
    /// </summary>
    private void CreateBoardUI()
    {
        // Obtiene la referencia al Board del GameManager
        Board board = GameManager.Instance.GameBoard;

        // Instancia las celdas del tablero en la UI
        for (int y = 0; y < board.Height; y++)
        {
            for (int x = 0; x < board.Width; x++)
            {
                // Crea una instancia del prefab de la celda
                Button cellButton = Instantiate(_hexCellPrefab, _boardPanel.transform);

                // Configura el botón
                HexCell cell = board.GetCell(x, y);
                cellButton.GetComponentInChildren<TextMeshProUGUI>().text = cell.Letter.ToString();

                // Obtenemos el script HexCellButton del botón y le asignamos la celda
                var hexCellButton = cellButton.GetComponent<HexCellButton>();
                hexCellButton.Cell = cell;

                // Suscribimos el método OnCellClicked al evento OnCellClicked del script HexCellButton
                hexCellButton.OnCellClicked.AddListener(OnCellClicked);
            }
        }
    }

    /// <summary>
    /// Called when a cell button is clicked.
    /// </summary>
    /// <param name="cell">The cell that was clicked.</param>
    private void OnCellClicked(HexCell cell)
    {
        // Procesa el movimiento del jugador en el GameManager
        GameManager.Instance.ProcessPlayerMove(cell);

        // Actualiza la UI
        UpdateCurrentWordText();
    }

    /// <summary>
    /// Called when the validate word button is clicked.
    /// </summary>
    private void OnValidateWordButtonClicked()
    {
        // Llama al método SubmitWord del GameManager
        GameManager.Instance.SubmitWord();

        // Actualiza la UI
        UpdateCurrentWordText();
        UpdateScoreText();
    }

    /// <summary>
    /// Updates the current word text.
    /// </summary>
    public void UpdateCurrentWordText()
    {
        _currentWordText.text = "Palabra actual: " + GameManager.Instance.GameState.CurrentWord;
    }

    /// <summary>
    /// Updates the score text.
    /// </summary>
    public void UpdateScoreText()
    {
        _scoreText.text = "Puntuación: " + GameManager.Instance.GameState.GetPlayer(GameManager.Instance.GameState.CurrentPlayerId).Score;
    }
}