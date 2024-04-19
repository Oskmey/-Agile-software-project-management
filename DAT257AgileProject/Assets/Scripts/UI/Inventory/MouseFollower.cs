using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform,
            Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        if (val) Update();
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}