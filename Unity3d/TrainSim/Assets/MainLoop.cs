using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MainLoop : MonoBehaviour
{
    public GameObject gridPrefab;
    public int rows = 10;
    public int columns = 10;


    void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject gridInstance = Instantiate(gridPrefab, new Vector3(i, 0, j), Quaternion.identity);
                AddBorderToGridPrefab(gridInstance, UnityEngine.Color.red);
            }
        }
    }

    private void AddBorderToGridPrefab(GameObject gridPrefab, UnityEngine.Color color)
    {
        GameObject border = new GameObject("Border");
        border.transform.parent = gridPrefab.transform;
        border.name = "Border";
        border.AddComponent<MeshRenderer>();
        border.GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
