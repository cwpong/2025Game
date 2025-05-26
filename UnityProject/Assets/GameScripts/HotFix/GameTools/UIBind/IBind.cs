using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools
{

    public enum BindType
    {
        UnityElement,
        Component
    }

    public interface IBind
    {
        string ComponentName { get; }

        string Comment { get; }

        Transform Transform { get; }

        BindType GetBindType();
    }
}

