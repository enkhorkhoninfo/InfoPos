using System;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;

namespace ISM.Template
{
    public class Resource
    {
        private Assembly asm = null;
        private Type type = null;
        private Hashtable hash = null;

        public Resource(string resource_folder_name)
        {
            try
            {
                type = this.GetType();
                asm = type.Assembly;
                hash = new Hashtable();

                if (!string.IsNullOrEmpty(resource_folder_name)) resource_folder_name += ".";

                string[] names = asm.GetManifestResourceNames();
                foreach (string s in names)
                {
                    string a = s;
                    a = a.Replace(string.Format("{0}.{1}", type.FullName, resource_folder_name), "");
                    a = a.ToLower();
                    int p = a.LastIndexOf(".");
                    if (p > 0)
                    {
                        a = a.Substring(0, p);
                    }
                    if (!hash.ContainsKey(a))
                    {
                        hash.Add(a, s);
                    }
                }
            }
            catch
            { }
        }
        public Bitmap GetBitmap(string resource_file_name)
        {
            Bitmap bitmap = null;
            try
            {
                if (!string.IsNullOrEmpty(resource_file_name))
                {
                    object obj = hash[resource_file_name.ToLower()];
                    if (obj != null)
                    {
                        System.IO.Stream stream = asm.GetManifestResourceStream((string)obj);
                        bitmap = new Bitmap(stream);
                    }
                }
            }
            catch
            { }
            return bitmap;
        }
        public Image GetImage(string resource_file_name)
        {
            Image image = null;
            try
            {
                object obj = hash[resource_file_name];
                if (obj != null)
                {
                    System.IO.Stream stream = asm.GetManifestResourceStream((string)obj);
                    image = Image.FromStream(stream);
                }
            }
            catch
            { }
            return image;
        }
        public Stream GetStream(string resource_file_name)
        {
            System.IO.Stream stream = null;
            try
            {
                object obj = hash[resource_file_name];
                if (obj != null)
                {
                    stream = asm.GetManifestResourceStream((string)obj);
                }
            }
            catch
            { }
            return stream;
        }
    }
}
