using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class AvatarRenderCamera : CBGGameObject
    {
        private static readonly Vector3 HEADSHOT_OFFSET = new Vector3(0f, -0.25f, 3f);

        private static readonly Vector3[] HEAD_OFFSETS = new Vector3[] {
            new Vector3(0f, -0.5f, 3.5f),
            new Vector3(1.6f, -0.25f, 4.25f),
            new Vector3(-1.6f, -0.25f, 4.25f)
        };
        private const int HEADSHOT_IMAGE_SIZE = 120;

        private static AvatarRenderCamera Singleton;
        private static Dictionary<string, RenderTexture> headshots;

        public Camera RenderCam;
        public Light Light;

        private void Awake()
        {
            Singleton = this;
            headshots = new Dictionary<string, RenderTexture>();

            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
        }

        void OnNewGame(NewGameEvent e)
        {
            if (headshots != null && headshots.Count > 0)
            {
                foreach (RenderTexture texture in headshots.Values)
                {
                    texture.Release();
                    Destroy(texture);
                }
            }

            headshots = new Dictionary<string, RenderTexture>();
        }

        public static RenderTexture RenderHeadshot(SurvivorModel model)
        {
            if (headshots.ContainsKey(model.Name))
            {
                return headshots[model.Name];
            }

            RenderTexture texture = new RenderTexture(HEADSHOT_IMAGE_SIZE, HEADSHOT_IMAGE_SIZE, 16);
            Singleton.RenderCam.targetTexture = texture;

            GameObject head = SurvivorAvatarGenerator.GenerateHeadAvatarForModel(model);
            head.transform.SetParent(Singleton.RenderCam.transform);
            head.transform.localPosition = HEADSHOT_OFFSET;
            head.transform.Rotate(0f, 180f, 0f);

            Singleton.Light.enabled = true;
            Singleton.RenderCam.Render();
            Singleton.RenderCam.targetTexture = null;
            Singleton.Light.enabled = false;

            DestroyImmediate(head);

            headshots.Add(model.Name, texture);
            return texture;
        }

        public static RenderTexture RenderScavengerTeam(ScavengerTeamModel model)
        {
            RenderTexture texture = new RenderTexture(HEADSHOT_IMAGE_SIZE, HEADSHOT_IMAGE_SIZE, 16);
            Singleton.RenderCam.targetTexture = texture;

            List<GameObject> heads = new List<GameObject>();
            int count = 0;

            foreach (SurvivorModel survivor in model.LinkedRoom.AssignedSurvivors)
            {
                GameObject head = SurvivorAvatarGenerator.GenerateHeadAvatarForModel(survivor);
                head.transform.SetParent(Singleton.RenderCam.transform);
                head.transform.localPosition = HEAD_OFFSETS[count];
                head.transform.Rotate(0f, 180f, 0f);

                heads.Add(head);

                count++;

                if (count >= HEAD_OFFSETS.Length)
                    break;
            }

            Singleton.Light.enabled = true;
            Singleton.RenderCam.Render();
            Singleton.RenderCam.targetTexture = null;
            Singleton.Light.enabled = false;

            foreach (GameObject head in heads)
            {
                DestroyImmediate(head);
            }

            return texture;
        }
    }
}