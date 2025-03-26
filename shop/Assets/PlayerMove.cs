using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private float HorizontalMove, VerticalMove;
    private Rigidbody2D rb;
    public InventoryManager inventoryManager;
    public bool d;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal");
        VerticalMove = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2 (HorizontalMove * speed, VerticalMove * speed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            inventoryManager.AddItem(col.gameObject.GetComponent<item>().Item, col.gameObject.GetComponent<item>().amount);
            Destroy(col.gameObject);
            d = true;
        }
        
    }
}
