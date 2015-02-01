using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public interface IDocumentSerializer
    {

        bool Serialize(Document document);
        string Deserialize(Document document);

    }
}
