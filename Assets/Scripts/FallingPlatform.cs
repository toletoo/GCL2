using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using Color = UnityEngine.Color;
public class FallingPlatform : MonoBehaviour
{
    #region Variables
    private List<SpriteRenderer> spriteRendererList = new List<SpriteRenderer>();
    private BoxCollider2D m_collider;
    #endregion


    void Start()
    {
        GetComponentsInChildren<SpriteRenderer>(spriteRendererList);
        m_collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) // Start PlatformFall when player collides with platform
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("collided");
            StartCoroutine(PlatformFall());
        }
    }

    private IEnumerator PlatformFall()
    {
        int i = 0;
        while (i < 5) //Platform Flashing 5 times
        {
            foreach (SpriteRenderer render in spriteRendererList)
            {   
                render.color = new Color(1, 1, 1, 0.5f);
            }
            yield return new WaitForSeconds(0.1f);
            foreach (SpriteRenderer render in spriteRendererList)
            {   
                render.color = new Color(1, 1, 1, 1f);
            }
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        yield return new WaitForSeconds(0.2f); // pause to avoid flashing while collider disappears
        
        foreach (SpriteRenderer render in spriteRendererList) //diable collider + set sprite opacity to 0
        {
            render.color = new Color(1, 1, 1, 0f);
            m_collider.enabled = false;
        }

        yield return new WaitForSeconds(3f);

        foreach (SpriteRenderer render in spriteRendererList) //reset collider + sprite opacity
        {
            
            render.color = new Color(1, 1, 1, 1f);
            m_collider.enabled = true;
        }
    }


}   
