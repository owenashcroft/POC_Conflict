﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestFTP.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ITestProject")]
    public interface ITestProject {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestProject/SubmitNewClientInformation", ReplyAction="http://tempuri.org/ITestProject/SubmitNewClientInformationResponse")]
        bool SubmitNewClientInformation(TestWCFProxy.ClientData[] clientData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestProject/SubmitNewClientInformation", ReplyAction="http://tempuri.org/ITestProject/SubmitNewClientInformationResponse")]
        System.Threading.Tasks.Task<bool> SubmitNewClientInformationAsync(TestWCFProxy.ClientData[] clientData);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITestProjectChannel : TestFTP.ServiceReference1.ITestProject, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestProjectClient : System.ServiceModel.ClientBase<TestFTP.ServiceReference1.ITestProject>, TestFTP.ServiceReference1.ITestProject {
        
        public TestProjectClient() {
        }
        
        public TestProjectClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TestProjectClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestProjectClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestProjectClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool SubmitNewClientInformation(TestWCFProxy.ClientData[] clientData) {
            return base.Channel.SubmitNewClientInformation(clientData);
        }
        
        public System.Threading.Tasks.Task<bool> SubmitNewClientInformationAsync(TestWCFProxy.ClientData[] clientData) {
            return base.Channel.SubmitNewClientInformationAsync(clientData);
        }
    }
}
