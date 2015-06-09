namespace TestWCFProxy
{
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface ITestProject
    {

        [OperationContract]
        bool SubmitNewClientInformation(IEnumerable<ClientData> clientData);
    }
}
