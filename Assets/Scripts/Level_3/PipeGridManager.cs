using Assets.Scripts.Level_2;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PipeGridManager : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    public Transform gridParent;

    [SerializeField] private PipeCell[,] grid;
    [SerializeField] private Vector2Int startCell;
    [SerializeField] private Vector2Int endCell;

    [SerializeField] private GameObject _nextDialog;
    [SerializeField] private GameObject _game;

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

      
        float scaleFactor = 1f;

      
        if (currentAspect > referenceAspect)
        {
          
            float excessRatio = currentAspect / referenceAspect;
            scaleFactor = 1f / excessRatio;

            scaleFactor *= 0.9f;
        }
        rt.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        //var rt = gridParent.GetComponent<RectTransform>();

        //float screenHeight = Screen.height;
        //float screenWidth = Screen.width;

        //float referenceAspect = 16f / 9f;
        //float currentAspect = screenWidth / screenHeight;

        //float scaleFactor = currentAspect < referenceAspect
        //    ? currentAspect / referenceAspect
        //    : referenceAspect / currentAspect;

        //rt.localScale = new Vector3(scaleFactor, scaleFactor, 1);

        //Vector2 canvasCenter = new Vector2(screenWidth / 2f, screenHeight / 2f);
        //rt.position = canvasCenter;
    }


    public void InitializeGrid()
    {
        if (gridParent == null)
        {
            return;
        }

        grid = new PipeCell[width, height];

        for (int i = 0; i < gridParent.childCount; i++)
        {
            PipeCell pipe = gridParent.GetChild(i).GetComponent<PipeCell>();
            if (pipe == null)
            {
                continue;
            }

            int x = i % width;
            int y = i / width;

            if (x >= width || y >= height)
            {
                continue;
            }

            grid[x, y] = pipe;
        }

        startCell = new Vector2Int(0, 0);
        endCell = new Vector2Int(width - 1, height - 1);
    }

    public void CheckConnection()
    {
        if (grid == null)
        {
            Debug.LogError("PipeGridManager: Сетка не инициализирована!");
            return;
        }

        bool[,] visited = new bool[width, height];
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        if (!InBounds(startCell))
        {
            return;
        }

        PipeCell startPipe = grid[startCell.x, startCell.y];
        if (startPipe == null || startPipe.GetConnections().Count == 0)
        {
            return;
        }

        queue.Enqueue(startCell);
        visited[startCell.x, startCell.y] = true;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            PipeCell currentPipe = grid[current.x, current.y];
            var currentDirs = currentPipe.GetConnections();

            foreach (var dir in currentDirs)
            {
                Vector2Int next = GetNextCoord(current, dir);
                if (!InBounds(next)) continue;
                if (visited[next.x, next.y]) continue;

                PipeCell nextPipe = grid[next.x, next.y];
                if (nextPipe == null) continue;

                var nextDirs = nextPipe.GetConnections();
                if (nextDirs.Count == 0) continue;

                if (nextDirs.Contains(Opposite(dir)))
                {
                    visited[next.x, next.y] = true;
                    queue.Enqueue(next);
                }
            }
        }

        if (visited[endCell.x, endCell.y])
        {
            Debug.Log("PipeGridManager: Соединение найдено! ");
            Win();
            return;
        }
        Debug.Log("PipeGridManager: Нет пути от A до B ");
    }

    Vector2Int GetNextCoord(Vector2Int from, PipeCell.Direction dir)
    {
        switch (dir)
        {
            case PipeCell.Direction.Up: return new Vector2Int(from.x, from.y - 1);
            case PipeCell.Direction.Right: return new Vector2Int(from.x + 1, from.y);
            case PipeCell.Direction.Down: return new Vector2Int(from.x, from.y + 1);
            case PipeCell.Direction.Left: return new Vector2Int(from.x - 1, from.y);
            default: return from;
        }
    }

    PipeCell.Direction Opposite(PipeCell.Direction dir)
    {
        return (PipeCell.Direction)(((int)dir + 2) % 4);
    }

    bool InBounds(Vector2Int coord)
    {
        return coord.x >= 0 && coord.x < width && coord.y >= 0 && coord.y < height;
    }

    private void Win()
    {
        StartCoroutine(ShowVictoryMessage());
    }

    private IEnumerator ShowVictoryMessage()
    {
        //Thread.Sleep(2000);
        yield return new WaitForSeconds(0.01f);
        Thread.Sleep(1500);
        yield return StartCoroutine(Shower.FadeResultRoutine("Победа!", true));
        _game.SetActive(false);
        _nextDialog.SetActive(true);
    }



}
