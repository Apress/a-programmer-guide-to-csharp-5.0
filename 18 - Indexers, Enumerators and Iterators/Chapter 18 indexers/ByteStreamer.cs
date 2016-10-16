using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chapter_18_indexers
{
    class ByteStreamer
    {
        string m_filename;
        public ByteStreamer(string filename)
        {
            m_filename = filename;
        }
        public IEnumerator<byte> GetEnumerator()
        {
            using (FileStream stream = File.Open(m_filename, FileMode.Open))
            {
                yield return (byte) stream.ReadByte();
            }
        }
    }
}
