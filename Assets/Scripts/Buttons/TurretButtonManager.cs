using Turrets;
using UnityEngine;

namespace Buttons
{
    public class TurretButtonManager : MonoBehaviour
    {
        [SerializeField] protected GameObject TurretPrefab;

        public void SetTurret()
        {
            BuildManager.SetTurretToBuild(TurretPrefab);
        }
    }
}