
using System.Runtime.InteropServices;
using System.Text;
using Framework.Library;

namespace Common.Tools
{
    public static class IniFile
    {
        // Note this is a private method that is capitalized because the DllImport funcation is capitalized due to it being public
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static string Read(string filePath, string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, filePath);
            if (i > 0)
            {
                return sb.ToString();
            }
            throw new TestFailureException("Couldnt locate the file or key");
        }
    }
}