using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlFile
{
    [XmlRoot("annotation")]
    public class XmlParameter
    {
        private string mfolder = "train";
        private string mfilename = null;
        private string mpath = null;
        private clsSource msource = null;
        private clsSize msize = null;
        private int msegmented = 0;
        private List<clsObject> mobject = null;

        public string folder
        {
            get { return mfolder; }
            set { mfolder = value; }
        }

        public string filename
        {
            get { return mfilename; }
            set { mfilename = value; }
        }

        public string path
        {
            get { return mpath; }
            set { mpath = value; }
        }

        public clsSource source
        {
            get { return msource; }
            set { msource = value; }
        }

        public clsSize size
        {
            get { return msize; }
            set { msize = value; }
        }

        public int segmented
        {
            get { return msegmented; }
            set { msegmented = value; }
        }

        [XmlElement]
        public List<clsObject> @object
        {
            get { return mobject; }
            set { mobject = value; }
        }

        public XmlParameter()
        {
            clsSource cSource = new clsSource();
            this.source = cSource;

            clsSize cSize = new clsSize();
            this.size = cSize;

            List<clsObject> cObject = new List<clsObject>();
            this.@object = cObject;
        }
    }
    public class clsSource
    {
        private string mdatabase = "Unknown";

        public string database
        {
            get { return mdatabase; }
            set { mdatabase = value; }
        }

        public clsSource()
        {

        }
    }

    public class clsSize
    {
        private int mwidth = 0;
        private int mheight = 0;
        private int mdepth = 0;

        public int width
        {
            get { return mwidth; }
            set { mwidth = value; }
        }

        public int height
        {
            get { return mheight; }
            set { mheight = value; }
        }

        public int depth
        {
            get { return mdepth; }
            set { mdepth = value; }
        }

        public clsSize()
        {

        }

        public clsSize(int _mwidth, int _mheight, int _mdepth)
        {
            mwidth = _mwidth;
            mheight = _mheight;
            mdepth = _mdepth;
        }
    }

    public class clsObject
    {
        private string mname = null;
        private string mpose = "Unspecified";
        private int mtruncated = 0;
        private int mdifficult = 0;
        private clsBndbox mbndbox = null;

        public string name
        {
            get { return mname; }
            set { mname = value; }
        }

        public string pose
        {
            get { return mpose; }
            set { mpose = value; }
        }

        public int truncated
        {
            get { return mtruncated; }
            set { mtruncated = value; }
        }

        public int difficult
        {
            get { return mdifficult; }
            set { mdifficult = value; }
        }

        public clsBndbox bndbox
        {
            get { return mbndbox; }
            set { mbndbox = value; }
        }

        public clsObject()
        {
            clsBndbox cBndBox = new clsBndbox();
            this.bndbox = cBndBox;
        }

        public clsObject(string _mname)
        {
            mname = _mname;
        }

        public clsObject(string _mname, clsBndbox _mbndbox)
        {
            mname = _mname;
            mbndbox = _mbndbox;
        }

        public void SetObject(string _mname, clsBndbox _mbndbox)
        {
            mname = _mname;
            mbndbox = _mbndbox;
        }
    }

    public class clsBndbox
    {
        private int mxmin = 0;
        private int mymin = 0;
        private int mxmax = 0;
        private int mymax = 0;

        public int xmin
        {
            get { return mxmin; }
            set { mxmin = value; }
        }

        public int ymin
        {
            get { return mymin; }
            set { mymin = value; }
        }

        public int xmax
        {
            get { return mxmax; }
            set { mxmax = value; }
        }

        public int ymax
        {
            get { return mymax; }
            set { mymax = value; }
        }

        public clsBndbox()
        {

        }

        public clsBndbox(int _mxmin, int _mymin, int _mxmax, int _mymax)
        {
            mxmin = _mxmin;
            mymin = _mymin;
            mxmax = _mxmax;
            mymax = _mymax;
        }
        public void SetBndbox(int _mxmin, int _mymin, int _mxmax, int _mymax)
        {
            mxmin = _mxmin;
            mymin = _mymin;
            mxmax = _mxmax;
            mymax = _mymax;
        }
    }
}
