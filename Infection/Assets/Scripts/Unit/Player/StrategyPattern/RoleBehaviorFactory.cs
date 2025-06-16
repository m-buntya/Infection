using System.Collections.Generic;
using UnityEngine;

namespace StrategyPatteren.Role
{
    // �C���X�^���X�����Ďg�p����(�I�u�W�F�N�g�ɃA�^�b�`���Ȃ��Ă悢)
    public static class RoleBehaviorFactory
    {
        // �e���[�����̋�����ۑ�
        private static Dictionary<UnitStats.ROLE, IRoleBehavior> behaviors = new()      // ���[���ǉ����ɂ����ɂ��ǉ�����
        {
            { UnitStats.ROLE.Attacker, new AttackerBehavior() },
            { UnitStats.ROLE.Healer, new HealerBehavior()     },
            { UnitStats.ROLE.Tank, new TankBehavior()         },
        };

        // ���[����n���ČĂяo�������œK�������s�����s��
        public static IRoleBehavior Get(UnitStats.ROLE role) => behaviors[role];
    }
}

