namespace TestWCFProxy
{
    using System.Collections.Generic;
    using System.ServiceModel;

    public class TestProjectProxy : ClientBase<ITestProject>, ITestProject
    {
        public bool SubmitNewClientInformation(IEnumerable<ClientData> clientData)
        {
            return Channel.SubmitNewClientInformation(clientData);
        }
    }
}
