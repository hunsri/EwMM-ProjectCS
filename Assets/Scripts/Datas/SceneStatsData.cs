namespace Data
{
    ///<summary>
    ///This class holds the necessary data to reload the state of a scene (this saving occurs during each end of a wave)
    ///It is recommended to access the fields of this class through SceneStats
    ///</summary>
    [System.Serializable]
    public class SceneStatsData
    {
        //TODO change access type
        public int Score;
        public string SceneName;
        //this indicates from which completed wave the data is
        public int ResultOfWaveNumber;
        public int CuredNPCS;
        public int VaccinesShot ;
        public int MasksShot;
        //the times someone got infected; spawned infections don't count
        public int InfectionEvents;
    }
}