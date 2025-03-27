using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public float speed;
    private float HorizontalMove, VerticalMove;
    private Rigidbody2D rb;
    public InventoryManager inventoryManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
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
        }
        
    }
}
