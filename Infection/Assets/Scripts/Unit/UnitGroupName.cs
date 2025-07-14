using StatePatteren.State;
using UnityEngine;

public static class UnitGroupName
{
    //PLAYER,ENEMY‚ðPlayer,Enemy‚É•ÏŠ·
    public static string ToProperGroupString(this UnitController.UNIT_GROUP group)
    {
        switch (group)
        {
            case UnitController.UNIT_GROUP.PLAYER:
                return "Player";
            case UnitController.UNIT_GROUP.ENEMY:
                return "Enemy";
            default:
                return group.ToString(); // ”O‚Ì‚½‚ß
        }
    }
}
