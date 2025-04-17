using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Hilft für saubere oriniterung für Strings - Gegner werden zb. auch laufen können dürfen aber nicht die gleiche variable haben sonst chaos
public class AnimationStrings
{
    internal static String isMoving = "IsMoving";
    internal static String isRunning = "IsRunning";
    internal static string isGrounded = "IsGrounded";

    internal static string yVelocity = "yVelocity";
    internal static string jump = "jump";

    internal static string isOnCeiling = "IsOnCeiling";
    internal static string isOnWall = "IsOnWall";
    internal static string attack = "attack";

    internal static string canMove = "canMove";
    internal static string hasTarget = "hasTarget";
    internal static string isAlive = "IsAlive";
    internal static string isHit = "IsHit";
    internal static string hitTrigger = "hit";
    internal static string lockVelocity = "lockVelocity";
}

