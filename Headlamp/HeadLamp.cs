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
        public override string Version => "1.0a";

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
            PLAYER = GameObject.Find("PLAYER");
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
            lightComp.intensity = 1;
            lightComp.range = 10;
            lightGameObject.transform.parent = PLAYER.transform;
            lightGameObject.transform.localPosition = new Vector3(0, 1.3f, 0.01f);
            lightGameObject.transform.localEulerAngles = new Vector3(10, 0, 0);
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
