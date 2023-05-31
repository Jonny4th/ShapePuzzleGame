using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class DragDetect : MonoBehaviour
{
    [SerializeField]
    TMP_Text positionDisplay;

    [SerializeField]
    LineRenderer line;

    [SerializeField]
    GameObject point;

    [SerializeField]
    float depth;

    [SerializeField]
    float lineWidth;

    public List<Vector2> points = new();

    bool m_IsPress;

    readonly string positionDisplayFormat = "( {0} , {1} )";
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    public void PointerPosition(InputAction.CallbackContext context)
    {
        if(!m_IsPress) return;
        var pos = context.ReadValue<Vector2>();
        points.Add(pos);
        positionDisplay.text = string.Format(positionDisplayFormat, (int)pos.x, (int)pos.y);
        DrawOnScreen(points, line);
    }

    public void OnPointerPressed(InputAction.CallbackContext context)
    {
        var press = context.ReadValue<float>();
        SetIsPressing(press > 0.5f);
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        var touch = context.ReadValue<TouchState>();
        Debug.Log(touch.phase);
        SetIsPressing(touch.phase != TouchPhase.Ended || touch.phase != TouchPhase.Canceled);
        if(!m_IsPress) return;
        var pos = touch.position;
        points.Add(pos);
        positionDisplay.text = string.Format(positionDisplayFormat, (int)pos.x, (int)pos.y);
        DrawOnScreen(points, line);
    }

    private void SetIsPressing(bool value)
    {
        m_IsPress = value;
        if(!m_IsPress) points.Clear();
    }

    private void DrawOnScreen(List<Vector2> points, LineRenderer line)
    {
        var pos = points.Select(point => cam.ScreenToWorldPoint(new Vector3(point.x, point.y, depth))).ToArray();
        line.positionCount = pos.Length;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.SetPositions(pos);
        ShowPointOnWorld(CenterOfMass(pos.ToList()));
    }

    private Vector3 CenterOfMass(List<Vector3> points)
    {
        var center = Vector3.zero;
        points.ForEach(point => { center += point; });
        center /= points.Count;
        return center;
    }

    private void ShowPointOnWorld(Vector3 pos)
    {
        point.transform.position = pos;
        point.SetActive(true);
    }

    private Vector2 Center(List<Vector2> points)
    {

        return default;
    }
}
