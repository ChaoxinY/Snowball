﻿using UnityEngine;
using System.Collections;

//Using interface makes the classes implementing it more predictable
public interface ICollideAble
{
    void ReactToCollision(Collision collision);
}
