using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : MonoBehaviour,Ihitable
{
    private Flash _flash;

    [SerializeField] private DiscoBallManger _discoBallManger;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
    }

    public void TakeHit()
    {
        _flash.StartFlash();
        _discoBallManger.TriggerDiscoParty();
    }
}
