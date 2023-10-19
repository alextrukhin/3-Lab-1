using System.Text;
using System.Text.Json;

namespace lab1db
{
    public class DB
    {
        public readonly static string fileName = AppDomain.CurrentDomain.BaseDirectory + "db.txt";
        public DB() { using StreamWriter w = File.AppendText(fileName); }
        public EntitiesBidirectionalList<Entity> Load()
        {
            EntitiesBidirectionalList<Entity> reading = new();
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 256))
            {
                String className = "Student";
                String line;
                int lineIndex = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if(lineIndex % 2 == 0)
                    {
                        className = line.Split(' ')[0];
                    }
                    else
                    {
                        var EntityType = Type.GetType("lab1db." + className) ?? throw new Exception("DB has unknown entity: "+ className);
                        var el = JsonSerializer.Deserialize(line, EntityType, new JsonSerializerOptions(JsonSerializerDefaults.Web)) as Entity;

                        reading.Push(el);
                    }
                    lineIndex++;
                }
            }
            return reading;
        }
        public void Save(EntitiesBidirectionalList<Entity> listToSave)
        {
            using StreamWriter writetext = new(fileName);
            int entityIndex = 0;
            while (entityIndex < listToSave.Length)
            {
                writetext.WriteLine(listToSave[entityIndex].GetType().Name+" "+ listToSave[entityIndex].LastName);
                writetext.WriteLine(JsonSerializer.Serialize((object) listToSave[entityIndex]));
                entityIndex++;
            }
        }
    }
}
