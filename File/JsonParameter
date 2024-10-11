using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFile
{
    public class JsonParameter
    {
        private Dictionary<string, clsJsonData> mdictionary = null;
        public Dictionary<string, clsJsonData> dictionary
        {
            get { return mdictionary; }
            set { mdictionary = value; }
        }

        public JsonParameter()
        {
            mdictionary = new Dictionary<string, clsJsonData>();
        }

        public string Serialize()
        {
            string resultJson = JsonConvert.SerializeObject(mdictionary);
            return resultJson;
        }

        public string DeSerialize(string _path)
        {
            string resultJson = File.ReadAllText(_path);
            return resultJson;
        }

        public bool Save(string _path, clsJsonData _cJsonData)
        {
            if (_cJsonData.filename == null || _cJsonData.size == 0)
                return false;
            if (Load(_path))
            {
                if (!mdictionary.ContainsKey(_cJsonData.filename + "" + _cJsonData.size))
                {
                    if (_cJsonData.regions.Count == 0)
                        return true;
                    else
                        mdictionary.Add(_cJsonData.filename + "" + _cJsonData.size, _cJsonData);
                }
                else
                {
                    if (_cJsonData.regions.Count == 0)
                        mdictionary.Remove(_cJsonData.filename + "" + _cJsonData.size);
                    else
                        mdictionary[_cJsonData.filename + "" + _cJsonData.size] = _cJsonData;
                }
            }
            else
            {
                if (_cJsonData.regions.Count == 0)
                    return true;
                else
                    mdictionary.Add(_cJsonData.filename + "" + _cJsonData.size, _cJsonData);
            }
            if (mdictionary.Count == 0 && File.Exists(_path))
            {
                new FileInfo(_path).Delete();
                return true;
            }
            string writeJson = Serialize();
            File.WriteAllText(_path, writeJson);
            return true;
        }

        public bool Load(string _path)
        {
            if (!File.Exists(_path))
                return false;
            string readJson = DeSerialize(_path);
            mdictionary = JsonConvert.DeserializeObject<Dictionary<string, clsJsonData>>(readJson);
            return true;
        }
    }

    public class clsJsonData
    {
        private string mfilename = null;
        private long msize = 0;
        private List<clsRegions> mregions = null;
        private clsFileAttributes mfileattributes = null;

        public string filename
        {
            get { return mfilename; }
            set { mfilename = value; }
        }
        public long size
        {
            get { return msize; }
            set { msize = value; }
        }
        public List<clsRegions> regions
        {
            get { return mregions; }
            set { mregions = value; }
        }
        public clsFileAttributes file_attributes
        {
            get { return mfileattributes; }
            set { mfileattributes = value; }
        }

        public clsJsonData()
        {
            List<clsRegions> cRegions = new List<clsRegions>();
            this.regions = cRegions;

            clsFileAttributes cFileAttributes = new clsFileAttributes();
            this.file_attributes = cFileAttributes;
        }
    }

    public class clsRegions
    {
        private clsShapeAttributes mshapeattributes = null;
        private Dictionary<string, string> mregionattributes = null;

        public clsShapeAttributes shape_attributes
        {
            get { return mshapeattributes; }
            set { mshapeattributes = value; }
        }
        public Dictionary<string, string> region_attributes
        {
            get { return mregionattributes; }
            set { mregionattributes = value; }
        }

        public clsRegions()
        {
            mshapeattributes = new clsShapeAttributes();
            mregionattributes = new Dictionary<string, string>();
        }
    }

    public class clsShapeAttributes
    {
        private string mname = null;
        private List<int> mallpointsx = null;
        private List<int> mallpointsy = null;

        public string name
        {
            get { return mname; }
            set { mname = value; }
        }
        public List<int> all_points_x
        {
            get { return mallpointsx; }
            set { mallpointsx = value; }
        }
        public List<int> all_points_y
        {
            get { return mallpointsy; }
            set { mallpointsy = value; }
        }

        public clsShapeAttributes()
        {
            mname = "polygon";
            mallpointsx = new List<int>();
            mallpointsy = new List<int>();
        }
    }

    public class clsFileAttributes
    {

    }
}
