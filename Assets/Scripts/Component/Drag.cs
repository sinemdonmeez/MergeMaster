using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : Identity
{
    /* ------------------------------------------ */

    public RuntimeAnimatorController Controller;

    public bool IsRunning;

    /* ------------------------------------------ */

    private void Awake()
    {
        ComponentManager.instance.Drag.Add(identity, this);
    }

    private void OnDestroy()
    {
        try
        {
            if (ComponentManager.instance.Drag.ContainsKey(identity))
                ComponentManager.instance.Drag.Remove(identity);
        }
        catch { }

    }
    /* ------------------------------------------ */

    public override void Process()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
    }
    /* ------------------------------------------ */

    public void ChangeAnimation()
    {
        identity.Animator.runtimeAnimatorController = Controller;
    }

    /* ------------------------------------------ */
}
