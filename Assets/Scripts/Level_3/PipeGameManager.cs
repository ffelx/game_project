using UnityEngine;

public class PipeGameManager : MonoBehaviour
{
    public GameObject pipePrefab;
    public Transform gridParent;

    public int gridSizeX = 5;
    public int gridSizeY = 5;

    public PipeGridManager gridManager;

    void Start()
    {
        GenerateGrid();

        if (gridManager != null)
        {
            gridManager.width = gridSizeX;
            gridManager.height = gridSizeY;
            gridManager.gridParent = gridParent;
            gridManager.InitializeGrid(); // вызываем **после** генерации трубы
        }
    }

    public void GenerateGrid()
    {
        // Выбери нужный способ генерации:
        //GenerateTestGrid();
        GenerateRandomGrid();
    }

    void GenerateTestGrid()
    {
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                GameObject pipe = Instantiate(pipePrefab, gridParent);
                pipe.name = $"Pipe ({x},{y})";

                var pipeCell = pipe.GetComponent<PipeCell>();
                if (pipeCell == null)
                {
                    Debug.LogError("Prefab не содержит PipeCell!");
                    continue;
                }

                if ((x == 0 && y == 0) || (x == gridSizeX - 1 && y == gridSizeY - 1))
                {
                    pipeCell.type = PipeCell.PipeType.Cross;
                    pipeCell.isEndpoint = true;
                    pipeCell.rotation = 0;
                }
                else if (x == gridSizeX - 1 && y == 0)
                {
                    pipeCell.type = PipeCell.PipeType.Corner;
                    pipeCell.isEndpoint = false;
                    pipeCell.rotation = 180;
                }
                else
                {
                    pipeCell.type = PipeCell.PipeType.Straight;
                    pipeCell.isEndpoint = false;
                    pipeCell.rotation = 0;
                }

                pipeCell.transform.rotation = Quaternion.Euler(0, 0, pipeCell.rotation);
                pipeCell.UpdateSprite();
            }
        }

        Debug.Log("✅ Тестовая сетка сгенерирована!");
    }

    void GenerateRandomGrid()
    {
        System.Array types = new PipeCell.PipeType[] { PipeCell.PipeType.Straight, PipeCell.PipeType.Corner };

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                GameObject pipe = Instantiate(pipePrefab, gridParent);
                pipe.name = $"Pipe ({x},{y})";

                var pipeCell = pipe.GetComponent<PipeCell>();
                if (pipeCell == null)
                {
                    Debug.LogError("Prefab не содержит PipeCell!");
                    continue;
                }

                pipeCell.type = (PipeCell.PipeType)types.GetValue(Random.Range(0, types.Length));
                pipeCell.rotation = 90 * Random.Range(0, 4);
                pipeCell.isEndpoint = (x == 0 && y == 0) || (x == gridSizeX - 1 && y == gridSizeY - 1);

                // Если endpoint — обязательно Cross
                if (pipeCell.isEndpoint)
                    pipeCell.type = PipeCell.PipeType.Cross;

                pipeCell.transform.rotation = Quaternion.Euler(0, 0, pipeCell.rotation);
                pipeCell.UpdateSprite();
            }
        }

        Debug.Log("🎲 Рандомная сетка сгенерирована!");
    }
}
