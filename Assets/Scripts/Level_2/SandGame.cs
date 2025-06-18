using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Assets.Scripts.GlobalInformation;
using Assets.Scripts.Level_2;
using System.Collections;

public class SandGame : MonoBehaviour
{
    public int rows = 10, cols = 10;
    public GameObject cellPrefab;
    public Transform gridParent;

    public Sprite soilSprite, treeSprite, sandSprite;

    [Header("Game Settings")]
    public int sandSpreadPerTurn = 2;

    private enum CellState { Soil, Tree, Sand }
    private CellState[,] grid;
    private Image[,] uiGrid;

    private bool gameEnded = false;
    private bool waitingForPlayer = true;

    private List<Vector2Int> sandFront = new();

    [SerializeField] private Dialog_1_1 _nextDialogueBox;

    private void Update()
    {
        FitGridToScreen();
    }

    void FitGridToScreen()
    {
        var rt = gridParent.GetComponent<RectTransform>();

        float screenHeight = Screen.height;
        float screenWidth = Screen.width;

        float referenceAspect = 16f / 9f;
        float currentAspect = screenWidth / screenHeight;

        float scaleFactor = currentAspect < referenceAspect
            ? currentAspect / referenceAspect 
            : referenceAspect / currentAspect; 

        rt.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }

    void OnEnable()
    {
        ClearGrid();
        InitGrid();
        InitSand();
    }

    void ClearGrid()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
    }

    void InitGrid()
    {
        grid = new CellState[rows, cols];
        uiGrid = new Image[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                var cell = Instantiate(cellPrefab, gridParent);
                cell.SetActive(true);
                int row = r, col = c;
                cell.GetComponent<Button>().onClick.AddListener(() => OnCellClicked(row, col));
                uiGrid[r, c] = cell.GetComponent<Image>();
                SetCellVisual(row, col, CellState.Soil);
            }
        }
    }

    void InitSand()
    {
        for (int r = 0; r < rows; r++)
        {
            grid[r, 0] = CellState.Sand;
            SetCellVisual(r, 0, CellState.Sand);
            sandFront.Add(new Vector2Int(r, 0));
        }
    }

    void OnCellClicked(int r, int c)
    {
        if (gameEnded || !waitingForPlayer) return;
        if (grid[r, c] != CellState.Soil) return;

        grid[r, c] = CellState.Tree;
        SetCellVisual(r, c, CellState.Tree);
        waitingForPlayer = false;

        Invoke(nameof(AdvanceSand), 0.4f);
    }

    void AdvanceSand()
    {
        if (gameEnded) return;

        HashSet<Vector2Int> possibleSpread = new();

        foreach (var pos in sandFront)
        {
            foreach (var dir in Directions())
            {
                Vector2Int next = pos + dir;
                if (!InBounds(next.x, next.y)) continue;
                if (grid[next.x, next.y] != CellState.Soil) continue;
                possibleSpread.Add(next);
            }
        }

        List<Vector2Int> candidates = new(possibleSpread);
        Shuffle(candidates);

        int spreadCount = 0;
        List<Vector2Int> newFront = new();

        foreach (var pos in candidates)
        {
            if (spreadCount >= sandSpreadPerTurn) break;

            grid[pos.x, pos.y] = CellState.Sand;
            SetCellVisual(pos.x, pos.y, CellState.Sand);
            newFront.Add(pos);
            spreadCount++;
        }

        if (spreadCount == 0)
        {
            Debug.Log("Песок не может распространяться. Победа!");
            EndGame(true);
            return;
        }

        sandFront.AddRange(newFront);
        CheckGameState();
        waitingForPlayer = true;
    }

    void SetCellVisual(int r, int c, CellState state)
    {
        Image img = uiGrid[r, c];
        switch (state)
        {
            case CellState.Soil: img.sprite = soilSprite; break;
            case CellState.Tree: img.sprite = treeSprite; break;
            case CellState.Sand: img.sprite = sandSprite; break;
        }
    }

    void CheckGameState()
    {
        int totalCells = rows * cols;
        int totalSoil = 0;
        int totalTrees = 0;

        foreach (var cell in grid)
        {
            if (cell == CellState.Soil) totalSoil++;
            else if (cell == CellState.Tree) totalTrees++;
        }

        if (totalSoil < totalCells / 5)
        {
            Debug.Log($"Поражение: почва {totalSoil}/{totalCells}");
            EndGame(false);
            return;
        }

        int reachableSoil = GetReachableSoilFromSand();
        int unreachableSoil = totalSoil - reachableSoil;

        if (unreachableSoil >= totalCells / 5)
        {
            Debug.Log($"Победа: защищено {unreachableSoil}/{totalCells}");
            EndGame(true);
        }
    }


    int GetReachableSoilFromSand()
    {
        bool[,] visited = new bool[rows, cols];
        Queue<Vector2Int> queue = new();
        int reachableSoil = 0;

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                if (grid[r, c] == CellState.Sand)
                {
                    queue.Enqueue(new Vector2Int(r, c));
                    visited[r, c] = true;
                }

        while (queue.Count > 0)
        {
            var pos = queue.Dequeue();

            foreach (var dir in Directions())
            {
                int nr = pos.x + dir.x;
                int nc = pos.y + dir.y;

                if (!InBounds(nr, nc) || visited[nr, nc])
                    continue;

                if (grid[nr, nc] == CellState.Tree)
                    continue;

                visited[nr, nc] = true;

                if (grid[nr, nc] == CellState.Soil)
                    reachableSoil++;

                queue.Enqueue(new Vector2Int(nr, nc));
            }
        }

        return reachableSoil;
    }

    void EndGame(bool win)
    {
        gameEnded = true;

        if (win)
        {
            ShowResultMessage("Победа!\nПесок не пройдёт.\nЗелёный фронт держится!", true);
            StartCoroutine(Win());
        }
        else
        {
            ShowResultMessage("Поражение...\nПесок поглотил всё живое.\nМир стал безжизненной пустыней.", false);
            Invoke(nameof(RestartGame), 3.5f);
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3.5f);
        HideResultMessage();
        _nextDialogueBox.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        ClearGrid();

        grid = new CellState[rows, cols];
        uiGrid = new Image[rows, cols];
        sandFront.Clear(); 

        InitGrid();
        InitSand();

        HideResultMessage();
        gameEnded = false;
        waitingForPlayer = true;

        
    }

    void HideResultMessage()
    {
        if (Shower.resultCanvas != null)
        {
            Destroy(Shower.resultCanvas.gameObject);
            Shower.resultCanvas = null;
            Shower.resultText = null;
        }
    }


    bool InBounds(int r, int c) => r >= 0 && r < rows && c >= 0 && c < cols;

    IEnumerable<Vector2Int> Directions()
    {
        yield return new Vector2Int(-1, 0); // вверх
        yield return new Vector2Int(1, 0);  // вниз
        yield return new Vector2Int(0, 1);  // вправо
        yield return new Vector2Int(0, -1); // влево
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }



    void ShowResultMessage(string message, bool victory)
    {
        //if (resultCanvas == null) Shower.CreateResultUI();
        //resultText.text = message;
        //resultText.color = victory ? new Color(0.8f, 1f, 0.8f) : new Color(1f, 0.7f, 0.7f); 
        StartCoroutine(Shower.FadeResultRoutine(message, victory));
    }

}