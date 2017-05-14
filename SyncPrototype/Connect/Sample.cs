namespace SyncPrototype.Connect
{
    public class Sample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Determines if this instance has changed in the cloud and these changes should be persisted.
        /// </summary>
        public bool Changed { get; set; }
        public bool Deleted { get; internal set; }
    }
}
