﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Player _player;


    public void OnPointerDown(PointerEventData eventData)
    {
        _player.ShootFunc();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _player.StopShootFunc();
    }
}
