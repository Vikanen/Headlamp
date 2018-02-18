using MSCLoader;
using UnityEngine;
using System.Threading;

namespace RallyLights
{
    public class RallyLights : Mod
    {
        public override string ID => "Headlamp";
        public override string Name => "Headlamp";
        public override string Author => "Vikanen";
        public override string Version => "1.0";

        private Keybind LampKey = new Keybind("KeyID", "Key name", KeyCode.KeypadMinus);
		
		private bool LampOff;
		public GameObject PLAYER;
		private GameObject lightGameObject;

		public override void OnLoad()
        {
            Keybind.Add(this, LampKey);
            new Thread(FindPlayer).Start();
        }

        void FindPlayer()
        {
            Thread.Sleep(9000);
            PLAYER = GameObject.Find("FPSCamera");
            LampOff = true;
            new Thread(LoadLight).Start();
        }

        void LoadLight()
        {
            Thread.Sleep(1000);

            lightGameObject = new GameObject("HeadlampLight");
            Light lightComp = lightGameObject.AddComponent<Light>();
            lightComp.color = Color.white;
            lightComp.type = LightType.Spot;
            lightComp.spotAngle = 70;
            lightComp.intensity = 5;
            lightComp.range = 20;
            lightGameObject.transform.parent = PLAYER.transform;
            lightGameObject.transform.localPosition = new Vector3(0, 0, 0);
            lightGameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        }


        public override void Update()
        {	
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
