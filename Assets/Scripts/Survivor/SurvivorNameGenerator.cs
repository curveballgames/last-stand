using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class SurvivorNameGenerator
    {
        private static Dictionary<SystemLanguage, List<string>> maleNamesByLanguage;
        private static Dictionary<SystemLanguage, List<string>> femaleNamesByLanguage;

        private static int maleIndex;
        private static int femaleIndex;

        private static SystemLanguage defaultLanguage;

        public static void Initialise()
        {
            defaultLanguage = SystemLanguage.English;

            maleNamesByLanguage = new Dictionary<SystemLanguage, List<string>>
            {
                { SystemLanguage.English, new List<string>{ "Alex", "Ben", "Chris", "David", "Eddy", "Fred", "Greg", "Harry", "Ian", "James", "Kev", "Lewis", "Mick",
                    "Neil", "Owen", "Paul", "Quincy", "Russ" , "Steven", "Tom" } }
            };

            femaleNamesByLanguage = new Dictionary<SystemLanguage, List<string>>
            {
                { SystemLanguage.English, new List<string>{ "Alice", "Beth", "Christina", "Deb", "Erin", "Fern", "Ginny", "Harriet", "Iona", "Jenny", "Kelly", "Laura", "Mandy",
                    "Natalie", "Opal", "Pauline", "Ruby" , "Sophie", "Tara" } }
            };

            maleIndex = 0;
            femaleIndex = 0;

            ShuffleNames(ref maleNamesByLanguage);
            ShuffleNames(ref femaleNamesByLanguage);

            EventSystem.Subscribe<NewGameEvent>(OnNewGame, new Object());
        }

        public static string GenerateName(bool male)
        {
            SystemLanguage selectedLanguage = GetSystemLanguageKey();
            string name;

            if (male)
            {
                List<string> names = maleNamesByLanguage[selectedLanguage];
                name = names[maleIndex];

                maleIndex++;

                if (maleIndex >= names.Count)
                {
                    ShuffleNames(ref maleNamesByLanguage);
                    maleIndex = 0;
                }
            }
            else
            {
                List<string> names = femaleNamesByLanguage[selectedLanguage];
                name = names[femaleIndex];

                femaleIndex++;

                if (femaleIndex >= names.Count)
                {
                    ShuffleNames(ref femaleNamesByLanguage);
                    femaleIndex = 0;
                }
            }

            return name;
        }

        private static void ShuffleNames(ref Dictionary<SystemLanguage, List<string>> namesByLanguage)
        {
            SystemLanguage selectedLanguage = GetSystemLanguageKey();
            List<string> names = namesByLanguage[selectedLanguage];
            int fixedCount = names.Count;

            for (int i = 0; i < fixedCount; i++)
            {
                int swapTo = Random.Range(0, fixedCount);

                string swappedOut = names[swapTo];
                names[swapTo] = names[i];
                names[i] = swappedOut;
            }

            namesByLanguage[selectedLanguage] = names;
        }

        private static void OnNewGame(NewGameEvent e)
        {
            ShuffleNames(ref maleNamesByLanguage);
            ShuffleNames(ref femaleNamesByLanguage);

            maleIndex = 0;
            femaleIndex = 0;
        }

        private static SystemLanguage GetSystemLanguageKey()
        {
            SystemLanguage selectedLanguage = Application.systemLanguage;

            if (!maleNamesByLanguage.ContainsKey(selectedLanguage))
            {
                selectedLanguage = defaultLanguage;
            }

            return selectedLanguage;
        }
    }
}