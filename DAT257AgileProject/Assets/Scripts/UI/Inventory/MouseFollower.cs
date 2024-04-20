using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField] 
    private Camera mainCam;

    [SerializeField]
    private UIInventoryItem item;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        mainCam = Camera.main;
        item = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }

    void Update()
    {
        FollowObject();
    }

    private void FollowObject()
    {
        // TODO: make use of new input system for mousePosition
        Vector2 position;
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform,
            mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        // Update to new position as last position can show
        // where item ui element was for mouse follower last for a frame
        if (val) Update();
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}