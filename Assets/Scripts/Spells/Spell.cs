using UnityEngine;
public interface Spell
{
    void CastSpell(Transform projectileSpawnPoint, Transform projectileDirection, bool spellReleased);

    void ResetSpell();
}
