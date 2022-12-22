using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireArm : Weapon
{
    public bool Single, SemiAuto, FullAuto;
    public enum ActionType { normal, needmotion }
    public enum ReloadType { changeMag, insertBullet}
}
