using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnGUI()
    {
        string debugText = "";

        debugText += "Test Input System";
        debugText += "\nPlateform = " + InputTouchBridge.get.plateform;
        debugText += "\n" + InputTouchBridge.get.touchCount;

        for (int i = 0; i < InputTouchBridge.get.touchCount; i++)
        {
            debugText += InputTouchBridge.get.fingers[i].Debug();
        }

        GUILayout.Label(debugText);
    }

    private void Create(InputTouch input)
    {
        GameObject go = Instantiate(prefab);
        go.transform.position = input.worldPos(10);


    }

}
