namespace NPC
{
    /// <summary>
    /// Attributes for each NPC (to adjust their behaviors)
    /// </summary>
    public class NPCAttributes
    {
        private float speed;
        private float infectedSpeed;
        private float curedSpeed;
        private int infectionRate;

        public float CuredSpeed { get => curedSpeed; set => curedSpeed = value; }
        public int InfectionRate { get => infectionRate; set => infectionRate = value; }
        public float InfectedSpeed { get => infectedSpeed; set => infectedSpeed = value; }
        public float Speed { get => speed; set => speed = value; }

        public NPCAttributes(float Speed, float InfectedSpeed, float CuredSpeed, int InfectionRate)
        {
            this.Speed = Speed;
            this.InfectedSpeed = Speed;
            this.CuredSpeed = CuredSpeed;
            this.InfectionRate = InfectionRate;
        }
    }
}