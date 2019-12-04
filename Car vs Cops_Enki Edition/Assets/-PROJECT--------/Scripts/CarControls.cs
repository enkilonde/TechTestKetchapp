using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class that control the inputs
/// </summary>
public class CarControls : MonoBehaviour
{

    public RectTransform wheelOutside;
    public RectTransform wheelInside;
    public float wheelAngle;

    public VehicleMove player;

    public bool active = false;


    void Awake()
    {
        InputTouchBridge.get.onTouch += OnTouch;
        InputTouchBridge.get.onHoverring += OnHover;
        InputTouchBridge.get.onRelease += OnRelease;
        wheelOutside.anchoredPosition = new Vector2(-10000, -10000);
    }
    void OnDestroy()
    {
        InputTouchBridge.get.onTouch -= OnTouch;
        InputTouchBridge.get.onHoverring -= OnHover;
        InputTouchBridge.get.onRelease -= OnRelease;
    }


    /// <summary>
    /// Called by the EventTrigger in CanvasStart
    /// </summary>
    public void StartGame(BaseEventData input)
    {
        active = true;
        wheelOutside.anchoredPosition = ((PointerEventData)input).position;
        Debug.Log("Start");
    }
    internal void EndGame()
    {
        active = false;
    }


    private void OnTouch(InputTouch input)
    {
        Debug.Log("Touch");
        if (!active)
            return;
        wheelOutside.anchoredPosition = input.startScreenPosPixels;

    }

    private void OnHover(InputTouch input)
    {
        Debug.Log("Hover");
        if (!active)
            return;
        wheelAngle = Vector2.SignedAngle((input.screenPosPixels - input.startScreenPosPixels).normalized, Vector2.down) + 180;
        player.targetAngle = wheelAngle;

        Vector2 posWheelInside = Vector2.zero;
        posWheelInside.x = Mathf.Sin((wheelAngle + 0) * Mathf.Deg2Rad);
        posWheelInside.y = Mathf.Cos((wheelAngle + 0) * Mathf.Deg2Rad);
        wheelInside.anchoredPosition = posWheelInside * 20; // the 20 is a value I made in the editor

    }

    private void OnRelease(InputTouch input)
    {
        if (!active)
            return;
        wheelOutside.anchoredPosition = new Vector2(-10000, -10000);
    }

}
