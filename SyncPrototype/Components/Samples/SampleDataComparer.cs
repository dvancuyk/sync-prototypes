using SyncPrototype.Client;
using SyncPrototype.Connect;

namespace SyncPrototype.Components.Samples
{
    public class SampleDataComparer
    {
        public bool HasChanged(Sample connect, Smpl client)
        {
            EnforcePreconditions(connect, client);

            return connect.Description != client.Description;
        }

        private void EnforcePreconditions(Sample connect, Smpl client)
        {
            var connectId = new SampleIdentity(connect);
            var clientId = new SampleIdentity(client);

            if(connectId != clientId)
            {
                throw new System.InvalidOperationException("The two records must have the same identity");
            }
        }
    }
}
