using ExLumina.SketchUp.API;
using System.Text;

namespace ExLumina.SketchUp.Factory
{
    internal static class Convert
    {
        public static string ToString(SU.StringRef suStringRef)
        {
            // We could add code to the API wrapper and use
            // the String(char*) constructor, because the C
            // API's SUStringRef is just a pointer to a buffer
            // full of Unicode. But that would be programming
            // to the implementation, not the interface, and
            // the C API is still changing. We will use this
            // more cumbersome method to be on the safe side.

            long length;

            SU.StringGetUTF8Length(suStringRef, out length);

            byte[] buff = new byte[length];

            SU.StringGetUTF8(
                suStringRef,
                buff.Length,
                buff,
                out length);

            return Encoding.UTF8.GetString(buff, 0, buff.Length);
        }

        public static string ToStringAndRelease(SU.StringRef suStringRef)
        {
            string convertedString = ToString(suStringRef);

            SU.StringRelease(suStringRef);

            return convertedString;
        }
    }
}
