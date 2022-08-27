using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    public abstract void OnSelect();
    public abstract void OnDeselect();
}