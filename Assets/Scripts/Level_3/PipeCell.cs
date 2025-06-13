using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeCell : MonoBehaviour
{
    public Image image;
    public Sprite straightSprite;
    public Sprite cornerSprite;
    public Sprite tSprite;
    public Sprite crossSprite;

    public Sprite startSprite;
    public Sprite endSprite;

    public bool isEndpoint = false;

    public enum PipeType
    {
        Straight,
        Corner,
        TJunction,
        Cross,
        Empty
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public PipeType type = PipeType.Straight;

    private int _rotation = 0;
    public int rotation
    {
        get => _rotation;
        set
        {
            _rotation = value % 360;
            if (_rotation < 0) _rotation += 360;
            transform.rotation = Quaternion.Euler(0, 0, _rotation);
            UpdateSprite();
        }
    }

    void Start()
    {
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (isEndpoint)
        {
            image.sprite = startSprite;
            return;
        }

        switch (type)
        {
            case PipeType.Straight:
                image.sprite = straightSprite;
                break;
            case PipeType.Corner:
                image.sprite = cornerSprite;
                break;
            case PipeType.TJunction:
                image.sprite = tSprite;
                break;
            case PipeType.Cross:
                image.sprite = crossSprite;
                break;
        }
    }

    public void RotatePipe()
    {
        rotation = (rotation - 90) % 360; // теперь по часовой!
        Debug.Log($"Повернута труба типа {type} на {rotation} градусов");

        var connections = GetConnections();
        string connsStr = string.Join(", ", connections);
        Debug.Log($"Выходы трубы: {connsStr}");
    }

    public List<Direction> GetConnections()
    {
        if (isEndpoint)
        {
            return new List<Direction> { Direction.Up, Direction.Right, Direction.Down, Direction.Left };
        }

        List<Direction> connections = new List<Direction>();

        switch (type)
        {
            case PipeType.Straight:
                if (rotation % 180 == 0)
                {
                    connections.Add(Direction.Left);
                    connections.Add(Direction.Right);
                }
                else
                {
                    connections.Add(Direction.Up);
                    connections.Add(Direction.Down);
                }
                break;

            case PipeType.Corner:
                if (rotation == 0)
                {
                    connections.Add(Direction.Left);
                    connections.Add(Direction.Down);
                }
                else if (rotation == 90)
                {
                    connections.Add(Direction.Down);
                    connections.Add(Direction.Right);
                }
                else if (rotation == 180)
                {
                    connections.Add(Direction.Right);
                    connections.Add(Direction.Up);
                }
                else if (rotation == 270)
                {
                    connections.Add(Direction.Up);
                    connections.Add(Direction.Left);
                }
                break;

            case PipeType.TJunction:
                var baseDirs = new List<Direction> { Direction.Up, Direction.Right, Direction.Left };
                connections = RotateConnections(baseDirs, rotation);
                break;

            case PipeType.Cross:
                connections.Add(Direction.Up);
                connections.Add(Direction.Right);
                connections.Add(Direction.Down);
                connections.Add(Direction.Left);
                break;
        }

        return connections;
    }

    private List<Direction> RotateConnections(List<Direction> original, int rot)
    {
        var rotated = new List<Direction>();
        foreach (var dir in original)
        {
            int newDir = ((int)dir + rot / 90) % 4;
            rotated.Add((Direction)newDir);
        }
        return rotated;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 pos = transform.position;
        foreach (var dir in GetConnections())
        {
            Vector3 offset = Vector3.zero;
            switch (dir)
            {
                case Direction.Up: offset = Vector3.up; break;
                case Direction.Right: offset = Vector3.right; break;
                case Direction.Down: offset = Vector3.down; break;
                case Direction.Left: offset = Vector3.left; break;
            }
            Gizmos.DrawLine(pos, pos + offset * 0.5f);
        }
    }
}
