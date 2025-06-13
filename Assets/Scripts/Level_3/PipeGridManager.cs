using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGridManager : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    public PipeCell[,] grid;

    public Vector2Int startCell;
    public Vector2Int endCell;

    // Объект, в котором лежат трубы
    public Transform gridParent;

    // Не вызываем InitializeGrid в Start, чтобы не было конфликта с генерацией
    void Start()
    {
        // Пусто
    }

    // Вызывать после генерации объектов в gridParent
    public void InitializeGrid()
    {
        if (gridParent == null)
        {
            Debug.LogError("PipeGridManager: gridParent не установлен!");
            return;
        }

        if (gridParent.childCount < width * height)
        {
            Debug.LogWarning($"PipeGridManager: В gridParent всего {gridParent.childCount} детей, а ожидается {width * height}");
        }

        grid = new PipeCell[width, height];

        for (int i = 0; i < gridParent.childCount; i++)
        {
            PipeCell pipe = gridParent.GetChild(i).GetComponent<PipeCell>();
            if (pipe == null)
            {
                Debug.LogWarning($"PipeGridManager: Дочерний объект #{i} не содержит PipeCell!");
                continue;
            }

            int x = i % width;
            int y = i / width;

            if (x >= width || y >= height)
            {
                Debug.LogWarning($"PipeGridManager: Индексы выходят за пределы сетки: x={x}, y={y}");
                continue;
            }

            grid[x, y] = pipe;
        }

        startCell = new Vector2Int(0, 0);
        endCell = new Vector2Int(width - 1, height - 1);

        Debug.Log("PipeGridManager: Инициализация сетки завершена");
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
            Debug.LogError($"PipeGridManager: Стартовая клетка {startCell} вне границ!");
            return;
        }

        PipeCell startPipe = grid[startCell.x, startCell.y];
        if (startPipe == null || startPipe.GetConnections().Count == 0)
        {
            Debug.LogError($"PipeGridManager: Стартовая клетка {startCell} пуста или не имеет выходов!");
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
            Debug.Log("PipeGridManager: Соединение найдено! ✅");
        else
            Debug.Log("PipeGridManager: Нет пути от A до B ❌");
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
}
