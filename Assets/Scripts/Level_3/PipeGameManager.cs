using UnityEngine;

public class PipeGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _pipePrefab;
    [SerializeField] private Transform _gridParent;

    [SerializeField] private int _gridSizeX = 5;
    [SerializeField] private int _gridSizeY = 5;

    [SerializeField] private PipeGridManager _gridManager;

    void Start()
    {
        GenerateGrid();

        if (_gridManager != null)
        {
            _gridManager.width = _gridSizeX;
            _gridManager.height = _gridSizeY;
            _gridManager.gridParent = _gridParent;
            _gridManager.InitializeGrid(); 
        }
    }

    public void GenerateGrid()
    {
        //GenerateTestGrid();
        GenerateRandomGrid();
     
    }

    void GenerateRandomGrid()
    {
        //var seed = 1;
        var seed = 907;

        var random = Random.Range(0, 3);
        //if (random == 0)
        //{
        //    seed = 700;
        //}
        //else if (random == 1)
        //{
        //    seed = 900;
        //}
        System.Random pseudoRandom = new System.Random(seed);

        PipeCell.PipeType[] types = new PipeCell.PipeType[]
        {
        PipeCell.PipeType.Straight,
        PipeCell.PipeType.Corner
        };

        for (int y = 0; y < _gridSizeY; y++)
        {
            for (int x = 0; x < _gridSizeX; x++)
            {
                GameObject pipe = Instantiate(_pipePrefab, _gridParent);
                pipe.name = $"Pipe ({x},{y})";

                var pipeCell = pipe.GetComponent<PipeCell>();
                if (pipeCell == null)
                {
                    Debug.LogError("Prefab не содержит PipeCell!");
                    continue;
                }

                // Используем наш псевдорандом с заданным сидом
                pipeCell.type = types[pseudoRandom.Next(0, types.Length)];
                pipeCell.rotation = 90 * pseudoRandom.Next(0, 4);
                pipeCell.isEndpoint = (x == 0 && y == 0) || (x == _gridSizeX - 1 && y == _gridSizeY - 1);

                // Если endpoint — обязательно Cross
                if (pipeCell.isEndpoint)
                    pipeCell.type = PipeCell.PipeType.Cross;

                pipeCell.transform.rotation = Quaternion.Euler(0, 0, pipeCell.rotation);
                pipeCell.UpdateSprite();
            }
        }

        Debug.Log($"Сетка сгенерирована");
    }
}
