using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class to use the touch OR the click
/// </summary>
public class InputTouchBridge : MonoBehaviour
{
    public static InputTouchBridge get
    {
        get
        {
            if (instance == null)
                Init();
            return instance;
        }
    }
    private static InputTouchBridge instance;

    private static void Init()
    {
        GameObject holder = new GameObject("InputTouchBridge");
        DontDestroyOnLoad(holder);
        instance = holder.AddComponent<InputTouchBridge>();
    }


    public enum Plateform { PC, Mobile };
    public Plateform plateform = Plateform.PC;


    int maxTouch = 2;


    public List<InputTouch> fingers = new List<InputTouch>();

    public Action<InputTouch> onTouch;
    public Action<InputTouch> onHoverring;
    public Action<InputTouch> onRelease;

    public int touchCount = 0; // read only



    private void Awake()
    {

#if UNITY_STANDALONE || UNITY_EDITOR
        plateform = Plateform.PC;
#elif UNITY_IOS || UNITY_ANDROID
        plateform = Plateform.Mobile;
#endif

        for (int i = 0; i < 5; i++)
        {
            fingers.Add(new InputTouch());
        }

    }

    private void Update()
    {
        switch (plateform)
        {
            case Plateform.PC:
                UpdateDesktop();
                break;
            case Plateform.Mobile:
                UpdateMobile();
                break;
            default:
                Debug.LogError("WTF, not implemented");
                break;
        }

        UpdateCallbacks();
    }


    private void UpdateDesktop()
    {
        bool mouseDown = Input.GetMouseButton(0);

        touchCount = mouseDown ? 1 : 0;

        InputTouch _finger;
        for (int i = 0; i < fingers.Count; i++)
        {
            _finger = fingers[i];

            //les doigts en trop
            if (i >= touchCount && _finger.Used) _finger.SetEnded();

            //les doigts qui sont encore là (ou nouveaux)
            else if (i < touchCount) _finger.Update(i, Input.mousePosition);
        }
    }

    private void UpdateMobile()
    {
        if (!Input.touchSupported) return;

        touchCount = Input.touchCount;

        Touch[] touches = Input.touches;

        InputTouch _finger = null;

        for (int i = 0; i < touchCount; i++)
        {
            if (i >= maxTouch) break; // don't process additionnal finger
                                      //if (touchCount > touches.Length) continue;
            UIDebug.WriteLine("Finger " + i + " Process");

            _finger = getFingerById(touches[i].fingerId);

            //si on trouve pas de doigt on en prend un dispo
            if (_finger == null) _finger = getFirstAvailableFinger();

            if (_finger == null)
            {
                UIDebug.WriteLine("no available finger but touchCount > 0 ?");
                Debug.LogWarning("no available finger but touchCount > 0 ?");
                continue;
            }
            UIDebug.WriteLine("Finger " + i + " Update");
            _finger.Update(touches[i]);
        }
    }

    private void UpdateCallbacks()
    {
        for (int i = 0; i < fingers.Count; i++)
        {
            if (i >= touchCount || fingers[i].Ended)
            {
                if (fingers[i].Used && onRelease != null)
                    onRelease(fingers[i]);
                fingers[i].Reset();
                continue;
            }


            if (fingers[i].phase == TouchPhase.Began)
            {
                if(onTouch != null)
                    onTouch(fingers[i]);
            }
            else if ((fingers[i].phase == TouchPhase.Moved || fingers[i].phase == TouchPhase.Stationary) && onHoverring != null)
                onHoverring(fingers[i]);
        }
    }


    public InputTouch getFingerById(int id, bool checkActivity = true)
    {
        for (int i = 0; i < fingers.Count; i++)
        {
            //on retourne pas les doigts qui ont déjà fini
            //sur mobile la phase "ended" dure 6+ frames ...
            if (checkActivity && !fingers[i].Used) continue;

            if (fingers[i].fingerId == id) return fingers[i];
        }
        return null;
    }

    protected InputTouch getFirstAvailableFinger()
    {
        for (int i = 0; i < fingers.Count; i++)
        {
            if (fingers[i].Canceled) return fingers[i];
        }
        return null;
    }

}


public class InputTouch
{
    public Vector2 startScreenPos;
    public Vector2 startScreenPosPixels;
    public Vector2 screenPosPixels;
    public Vector2 screenPos;
    private Vector2 lastScreenPosition;
    private Vector2 deltaScreenPosition;


    private Camera camera;

    public TouchPhase phase;

    public int fingerId;

    public InputTouch()
    {
        camera = Camera.main; //Maybe need to use another camera?
        Reset();
    }

    internal void Reset()
    {
        fingerId = -1;
        phase = TouchPhase.Canceled;
    }

    public Vector3 worldPos(float distance)
    {
        return camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distance));
    }

    public bool Used { get { return phase != TouchPhase.Canceled; } }
    public bool Ended { get { return phase == TouchPhase.Ended; } }
    public bool Canceled { get { return phase == TouchPhase.Canceled; } }


    /// <summary>
    /// Update avec Touch
    /// </summary>
    /// <param name="touch"></param>
    public void Update(Touch touch)
    {
        phase = touch.phase;
        fingerId = touch.fingerId;
        screenPosPixels = touch.position;
        lastScreenPosition = screenPos;
        lastScreenPosition = touch.position;

        if(phase == TouchPhase.Began)
        {
            startScreenPos = screenPos;
            startScreenPosPixels = screenPosPixels;
        }

        CommonUpdate();
    }


    /// <summary>
    /// Update avec simulation de Touch (pour PC)
    /// </summary>
    /// <param name="touch"></param>
    public void Update(int idx, Vector3 mousePosition)
    {
        fingerId = idx;
        screenPosPixels = mousePosition; // set new position

        CommonUpdate();

        if (phase == TouchPhase.Canceled || phase == TouchPhase.Ended)
        {
            phase = TouchPhase.Began;
            startScreenPos = screenPos;
            startScreenPosPixels = screenPosPixels;
        }
        else if (deltaScreenPosition.magnitude > 0)
        {
            phase = TouchPhase.Moved;
        }
        else if (deltaScreenPosition.magnitude == 0)
        {
            phase = TouchPhase.Stationary;
        }
    }

    private void CommonUpdate()
    {
        screenPos = new Vector2(screenPosPixels.x / Screen.width, screenPosPixels.y / Screen.height);

        //faut éviter un delta incohérent au moment du touch
        if (phase == TouchPhase.Began || phase == TouchPhase.Canceled)
        {
            lastScreenPosition = screenPos;
        }

        deltaScreenPosition = screenPos - lastScreenPosition; // solve delta with old position
        lastScreenPosition = screenPos;


    }

    internal void SetEnded()
    {
        phase = TouchPhase.Ended;
    }

    public string Debug()
    {
        string debugText = "";

        debugText += "\nIndex =  = " + fingerId;
        debugText += "\nScreen position in pixels = " + screenPosPixels;
        debugText += "\nInitial position in pixels = " + startScreenPosPixels;
        debugText += "\nScreen position = " + screenPos;
        debugText += "\nStart position = " + startScreenPos;
        debugText += "\nLast position = " + lastScreenPosition;
        debugText += "\nDelta position = " + deltaScreenPosition;
        debugText += "\nScreen position = " + phase;

        return debugText;

    }


}