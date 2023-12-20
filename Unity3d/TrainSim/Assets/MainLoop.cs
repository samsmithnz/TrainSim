using UnityEngine;
using Color = UnityEngine.Color;

public class MainLoop : MonoBehaviour
{
    public GameObject gridPrefab;
    public int width = 20;
    public int breadth = 20;
    private bool showCoordsOnFloor = true;
    private bool showLinesOnFloor = true;


    void Start()
    {
        SetupLevel(gameObject, width, breadth);
    }

    private void SetupLevel(GameObject parentGameObject, int width, int breadth)
    {
        Font arialFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        //setup the map object and create the map
        GameObject parentFloor = new GameObject
        {
            name = "parentFloor"
        };
        parentFloor.transform.parent = parentGameObject.transform;

        int y = 0;
        float scale = 2f;
        //Draw the map on the screen
        for (int x = 0; x <= width - 1; x++)
        {
            for (int z = 0; z <= breadth - 1; z++)
            {
                //Create map floor
                GameObject newFloorObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newFloorObject.transform.position = new Vector3(x * scale, -(scale/2), z * scale);
                newFloorObject.transform.localScale = new Vector3(scale, scale, scale);
                newFloorObject.name = $"floor_x{x}_y{y}_z{z}";
                newFloorObject.transform.parent = parentFloor.transform;

                if (showCoordsOnFloor == true)
                {
                    GameObject newFloorCanvasObject = new GameObject
                    {
                        name = "Canvas"
                    };
                    newFloorCanvasObject.transform.parent = newFloorObject.transform;
                    Canvas floorCanvas = newFloorCanvasObject.AddComponent<Canvas>();
                    floorCanvas.transform.localPosition = Vector3.zero;
                    floorCanvas.renderMode = RenderMode.WorldSpace;
                    floorCanvas.worldCamera = Camera.main;

                    GameObject newFloorTextObject = new GameObject
                    {
                        name = "Text"
                    };
                    newFloorTextObject.transform.SetParent(newFloorCanvasObject.transform);
                    UnityEngine.UI.Text floorText = newFloorTextObject.transform.gameObject.AddComponent<UnityEngine.UI.Text>();
                    floorText.transform.localPosition = new Vector3(0f, (scale/2) + 0.501f, 0f);
                    floorText.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
                    floorText.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    //floorText.transform.parent = floorCanvas.transform;
                    floorText.rectTransform.sizeDelta = new Vector2(100f, 100f);
                    floorText.color = Color.black;
                    floorText.alignment = TextAnchor.MiddleCenter;
                    floorText.fontSize = 24;
                    floorText.font = arialFont;
                    floorText.text = "x" + x.ToString() + ",z" + z.ToString();
                }

                //GameObject newObject = GameObject.Instantiate(Resources.Load<GameObject>("PolygonStarter/Prefabs/SM_PolygonPrototype_Buildings_Block_1x1_01P"), Vector3.zero, Quaternion.identity) as GameObject;


                //Create map objects, if exists
                if (showLinesOnFloor == true)
                {
                    Color darkGray = new Color(0.184313725f, 0.309803922f, 0.309803922f); //Dark gray
                    //Draw line renderers
                    if (x == 0 && z != breadth - 1)
                    {
                        LineRenderer xguideLine = newFloorObject.AddComponent<LineRenderer>();
                        xguideLine.material = new Material(Shader.Find("Sprites/Default"));
                        xguideLine.widthMultiplier = 0.01f;
                        xguideLine.startColor = darkGray;
                        xguideLine.endColor = darkGray;
                        xguideLine.SetPosition(0, new Vector3(-(scale / 2), 0.01f, (z * scale) + (scale / 2)));
                        xguideLine.SetPosition(1, new Vector3(width - (scale / 2), 0.01f, (z * scale) + (scale / 2)));
                    }
                    else if (z == breadth - 1 && x != 0)
                    {
                        LineRenderer zguideLine = newFloorObject.AddComponent<LineRenderer>();
                        zguideLine.material = new Material(Shader.Find("Sprites/Default"));
                        zguideLine.widthMultiplier = 0.01f;
                        zguideLine.startColor = darkGray;
                        zguideLine.endColor = darkGray;
                        zguideLine.SetPosition(0, new Vector3((x * scale) - (scale / 2), 0.01f, -(scale / 2)));
                        zguideLine.SetPosition(1, new Vector3((x * scale) - (scale / 2), 0.01f, breadth - (scale / 2)));
                    }
                }

            } //end z for
        } //end x for
        //for (int i = 0; i < rows; i++)
        //{
        //    for (int j = 0; j < columns; j++)
        //    {
        //        GameObject gridInstance = Instantiate(gridPrefab, new Vector3(i, 0, j), Quaternion.identity);
        //        AddBorderToGridPrefab(gridInstance, UnityEngine.Color.red);
        //    }
        //}
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
