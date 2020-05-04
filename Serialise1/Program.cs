using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Serialise1
{
    [Serializable]
    public class X
    {
        private string _xml;
        private XElement _xElement;

        public string Xml
        {
            get
            {
                return _xml;
            }
            set
            {
                _xml = value;
                _xElement = null;
            }
        }

        [XmlIgnore]
        public XElement XElement
        {
            get => GetXElement();
            set
            {
                _xElement = value;
            }
        }

        private XElement GetXElement()
        {
            _xElement = XElement.Parse(_xml);
            return _xElement;
        }

        public X() { }
        public X(string xml)
        {
            _xml = xml;
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var xml = "<Root></Root>";
                var serialiser = new XmlSerializer(typeof(X));
                var x = new X(xml);

                Console.WriteLine($"{x.XElement}");

                var stringwriter = new StringWriter();

                serialiser.Serialize(stringwriter, x);
                Console.WriteLine(stringwriter.ToString());
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }
    }
}
