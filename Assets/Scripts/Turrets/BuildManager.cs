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
            GameObject turretToBuild = BuildManager.GetTurretToBuild();
            Resource.BuildTurret(10);
            instance.ClearTurret();
            return Instantiate(turretToBuild, position, rotation);
        }

        public static void SetTurretToBuild(GameObject _turretToBuild)
        {
            instance.turretToBuild = _turretToBuild;
        }

        public void ClearTurret()
        {
            turretToBuild = null;
        }
    }
}