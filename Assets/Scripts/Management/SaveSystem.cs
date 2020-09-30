using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GridMaster {
    static public class SaveSystem
    {
        public static void SaveWorld (int seed, MapGeneration map, FactionHandler factions, SpellGeneration spells, LoreHandler lore) {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + seed.ToString() + "world.data";
            FileStream stream = new FileStream(path, FileMode.Create);

            WorldData worldData = new WorldData(factions, map, spells, lore);

            formatter.Serialize(stream, worldData);
            stream.Close();
        }

        public static WorldData LoadWorld (int seed) {
            string path = Application.persistentDataPath + "/" + seed.ToString() + "world.data";
            if (File.Exists(path)) {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                WorldData data = formatter.Deserialize(stream) as WorldData;
                stream.Close();
                return data;
            } else {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }

        public static void SavePlayer (int seed, GameObject player) {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + seed.ToString() + "player.data";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData playerData = new PlayerData(player);

            formatter.Serialize(stream, playerData);
            stream.Close();
        }

        public static PlayerData LoadPlayer (int seed) {
            string path = Application.persistentDataPath + "/" + seed.ToString() + "player.data";
            if (File.Exists(path)) {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            } else {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
    }
}
