using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace lab3
{

      internal static class Sound
    {
        [DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        internal static extern bool PlaySound(string szSound, System.IntPtr hMod, PlaySoundFlags flags);

       
    }
    [System.Flags]
    public enum PlaySoundFlags : int
    {
        SND_SYNC = 0x0000,
        SND_ASYNC = 0x0001,
        SND_NODEFAULT = 0x0002,
        SND_LOOP = 0x0008,
        SND_NOSTOP = 0x0010,
        SND_NOWAIT = 0x00002000,
        SND_FILENAME = 0x00020000,
        SND_RESOURCE = 0x00040004
    }
}
