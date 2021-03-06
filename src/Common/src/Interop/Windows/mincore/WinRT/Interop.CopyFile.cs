// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;

internal partial class Interop
{
    internal partial class mincore
    {
        internal static int CopyFile(String src, String dst, bool failIfExists)
        {
            uint copyFlags = failIfExists ? (uint)Interop.mincore.FileOperations.COPY_FILE_FAIL_IF_EXISTS : 0;
            Interop.mincore.COPYFILE2_EXTENDED_PARAMETERS parameters = new Interop.mincore.COPYFILE2_EXTENDED_PARAMETERS()
            {
                dwSize = (uint)Marshal.SizeOf<Interop.mincore.COPYFILE2_EXTENDED_PARAMETERS>(),
                dwCopyFlags = copyFlags
            };

            int hr = Interop.mincore.CopyFile2(src, dst, ref parameters);

            return Win32Marshal.TryMakeWin32ErrorCodeFromHR(hr);
        }
    }
}
