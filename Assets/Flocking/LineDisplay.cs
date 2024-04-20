using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDisplay : MonoBehaviour
{
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    public void Display(Vector2 start, Vector2 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    public void DisplayRelative(Vector2 v)
    {
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, this.transform.position + (Vector3)v);
    }

    public void Hide()
    {
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
    }
}
