namespace Data
{
    ///<summary>
    /// This class holds the data that determines the players progress
    /// It is highly recommended to only access the values of this class through <c>PlayerStats</c>
    ///</summary>
    [System.Serializable]
    public class PlayerStatsData
    {
        public int Score;
        public int XP;
        public int CuredNPCS;
    }
}