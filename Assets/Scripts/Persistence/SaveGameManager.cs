using System.IO;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class SaveGameManager : CBGGameObject
    {
        public const int SAVE_SLOT_COUNT = 3;

        private static readonly string LAST_SAVED_SLOT_PLAYER_PREFS_KEY = "last_save_slot";
        private static readonly string METADATA_FILE_PREFIX = ".savemetadata";
        private static readonly string SAVE_FILE_PREFIX = "savegame";
        private static string SAVE_FILE_DIRECTORY;

        public static SaveGameMetadata[] MetadataEntries { get; private set; }

        private static int lastSavedSlot;

        private void Awake()
        {
            SAVE_FILE_DIRECTORY = Application.persistentDataPath + "/savegames/";
            lastSavedSlot = Mathf.Clamp(PlayerPrefs.GetInt(LAST_SAVED_SLOT_PLAYER_PREFS_KEY, 0), 0, SAVE_SLOT_COUNT);
            InitialiseMetadata();
        }

        private static void InitialiseMetadata()
        {
            MetadataEntries = new SaveGameMetadata[SAVE_SLOT_COUNT];
            string basePath = SAVE_FILE_DIRECTORY + METADATA_FILE_PREFIX;

            for (int i = 0; i < SAVE_SLOT_COUNT; i++)
            {
                try
                {
                    string json = File.ReadAllText(basePath + i);
                    MetadataEntries[i] = SaveGameMetadata.FromJSON(json);
                }
                catch (System.Exception) { }
            }
        }

        public static bool IsSaveSlotEmpty(int index)
        {
            return MetadataEntries[index].IsEmpty();
        }

        public static bool SaveGame(int saveFileIndex)
        {
            SaveGameMetadata metadata = new SaveGameMetadata();
            metadata.BaseName = BaseModel.CurrentBase.Name;
            metadata.SaveTimestamp = System.DateTime.Now.Ticks;

            SaveGameData saveData = new SaveGameData();
            saveData.CurrentBase = BaseModel.CurrentBase;
            saveData.Survivors = SurvivorModel.AllModels.ToArray();

            try
            {
                File.WriteAllText(SAVE_FILE_DIRECTORY + METADATA_FILE_PREFIX + saveFileIndex, metadata.ToJSON());
                File.WriteAllText(SAVE_FILE_DIRECTORY + SAVE_FILE_PREFIX + saveFileIndex, saveData.ToJSON());
            }
            catch (System.Exception)
            {
                Debug.Log("An error occured while saving the game");
                return false;
            }

            return true;
        }

        public static void LoadGame(int saveFileIndex)
        {
            SaveGameData saveGame = null;

            try
            {
                string json = File.ReadAllText(SAVE_FILE_DIRECTORY + SAVE_FILE_PREFIX + saveFileIndex);
                saveGame = SaveGameData.FromJSON(json);
            }
            catch (System.Exception)
            {
                Debug.Log("Could not save game");
                return;
            }

            BaseModel.CurrentBase = saveGame.CurrentBase;
            EventSystem.Publish(new ModelsInitialisedEvent());
        }
    }
}