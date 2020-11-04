namespace CountdownTimer
{
    public class Stage
    {
        public string Name { get; set; }
        public int Seconds { get; set; }
        public string During { get; set; }
        public string After { get; set; }
        public bool Default { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
