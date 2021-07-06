
namespace NPC
{
    /// <summary>
    /// Class to get NPC attributes based on difficulty level;
    /// </summary>
    public class NPCDifficultyLevel
    {
        // todo [] adjust attributes based on difficulty
        public static NPCAttributes EasyNPCAttribute = new NPCAttributes(4, 2, 4, 10);
        public static NPCAttributes MediumNPCAttribute = new NPCAttributes(4.5f, 2.5f, 4.5f, 20);
        public static NPCAttributes HardNPCAttribute = new NPCAttributes(5, 3, 5, 30);
    }
}