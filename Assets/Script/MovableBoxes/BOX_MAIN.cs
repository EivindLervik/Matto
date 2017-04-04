using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOX_MAIN : MonoBehaviour {

    public float moveSpeed;

    protected bool open;
    protected RectTransform trans;

    protected void GetRect()
    {
        trans = GetComponent<RectTransform>();
    }

    protected void Move()
    {
        if (open)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0.0f, transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-Screen.width * trans.anchorMax.x, transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
        }
    }

    public virtual void Toggle()
    {
        open = !open;
    }

    public virtual void Toggle(bool state)
    {
        open = state;
    }
}
