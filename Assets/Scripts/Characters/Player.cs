using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public EquipController EquipCtrl;
    public static Player Instance;

    void Awake ()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
