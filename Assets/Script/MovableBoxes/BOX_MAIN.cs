using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOX_MAIN : MonoBehaviour {
    public float moveSpeed;
    public float openX;
    public float closeX;

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
            transform.position = Vector3.Lerp(transform.position, new Vector3(openX, transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(closeX, transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
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
