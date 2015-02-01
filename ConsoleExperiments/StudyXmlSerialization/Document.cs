using System.Security.AccessControl;

namespace StudyXmlSerialization
{
    public class Document
    {
        public Document(string path, string content, string fileType)
        {
            _path = path;
            _content = content;
            _fileType = fileType;
        }
        private string _path;

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        private string _fileType;

        public string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }
        
    }
}