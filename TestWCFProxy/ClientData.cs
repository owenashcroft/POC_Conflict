using System.Runtime.Serialization;

namespace TestWCFProxy
{
    [DataContract]
    public class ClientData
    {
        [DataMember]
        private readonly string _value1;
        [DataMember]
        private readonly string _value2;
        [DataMember]
        private readonly string _value3;
        [DataMember]
        private readonly string _value4;
        [DataMember]
        private readonly string _value5;

        public string Value1
        {
            get { return _value1; }
        }

        public string Value2
        {
            get { return _value2; }
        }

        public string Value3
        {
            get { return _value3; }
        }

        public string Value4
        {
            get { return _value4; }
        }

        public string Value5
        {
            get { return _value5; }
        }

        public ClientData(string value1, string value2, string value3, string value4, string value5)
        {
            _value1 = value1;
            _value2 = value2;
            _value3 = value3;
            _value4 = value4;
            _value5 = value5;
        }
    }
}