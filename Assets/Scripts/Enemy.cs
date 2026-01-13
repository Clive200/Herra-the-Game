using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] points;

    private int i;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        i = 0;
        if(points != null && points.Length > 0){
       
            i = 0;
        }
    }

    void Update()
    {
    
        if(points == null || points.Length == 0) return;
        
        if(i < 0 || i >= points.Length) i = 0;
        
      
        if(points[i] == null) return;
   
        float distance = Vector2.Distance(transform.position, points[i].position);
        if (distance < 0.25f) {
            i++;
            if (i >= points.Length) {
                i = 0; 
            }
        }
        
        
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        
        if(points[i] != null){
            spriteRenderer.flipX = (transform.position.x - points[i].position.x) < 0f;
        }
    }
}
