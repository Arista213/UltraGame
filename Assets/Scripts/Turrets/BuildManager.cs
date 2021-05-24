using General;
using UnityEngine;

namespace Turrets
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager instance;
        private GameObject turretToBuild;

        void Awake()
        {
            if (instance != null)
            {
                return;
            }

            instance = this;
        }

        public bool CanBuild
        {
            get { return turretToBuild != null; }
        }

        public static GameObject GetTurretToBuild()
        {
            return instance.turretToBuild;
        }

        public static GameObject BuildTurret(Vector3 position, Quaternion rotation)
        {
            GameObject turretToBuild = GetTurretToBuild();
            var price = turretToBuild.GetComponent<Turret>().BuildPrice;
            if (Resource.BuildTurret(price))
            {
                ClearTurret();
                return Instantiate(turretToBuild, position, rotation);
            }

            ClearTurret();
            return null;
        }

        public static void SetTurretToBuild(GameObject _turretToBuild)
        {
            instance.turretToBuild = _turretToBuild;
        }

        public static void ClearTurret()
        {
            instance.turretToBuild = null;
        }
    }
}