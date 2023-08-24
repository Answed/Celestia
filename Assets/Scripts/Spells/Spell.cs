using System;
using UnityEngine;
public interface Spell
{
    void CastSpell(Transform projectileSpawnPoint, Transform projectileDirection);

    void ResetSpell();
}
