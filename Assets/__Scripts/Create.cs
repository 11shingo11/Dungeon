using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
