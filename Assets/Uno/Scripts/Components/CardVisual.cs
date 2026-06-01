
using UnityEngine;

public class CardVisual: MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Transform target;
    public float smoothingTime = 0.2f;
    private Vector3 currentVelocity = Vector3.zero;
    
    void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        transform.position = Vector3.SmoothDamp(
                    transform.position,
                    target.position,
                    ref currentVelocity,
                    smoothingTime,
                    Mathf.Infinity,
                    Time.deltaTime
                );
        
        //check if 
    }
    
    private Vector3 velocity = Vector3.zero;
}