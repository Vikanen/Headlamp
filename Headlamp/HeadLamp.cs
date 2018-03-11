using MSCLoader;
using UnityEngine;

namespace Headlamp
{
    public class Headlamp : Mod
    {
        public override string ID => "Headlamp";
        public override string Name => "Headlamp";
        public override string Author => "Vikanen";
        public override string Version => "1.2";

        private Keybind LampKey = new Keybind("KeyID", "Key name", KeyCode.KeypadMinus);
		
		private bool LampOff;
        public static bool fullyLoaded = false;
        public GameObject PLAYER;
		private GameObject lightGameObject;

		public override void OnLoad()
        {
            Keybind.Add(this, LampKey);
            LampOff = true;
        }

        public override void Update()
        {
            if (!fullyLoaded)
            {
                if (GameObject.Find("FPSCamera") != null)
                {
                    PLAYER = GameObject.Find("FPSCamera");

                    lightGameObject = new GameObject("HeadlampLight");
                    Light lightComp = lightGameObject.AddComponent<Light>();
                    lightComp.color = Color.white;
                    lightComp.type = LightType.Spot;
                    lightComp.spotAngle = 70;
                    lightComp.intensity = 4;
                    lightComp.range = 20;
                    lightGameObject.transform.parent = PLAYER.transform;
                    lightGameObject.transform.localPosition = new Vector3(0, 0, 0);
                    lightGameObject.transform.localEulerAngles = new Vector3(0, 0, 0);

                    fullyLoaded = true;
                }
            }

            if (LampKey.IsDown())
            {
                LampOff = !LampOff;
            }

			if(LampOff)
			{
				lightGameObject.SetActive(false);
			}
			else
			{
				lightGameObject.SetActive(true);
			}
		}
    }
}
