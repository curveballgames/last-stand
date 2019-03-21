using UnityEngine;
using Curveball;

namespace LastStand
{
    public class SurvivorAvatarGenerator : CBGGameObject
    {
        private static SurvivorAvatarGenerator Singleton;

        public static int NumSkinTones { get => Singleton.SkinMaterials.Length; }
        public static int NumMaleHairstyles { get => Singleton.MaleHairPrefabs.Length; }
        public static int NumFemaleHairstyles { get => Singleton.FemaleHairPrefabs.Length; }
        public static int NumHairColours { get => Singleton.HairMaterials.Length; }

        public Material[] SkinMaterials;
        public Material[] HairMaterials;
        public GameObject[] MaleHairPrefabs;
        public GameObject[] FemaleHairPrefabs;
        public GameObject SurvivorHeadPrefab;

        private void Awake()
        {
            Singleton = this;
        }

        public static GameObject GenerateHeadAvatarForModel(SurvivorModel model)
        {
            GameObject root = new GameObject(model.Name + "'s Head");

            GameObject newHead = Instantiate(Singleton.SurvivorHeadPrefab, root.transform);
            newHead.GetComponent<Renderer>().material = Singleton.SkinMaterials[model.SkinTone];

            GameObject hairPrefab = model.IsMale ? Singleton.MaleHairPrefabs[model.HairStyle] : Singleton.FemaleHairPrefabs[model.HairStyle];
            GameObject hairInstance = Instantiate(hairPrefab, root.transform);
            hairInstance.GetComponent<Renderer>().material = Singleton.HairMaterials[model.HairColour];

            return root;
        }
    }
}