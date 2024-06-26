using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    //[SerializeField]
    //private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }

    public void CollectItem()
    {
        //GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup(true));
    }

    public void DestroyItem()
    {
        //GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup(false));
    }

    private IEnumerator AnimateItemPickup(bool IsCollected)
    {
        if(IsCollected)
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            float speed = 5f; // Adjust this value to change the speed of the item

            while (Vector3.Distance(transform.position, playerTransform.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                yield return null;
            }
        }

        // audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }

        Destroy(gameObject);
    }
}
