using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T m_instance;
    public static T Instance => m_instance;

    protected virtual void Awake()
    {
        m_instance = this as T;
    }
}
