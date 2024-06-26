using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,
        IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text quantityText;

        [SerializeField]
        private Image borderImage;

        public event Action<UIItem> OnItemClicked, OnItemDroppedOn,
            OnItemBeginDrag, OnItemEndDrag, OnRightMouseButtonClick;

        private bool empty = true;

        private int quantity;

        public int Quantity { get => quantity; private set => quantity = value; }
        public void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            empty = true;
        }

        public void Deselect()
        {
            borderImage.enabled = false;
        }

        // TODO: do check for quantity = 0
        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityText.text = quantity.ToString();
            Quantity = quantity;
            empty = false;
        }

        public void Select()
        {
            borderImage.enabled = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
            {
                return;
            }
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseButtonClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}
